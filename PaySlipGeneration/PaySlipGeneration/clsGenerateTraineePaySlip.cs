using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Web.UI.WebControls;

namespace PaySlipGeneration
{
    public class clsGenerateTraineePaySlip : IGeneratePDF
    {

        public byte[] GeneratePDF(GridViewRow dr)
        {
            double totGE = 0;
            double totTD = 0;
            double totNP = 0;

            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                //Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;
                //document.NewPage();
                document.Open();

                //Header Table
                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.7f });

                //Company Logo
                cell = clsPDFWriter.ImageCell("~/Images/Menlologo5.png", 60f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);


                //Separater Line
                color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                clsPDFWriter.DrawLine(writer, 25f, document.Top - 50f, document.PageSize.Width - 25f, document.Top - 50f, color);
                //clsPDFWriter.DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
                document.Add(table);


                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 50f, 117f });
                cell = new PdfPCell(new Phrase("Pay slip for the month of :", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                //cell.Colspan = 2;
                table.SpacingBefore = 40f;
                table.SpacingAfter = 1f;

                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[4].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 50f, 117f });
                cell = new PdfPCell(new Phrase("Name of trainee employee :", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                //cell.Colspan = 2;
                table.SpacingBefore = 3f;
                table.SpacingAfter = 1f;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[3].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                cell = new PdfPCell(new Phrase("Trainee Employee ID :", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                //cell.Colspan = 2;
                //table.SpacingBefore = 3f;
                //table.SpacingAfter = 1f;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[2].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Work days : ", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[25].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                document.Add(table);


                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                cell = new PdfPCell(new Phrase("Vacation balance days:", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                //cell.Colspan = 2;
                //table.SpacingBefore = 3f;
                table.SpacingAfter = 3f;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[30].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Paid days: ", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[26].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                document.Add(table);


                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 100f, 100f });
                cell = new PdfPCell(new Phrase("Gross Earnings Summary", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Taxes & Deductions ", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
                document.Add(table);


                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                cell = new PdfPCell(new Phrase("Description", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("This Period(Rs.) ", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Description", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("This Period(Rs.) ", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                cell = new PdfPCell(new Phrase("Stipend", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(dr.Cells[5].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                if (!string.IsNullOrEmpty(dr.Cells[5].Text.Trim()))
                {
                    totGE += Convert.ToDouble(dr.Cells[5].Text);
                }

                cell = new PdfPCell(new Phrase("ESI", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[17].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                document.Add(table);
                if (!string.IsNullOrEmpty(dr.Cells[17].Text.Trim()))
                {
                    totTD += Convert.ToDouble(dr.Cells[17].Text);
                }

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                cell = new PdfPCell(new Phrase("Bonus", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[14].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                if (!string.IsNullOrEmpty(dr.Cells[14].Text.Trim()))
                {
                    totGE += Convert.ToDouble(dr.Cells[14].Text);
                }

                cell = new PdfPCell(new Phrase("Provident Fund (PF)", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(dr.Cells[18].Text, new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                document.Add(table);
                if (!string.IsNullOrEmpty(dr.Cells[18].Text.Trim()))
                {
                    totTD += Convert.ToDouble(dr.Cells[18].Text);
                }

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                table.SpacingAfter = 5f;
                cell = new PdfPCell(new Phrase("Total Gross Earnings (A)", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);
                totNP = totGE - totTD;
                //cell = new PdfPCell(new Phrase(totGE.ToString(), new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell = new PdfPCell(new Phrase(dr.Cells[15].Text, new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total Taxes & Deductions (B)", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                table.AddCell(cell);

                //cell = new PdfPCell(new Phrase(totTD.ToString(), new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell = new PdfPCell(new Phrase(dr.Cells[23].Text, new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 60f, 40f, 60f, 40f });
                cell = new PdfPCell(new Phrase("Net Pay(Rs.) = (A) - (B)", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.Colspan = 3;
                table.AddCell(cell);

                //cell = new PdfPCell(new Phrase(totNP.ToString(), new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell = new PdfPCell(new Phrase(dr.Cells[24].Text, new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);


                document.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 200f });
                table.SpacingBefore = 40f;
                cell = new PdfPCell(new Phrase("This is a computer generated pay slip.  Hence, no signature is required.", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.BorderColor = Color.WHITE;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 200f });
                table.SpacingBefore = 450f;
                cell = new PdfPCell(new Phrase("Menlo Technologies India Private Limited", new Font(Font.HELVETICA, 10f, Font.BOLD, Color.BLACK)));
                cell.BorderColor = Color.WHITE;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 200f });
                table.SpacingBefore = 2f;
                cell = new PdfPCell(new Phrase("# 103, Aditya Trade Center, Aditya Enclave Road, Ameerpet, Hyderabad - 500 038, INDIA", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.BorderColor = Color.WHITE;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 200f });
                table.SpacingBefore = 3f;
                cell = new PdfPCell(new Phrase("Ph: (91) (40) 6555 4067 / 6649; Web: www.menlo-technologies.com", new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK)));
                cell.BorderColor = Color.WHITE;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
                document.Add(table);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;
            }
        }
    }
}