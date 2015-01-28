
namespace PaySlipGeneration
{
    public class clsPaySlipFactory
    {
        public IGeneratePDF GetPaySlipObject( string type)
        {
            IGeneratePDF objPdf;
            if(type.Trim()=="E")
            {
                return objPdf = new clsGenerateEmployeePaySlip();
            }
            else if(type.Trim()=="T")
            {
                return objPdf = new clsGenerateTraineePaySlip();
            }
            else
            {
                return objPdf = new clsGenerateTraineePaySlip();
            }
        }
    }
}