using System.ComponentModel.DataAnnotations;
//validação

namespace Agenda.ViewModels
{
    public class CreateContatoVM
    {
        [Required]
        public string? NomeCompleto { get; set; }

        [Required]
        [StringLength(11, MinimumLength =10)]
        //DEFINE  O NUMERO MAX DE CARACTERES
        public string? Telefone { get; set; }
        [Required]
        public DateTime DataDeNascimento { get; set; }

    }
    public class UpdateContatoVM
    {
        public string? NomeCompleto { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataDeNascimento { get; set; }

    }

}