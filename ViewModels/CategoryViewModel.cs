using System.ComponentModel.DataAnnotations;
namespace MinhaLoja.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
