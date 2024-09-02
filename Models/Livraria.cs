using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livraria
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Editora { get; set; }

        public int Estoque { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
}
