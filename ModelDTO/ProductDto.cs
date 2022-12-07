using System.ComponentModel.DataAnnotations;

namespace AssessmentTask.ModelDTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
