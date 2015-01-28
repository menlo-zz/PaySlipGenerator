using System.Web.UI.WebControls;

namespace PaySlipGeneration
{
   public interface IGeneratePDF
    {
       byte[] GeneratePDF(GridViewRow gvrow);
    }
}
