using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;

namespace PaySlipGeneration
{
    public class clsConfig
    {
        XmlDocument xmlDoc;
        string filename = ConfigurationManager.AppSettings["XMLPATH"].ToString(); //ConfigErrorMessage.CONFIG_FILE_PATH;  //@"~/XMLFile1.xml";E:\MyProject\PaySlipGeneration\PaySlipGeneration\bin\XMLFile1.xml
     
        public static List<clsMailId> objEmpMailList = new List<clsMailId>();
        public static List<clsMailId> objTrainMailList = new List<clsMailId>();
        clsMailId objMail;
        public  string ReadConfigFile()
        {
            
            if (filename.Equals(""))
            {
                return ConfigErrorMessage.CONF_FILE_NOT_FOUND; 
            }
            XmlDocument xmlDoc;
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);
            }
            catch (Exception ex)
            {
                return ConfigErrorMessage.BAD_CONFIG_FILE; 
            }
            try
            {
                if (xmlDoc.SelectNodes("/PaySlip/Employee/EmpRec").Count == 0)
                {
                    return ConfigErrorMessage.BAD_CONFIG_FILE;
                }
                if (xmlDoc.SelectNodes("/PaySlip/Trainee/TrainRec").Count == 0)
                {
                    return ConfigErrorMessage.BAD_CONFIG_FILE;
                }
            }
            catch (Exception ex)
            {
                return ConfigErrorMessage.BAD_CONFIG_FILE; 
            }

            if (xmlDoc.DocumentElement.ChildNodes == null)
                return ConfigErrorMessage.BAD_CONFIG_FILE; 
            try
            {
                // iterate through top-level elements
                foreach (XmlNode itemNode in xmlDoc.DocumentElement.ChildNodes)
                {
                    // because we know that the node is an element, we can do this:
                    XmlElement itemElement = (XmlElement)itemNode;

                    if (itemNode.Name.ToUpper() == "EMPLOYEE")
                    {
                        if (itemNode.ChildNodes.Count == 0)
                        {
                            return ConfigErrorMessage.EMAILID_NOT_FOUND; 
                        }
                    }
                    else
                    {
                        if (itemNode.Name.ToUpper() == "TRAINEE")
                        {
                            if (itemNode.ChildNodes.Count == 0)
                            {
                                return ConfigErrorMessage.EMAILID_NOT_FOUND; 
                            }
                        }
                    }

                    foreach (XmlNode childNode in itemNode.ChildNodes)
                    {
                        String RootNode = childNode.Name.ToUpper();
                        objMail = new clsMailId();

                        if (childNode.Attributes["EmpID"] == null || childNode.Attributes["MailID"] == null)
                        {
                            return ConfigErrorMessage.EMAILID_NOT_FOUND; 
                        }
                        objMail.EmpId = childNode.Attributes["EmpID"] != null ? childNode.Attributes["EmpID"].Value : "";
                        objMail.EmailId = childNode.Attributes["MailID"] != null ? childNode.Attributes["MailID"].Value : "";
                        if (RootNode == "EMPREC")
                        {
                            objEmpMailList.Add(objMail);
                        }
                        else if (RootNode == "TRAINREC")
                        {
                            objTrainMailList.Add(objMail);
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                return  ConfigErrorMessage.BAD_CONFIG_FILE;
            }
            return ConfigErrorMessage.SUCCESS;
        }
    }
}