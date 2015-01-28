using System.Data;
using System.Data.OleDb;
//using Microsoft.Office.Interop.Excel;
using Microsoft.CSharp;
using System.Collections.Generic;

namespace PaySlipGeneration
{
    public static class ClsUploadExcel
    {


        public static DataSet exceldata(string filelocation,string SheetType)
        {
         
            DataSet ds = new DataSet();
            OleDbCommand excelCommand = new OleDbCommand(); OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter();            
            string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filelocation + ";Excel 12.0 Xml; HDR=YES";
            OleDbConnection excelConn = new OleDbConnection(excelConnStr);          
            excelConn.Open();
            System.Data.DataTable dt = new System.Data.DataTable();
            //if (SheetType.Trim() == "E")
            //{
            //    excelCommand = new OleDbCommand("Select * from [EmployeesDataSheet$]", excelConn);EmployeesDataSheet
            //}
            //else
            //{
            //    excelCommand = new OleDbCommand("Select * from [TraineesDataSheet$]", excelConn);
            //}
            excelCommand = new OleDbCommand("Select * from [EmployeesDataSheet$]", excelConn);           
            excelDataAdapter.SelectCommand = excelCommand;
            excelDataAdapter.Fill(dt);
            ds.Tables.Add(dt);
            excelConn.Close();

            DataTable dtLeav=ReadExcelCellValue(filelocation);


            ds.Tables[0].Columns.Add("Leave Bal C/F", typeof(string));
            ds.Tables[0].Columns.Add("Leave Credited", typeof(string));
            ds.Tables[0].Columns.Add("Leave Taken", typeof(string));
            ds.Tables[0].Columns.Add("Leave Bal EOM", typeof(string));

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                var query = from p in dtLeav.AsEnumerable()
                            where p.Field<string>("EmpId") == dr["Emp'e ID"].ToString()
                            select new
                            {
                                EmpId = p.Field<string>("EmpId"),
                                LeaveCarryForward = p.Field<string>("Bal C/F"),
                                LeaveCredited = p.Field<string>("Leave Credited"),
                                LeaveTaken = p.Field<string>("Leave Taken"),
                                LeaveBalanceEOM = p.Field<string>("Bal EOM")
                                
                            };                 
                foreach (var name in query)
                {
                    dr["Leave Bal C/F"] = name.LeaveCarryForward.ToString();
                    dr["Leave Credited"] = name.LeaveCredited.ToString();
                    dr["Leave Taken"] = name.LeaveTaken.ToString();
                    dr["Leave Bal EOM"] = name.LeaveBalanceEOM.ToString(); 
                }
            }
            return ds;
        }

        public static DataTable ReadExcelCellValue(string filelocation)
        {

            DataSet ds = new DataSet();            
            OleDbCommand excelCommand = new OleDbCommand(); OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter();
            string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filelocation + ";Excel 12.0 Xml; HDR=YES";
            OleDbConnection excelConn = new OleDbConnection(excelConnStr);
            excelConn.Open();
            System.Data.DataTable dt = new System.Data.DataTable();
           

            //excelCommand = new OleDbCommand("Select * from [EmployeesDataSheet$]", excelConn);
            excelCommand = new OleDbCommand("Select * from [VacationData$]", excelConn);
            excelDataAdapter.SelectCommand = excelCommand;
            excelDataAdapter.Fill(dt);
            ds.Tables.Add(dt);
            excelConn.Close();

            DataTable dtLeave = new DataTable();
            dtLeave.Columns.Add("EmpId", typeof(string));
            dtLeave.Columns.Add("EmpName", typeof(string));
            dtLeave.Columns.Add("DOJ", typeof(string));
            dtLeave.Columns.Add("Bal C/F", typeof(string));
            dtLeave.Columns.Add("Leave Credited", typeof(string));
            dtLeave.Columns.Add("Leave Taken", typeof(string));
            dtLeave.Columns.Add("Bal EOM", typeof(string));

            string strEmpId = string.Empty;
            string strEmpName = string.Empty;
            string strDoj = string.Empty;

            string strBalCF = string.Empty;
            string strLeavCredit = string.Empty;
            string strLeavtak = string.Empty;

            
            foreach(DataRow dr in ds.Tables[0].Rows)
            {

                if ((!string.IsNullOrEmpty(dr[2].ToString())) && (string.IsNullOrEmpty(dr[3].ToString())) && (dr[2].ToString()!="Adjustment Explanation:") && (!string.IsNullOrEmpty(dr[0].ToString())))//(!string.IsNullOrEmpty(dr[0].ToString())) && (!string.IsNullOrEmpty(dr[1].ToString())) && 
                {
                   
                    strEmpId = dr[0].ToString();
                    strEmpName = dr[2].ToString();
                    strDoj = dr[1].ToString();
                    continue;
                }
               
                if ((!string.IsNullOrEmpty(dr[2].ToString())) && (dr[2].ToString().Trim() == "Bal c/f from previous month"))
                    {
                      
                        strBalCF = dr[3].ToString();
                      
                        continue;
                    }
                
                if ((!string.IsNullOrEmpty(dr[2].ToString())) && (dr[2].ToString().Trim() == "Leave credited"))
                {                   
                    strLeavCredit = dr[3].ToString();
                    
                    continue;
                }

                if ((!string.IsNullOrEmpty(dr[2].ToString())) && (dr[2].ToString().Trim() == "Leave taken"))
                {
                    
                    strLeavtak = dr[3].ToString();
                   
                    continue;
                }

                if ((!string.IsNullOrEmpty(dr[2].ToString())) && (dr[2].ToString().Trim() == "Bal as of end of current month"))
                {
                    DataRow drleave = dtLeave.NewRow();
                    drleave["EmpId"] = strEmpId;
                    drleave["DOJ"] = strDoj;
                    drleave["EmpName"] = strEmpName;
                    drleave["Bal C/F"] = strBalCF;
                    drleave["Leave Credited"] = strLeavCredit;
                    drleave["Leave Taken"] = strLeavtak;
                    drleave["Bal EOM"] = dr[3].ToString();
                    dtLeave.Rows.Add(drleave);
                }
             
            }

            return dtLeave;
        }

    }
}