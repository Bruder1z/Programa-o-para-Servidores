using System.ComponentModel.DataAnnotations;

namespace MinhaLoja.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Value { get; set; }
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
