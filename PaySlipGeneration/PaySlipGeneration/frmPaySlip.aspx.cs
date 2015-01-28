using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Configuration;
using ASPSnippets.SmsAPI;
using Joshi.Utils.Imap;

namespace PaySlipGeneration
{
    public partial class frmPaySlip : System.Web.UI.Page
    {
        string filename;
        public static string filePath;
        public static int rowNo = 0;
        clsConfig objConfig;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                btnClear.Visible = false;
                objConfig = new clsConfig();
                string strRetVal = objConfig.ReadConfigFile();
                if (!strRetVal.Trim().Contains(ConfigErrorMessage.SUCCESS))
                {
                    lblMsg.Visible = true;
                    lblMsg.Style.Add("color", "red");
                    lblMsg.Text = strRetVal;
                    FileUploadToServer.Enabled = false;
                    btnUpload.Enabled = false;

                }
                else
                {
                    FileUploadToServer.Enabled = true;
                    btnUpload.Enabled = true;
                }

            }
            else
            {
                // BindPage();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            BindGridView(rbtnlst.SelectedValue);
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindPage(rbtnlst.SelectedValue);
            grvBatchUpload.PageIndex = e.NewPageIndex;
            grvBatchUpload.DataBind();
            lblMsg.Visible = false;

        }
        protected void OnPagingTrainee(object sender, GridViewPageEventArgs e)
        {
            BindPage(rbtnlst.SelectedValue);
            //grvTrainee.PageIndex = e.NewPageIndex;
            //grvTrainee.DataBind();
            lblMsg.Visible = false;

        }
        private void BindPage(string SheetType)
        {
            if (SheetType.Trim() == "E")
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = ClsUploadExcel.exceldata(filePath, SheetType);
                dt = ds.Tables[0];

                if (dt != null && dt.Rows.Count > 0) // Check if File is Blank or not
                {
                    grvBatchUpload.DataSource = ds;
                    grvBatchUpload.DataBind();
                    lblMsg.Visible = false;
                }
            }
            else
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = ClsUploadExcel.exceldata(filePath, SheetType);
                dt = ds.Tables[0];

                if (dt != null && dt.Rows.Count > 0) // Check if File is Blank or not
                {
                    //grvTrainee.DataSource = ds;
                    //grvTrainee.DataBind();
                    lblMsg.Visible = false;
                }
            }

        }


        public void BindGridView(string sheetType)
        {
            string FilePath = ResolveUrl("~/Uploads/"); // Give Upload File Path
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //string filePath = string.Empty;
            if (FileUploadToServer.HasFile) // Check FileControl has any file or not
            {
                try
                {
                    string[] allowdFile = { ".xls", ".xlsx" };
                    string FileExt = System.IO.Path.GetExtension(FileUploadToServer.PostedFile.FileName).ToLower();// get extensions
                    bool isValidFile = allowdFile.Contains(FileExt);

                    // check if file is valid or not
                    if (!isValidFile)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Style.Add("color", "red");
                        lblMsg.Text = ConfigErrorMessage.UPLOAD_XML.ToString();
                    }
                    else
                    {
                        int FileSize = FileUploadToServer.PostedFile.ContentLength; // get filesize
                        if (FileSize <= 1048576) //1048576 byte = 1MB
                        {
                            filename = Path.GetFileName(Server.MapPath(FileUploadToServer.FileName));// get file name
                            FileUploadToServer.SaveAs(Server.MapPath(FilePath) + filename); // save file to uploads folder
                            filePath = Server.MapPath(FilePath) + filename;
                            if (sheetType.Trim() == "E")
                            {
                                ds.Clear();
                                ds = ClsUploadExcel.exceldata(filePath, sheetType);
                              //ClsUploadExcel.ReadExcelCellValue(filePath);
                                dt = ds.Tables[0];
                                if (dt != null && dt.Rows.Count > 0) // Check if File is Blank or not
                                {
                                    grvBatchUpload.DataSource = ds;
                                    grvBatchUpload.DataBind();
                                    lblMsg.Visible = false;
                                    btnClear.Visible = true;
                                }
                                else
                                {

                                    lblMsg.Visible = true;
                                    lblMsg.Style.Add("color", "red");                                   
                                    lblMsg.Text = ConfigErrorMessage.NO_ROW_FOUND.ToString();
                                }
                            }
                            else
                            {
                                ds.Clear();
                                ds = ClsUploadExcel.exceldata(filePath, sheetType);
                                dt = ds.Tables[0];
                                if (dt != null && dt.Rows.Count > 0) // Check if File is Blank or not
                                {
                                    //grvTrainee.DataSource = ds;
                                    //grvTrainee.DataBind();
                                    lblMsg.Visible = false;
                                    btnClear.Visible = true;
                                }
                                else
                                {

                                    lblMsg.Visible = true;
                                    lblMsg.Style.Add("color", "red");                                   
                                    lblMsg.Text = ConfigErrorMessage.NO_ROW_FOUND.ToString();
                                }
                            }
                            FilePath = ResolveUrl("~/Uploads/");
                            string fileName = Server.MapPath(FilePath) + filename;
                            FileInfo f = new FileInfo(fileName);
                            if (f.Exists)
                            {
                                //f.IsReadOnly = false;
                                //f.Delete();
                            }
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Style.Add("color", "red");                            
                            lblMsg.Text = ConfigErrorMessage.FILE_SIZE.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Style.Add("color", "red");                    
                    lblMsg.Text = ConfigErrorMessage.ERROR_UPLOADING + " :" + ex.Message;
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Style.Add("color", "red");
                lblMsg.Text = ConfigErrorMessage.SELECT_FILE.ToString();
            }
        }

        protected void GenerateReport(object sender, EventArgs e)
        {
            clsPaySlipFactory objPaySlip = new clsPaySlipFactory();
            IGeneratePDF objpdf = objPaySlip.GetPaySlipObject(rbtnlst.SelectedValue);//new clsGenerateEmployeePaySlip();

            if (rbtnlst.SelectedValue.Trim() == "E")
            {
                foreach (GridViewRow dr in grvBatchUpload.Rows)
                {
                    CheckBox chk = (CheckBox)dr.FindControl("chkSelect");
                    if (chk != null & chk.Checked)
                    {

                        byte[] bytes = objpdf.GeneratePDF(dr);
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + dr.Cells[3].Text + ".pdf");
                        Response.ContentType = "application/pdf";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                        Response.Close();
                    }

                }
            }
            else
            {

                //foreach (GridViewRow dr in grvTrainee.Rows)
                //{
                //    CheckBox chk = (CheckBox)dr.FindControl("chkSelect");
                //    rowNo += rowNo;
                //    if (chk != null & chk.Checked)
                //    {
                //        //btnGenPdf_Click(sender, e);

                //        byte[] bytes = objpdf.GeneratePDF(dr);
                //        HttpContext.Current.Response.ClearHeaders();
                //        HttpContext.Current.Response.ClearContent();
                //        Response.Clear();
                //        Response.ContentType = "application/pdf";
                //        Response.AddHeader("Content-Disposition", "attachment; filename=" + dr.Cells[3].Text + ".pdf");
                //        Response.ContentType = "application/pdf";
                //        Response.Buffer = true;
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.BinaryWrite(bytes);
                //        Response.Flush();
                //        Response.End();
                //        Response.Close();

                //    }

                //}

            }
        }

        protected void rbtnlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnlst.SelectedValue.Trim() == "T")
            {
                lblMsg.Visible = false;
                grvBatchUpload.Enabled = false;
                grvBatchUpload.Visible = false;
                grvBatchUpload.Dispose();
                //grvTrainee.Visible = true;
                //grvTrainee.Enabled = true;
            }
            else if (rbtnlst.SelectedValue.Trim() == "E")
            {
                lblMsg.Visible = false;
                //grvTrainee.Enabled = false;
                //grvTrainee.Visible = false;
                //grvTrainee.Dispose();
                grvBatchUpload.Enabled = true;
                grvBatchUpload.Visible = true;

            }
        }


        //protected void grvTrainee_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow dr = grvTrainee.Rows[index];
        //        clsPaySlipFactory objPaySlip = new clsPaySlipFactory();
        //        IGeneratePDF objpdf;
        //        if ((!string.IsNullOrEmpty(dr.Cells[6].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[7].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[8].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[9].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[10].Text.Trim())))
        //        {
        //            if ((Convert.ToInt32(dr.Cells[6].Text) == 0) && (Convert.ToInt32(dr.Cells[7].Text) == 0) && (Convert.ToInt32(dr.Cells[8].Text) == 0) && (Convert.ToInt32(dr.Cells[9].Text) == 0) && (Convert.ToInt32(dr.Cells[10].Text) == 0))
        //            {
        //                objpdf = objPaySlip.GetPaySlipObject("E");
        //            }
        //            else
        //            {
        //                objpdf = objPaySlip.GetPaySlipObject("T");
        //            }
        //        }
        //        else
        //        {
        //            lblMsg.Visible = true;
        //            lblMsg.Style.Add("color", "red");                   
        //            lblMsg.Text = ConfigErrorMessage.DATA_FORMAT;
        //            return;
        //        }
        //        if (e.CommandName == "Select")
        //        {

        //            byte[] bytes = objpdf.GeneratePDF(dr);
        //            Response.ClearHeaders();
        //            Response.ClearContent();
        //            Response.Clear();
        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("Content-Disposition", "attachment; filename=" + dr.Cells[3].Text + ".pdf");
        //            Response.ContentType = "application/pdf";
        //            Response.Buffer = true;
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.BinaryWrite(bytes);
        //            Response.End();
        //            Response.Close();

        //        }
        //        else if (e.CommandName == "SendMail")
        //        {
        //            if ((!string.IsNullOrEmpty(dr.Cells[2].Text.Trim()))) //(!string.IsNullOrEmpty(dr.Cells[11].Text.Trim())) &&
        //            {
        //                // TO GET THE EMAIL ID OF EMPLOYEE FROM XML FILE.

                       
        //                var empMail = clsConfig.objTrainMailList.Where(x => x.EmpId == dr.Cells[3].Text).ToList();

        //                MailMessage message = new MailMessage();
        //                // message.To.Add(dr.Cells[11].Text.Trim());// Email-ID of Receiver pavanm@menlo-technologies.com 
        //                message.To.Add(empMail[0].EmailId);// Email-ID of Receiver pavanm@menlo-technologies.com 
        //                message.Subject = "PaySlip";// Subject of Email  
        //                message.From = new System.Net.Mail.MailAddress("menlotechoffshore@gmail.com");// Email-ID of Sender  
        //                message.IsBodyHtml = true;

        //                MemoryStream file = new MemoryStream(objpdf.GeneratePDF(dr));

        //                file.Seek(0, SeekOrigin.Begin);
        //                Attachment data = new Attachment(file, dr.Cells[2].Text + ".pdf", "application/pdf");
        //                ContentDisposition disposition = data.ContentDisposition;
        //                disposition.CreationDate = System.DateTime.Now;
        //                disposition.ModificationDate = System.DateTime.Now;
        //                disposition.DispositionType = DispositionTypeNames.Attachment;
        //                message.Attachments.Add(data);//Attach the file  

        //                message.Body = "Please find pay slip as an attachment.";
        //                SmtpClient SmtpMail = new SmtpClient();
        //                SmtpMail.Host = "smtp.gmail.com";//"Your Host";//name or IP-Address of Host used for SMTP transactions  
        //                SmtpMail.Port = 587;//Port for sending the mail  
        //                SmtpMail.Credentials = new System.Net.NetworkCredential("menlotechoffshore@gmail.com", "menlo123");//username/password of network, if apply  
        //                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                SmtpMail.EnableSsl = true;
        //                SmtpMail.ServicePoint.MaxIdleTime = 0;
        //                SmtpMail.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);
        //                message.BodyEncoding = Encoding.Default;
        //                message.Priority = MailPriority.Normal;
        //                SmtpMail.Send(message); //Smtpclient to send the mail message  
        //                lblMsg.Visible = true;
        //                lblMsg.Style.Add("color", "green");                       
        //                lblMsg.Text = ConfigErrorMessage.EMAIL_SEND_SUCCESS;
        //            }
        //            else
        //            {
        //                lblMsg.Visible = true;
        //                lblMsg.Style.Add("color", "red");
        //                lblMsg.Text = ConfigErrorMessage.EMPLOYEE_DOESNOT_EXIST;
                        
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Visible = true;
        //        lblMsg.Style.Add("color", "red");
        //        lblMsg.Text = ConfigErrorMessage.EMAIL_SEND_FAIL+"--" + ex.Message;

        //    }
        //}



        protected void grvBatchUpload_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMsg.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow dr = grvBatchUpload.Rows[index];
                clsPaySlipFactory objPaySlip = new clsPaySlipFactory();
                IGeneratePDF objpdf;
                if ((!string.IsNullOrEmpty(dr.Cells[6].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[7].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[8].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[9].Text.Trim())) && (!string.IsNullOrEmpty(dr.Cells[10].Text.Trim())))
                {
                    if ((Convert.ToDouble(dr.Cells[6].Text) == 0) && (Convert.ToDouble(dr.Cells[7].Text) == 0) && (Convert.ToDouble(dr.Cells[8].Text) == 0) && (Convert.ToDouble(dr.Cells[9].Text) == 0) && (Convert.ToDouble(dr.Cells[10].Text) == 0))
                    {
                        objpdf = objPaySlip.GetPaySlipObject("T");
                    }
                    else
                    {
                        objpdf = objPaySlip.GetPaySlipObject("E");
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Style.Add("color", "red");
                    lblMsg.Text = ConfigErrorMessage.DATA_FORMAT;
                    return;
                }

                if (e.CommandName == "Select")
                {

                    byte[] bytes = objpdf.GeneratePDF(dr);
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + dr.Cells[3].Text + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    Response.Close();

                }
                else if (e.CommandName == "SendMail")
                {
                    if ((!string.IsNullOrEmpty(dr.Cells[3].Text.Trim())))//!string.IsNullOrEmpty(dr.Cells[24].Text.Trim())) && 
                    {
                        var empMail = clsConfig.objEmpMailList.Where(x => x.EmpId == dr.Cells[2].Text).ToList();
                        if (empMail.Count == 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Style.Add("color", "red");                            
                            lblMsg.Text = ConfigErrorMessage.EMAILID_NOT_FOUND;
                            return;
                        }
                        MailMessage message = new MailMessage();                       
                        //message.To.Add(dr.Cells[24].Text.Trim());// Email-ID of Receiver pavanm@menlo-technologies.com ,akhtars,"harid@menlo-technologies.com"
                        message.To.Add(empMail[0].EmailId);
                        message.Subject = "PaySlip";// Subject of Email  
                        message.From = new System.Net.Mail.MailAddress("menlotechoffshore@gmail.com", "Menlo Payroll");// Email-ID of Sender                       
                        //message.From = new MailAddress("payroll@menlo-technologies.com", "Payroll Menlo-Technologies" );

                        message.IsBodyHtml = true;

                        MemoryStream file = new MemoryStream(objpdf.GeneratePDF(dr));
                        file.Seek(0, SeekOrigin.Begin);
                        Attachment data = new Attachment(file, dr.Cells[3].Text + ".pdf", "application/pdf");
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.DateTime.Now;
                        disposition.ModificationDate = System.DateTime.Now;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        message.Attachments.Add(data);//Attach the file                         
                        message.Body = "Please find pay slip as an attachment. For any clarification, do not reply back to this email. Contact finance@menlo-technologies.com.";
                        SmtpClient SmtpMail = new SmtpClient();
                        SmtpMail.Host ="smtp.gmail.com"; //"relay-hosting.secureserver.net";//"smtp.gmail.com";//"Your Host";//name or IP-Address of Host used for SMTP transactions  
                        //SmtpMail.Host ="smtpout.asia.secureserver.net";
                        SmtpMail.Port = 587;//Port for sending the mail 587 
                        //SmtpMail.Port = 465;//Port for sending the mail 587 
                        //SmtpMail.Host = "pod51018.outlook.com";//"Your Host";//name or IP-Address of Host used for SMTP transactions  
                        //SmtpMail.Port = 993;//Port for sending the mail  
                        SmtpMail.Credentials = new System.Net.NetworkCredential("menlotechoffshore@gmail.com", "menlo123");//username/password of network, if apply  
                        SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                        SmtpMail.EnableSsl = true;                       
                        //SmtpMail.UseDefaultCredentials = false;
                        SmtpMail.ServicePoint.MaxIdleTime = 1;
                        SmtpMail.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);                      
                        message.BodyEncoding = Encoding.Default;
                        message.Priority = MailPriority.Normal;
                        SmtpMail.Send(message); //Smtpclient to send the mail message  


                  //SmtpClient ss = new SmtpClient();
                  //ss.Host = "smtpout.secureserver.net";
                  //ss.Port = 465;
                  //ss.Timeout = 10000;
                  //ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                  //ss.UseDefaultCredentials = false;
                  //ss.Credentials = new System.Net.NetworkCredential("support@menlocloud.com", "Menlo@1234");
                  //ss.EnableSsl = true;

                  //MailMessage mailMsg = new MailMessage("testingaccount@comicidolonline.com", "youremail@youremail.com", "subject here", "my body");
                  //mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                  //ss.Send(mailMsg);


                        ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessMessage", "alert('Mail sent Successfully to --> " + dr.Cells[3].Text +"')", true);
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Style.Add("color", "red");
                        lblMsg.Text = ConfigErrorMessage.NO_DATA ;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Style.Add("color", "red");
                lblMsg.Text =ConfigErrorMessage.EMAIL_SEND_FAIL + " -- " + ex.Message;
            }
        }
        protected void ClearGridView(object sender, EventArgs e)
        {
            FileInfo f = new FileInfo(filePath);
            if (f.Exists)
            {
                f.IsReadOnly = false;
                f.Delete();
            }
            
            grvBatchUpload.DataSource = null;
            grvBatchUpload.DataBind();
            btnClear.Visible = false;
        }

        private void SendSMS(string MobileNo)
        {
            //SMS.APIType = SMSGateway.Site2SMS;
            //SMS.MashapeKey = "<Mashape API Key>";
            //SMS.Username = txtNumber.Text.Trim();
            //SMS.Password = txtPassword.Text.Trim();
            //if (txtRecipientNumber.Text.Trim().IndexOf(",") == -1)
            //{
            //    //Single SMS
            //    SMS.SendSms(txtRecipientNumber.Text.Trim(), txtMessage.Text.Trim());
            //}
            //else
            //{
            //    //Multiple SMS
            //    List<string> numbers = txtRecipientNumber.Text.Trim().Split(',').ToList();
            //    SMS.SendSms(numbers, txtMessage.Text.Trim());
            //}
        }

        protected void grvBatchUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string backColor = e.Row.BackColor.Name;
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.backgroundColor='yellow'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFF7E7'");
            }
        }
        private void SendMail()
        {
            //Imap oImap = new Imap();
        }
    }

     
        

}
