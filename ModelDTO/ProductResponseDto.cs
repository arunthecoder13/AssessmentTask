using AssessmentTask.Models;
namespace AssessmentTask.ModelDTO
{
    public class ProductResponseDto : Response
    {
        public List<Product> Products { get; set; }
    }
}
