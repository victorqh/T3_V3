using System.ComponentModel.DataAnnotations;

namespace T3_VQUISPEH.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(150, ErrorMessage = "El autor no puede exceder 150 caracteres")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "El tema es obligatorio")]
        [StringLength(100, ErrorMessage = "El tema no puede exceder 100 caracteres")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "La editorial es obligatoria")]
        [StringLength(100, ErrorMessage = "La editorial no puede exceder 100 caracteres")]
        public string Editorial { get; set; }

        [Required(ErrorMessage = "El año de publicación es obligatorio")]
        [Range(1900, 3000, ErrorMessage = "El año de publicación debe estar entre 1900 y 3000")]
        public int AnioPublicacion { get; set; }

        [Required(ErrorMessage = "El número de páginas es obligatorio")]
        [Range(10, 1000, ErrorMessage = "El número de páginas debe estar entre 10 y 1000")]
        public int Paginas { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "El material es obligatorio")]
        [StringLength(50, ErrorMessage = "El material no puede exceder 50 caracteres")]
        public string Material { get; set; }

        [Required(ErrorMessage = "El número de copias es obligatorio")]
        [Range(1, 20, ErrorMessage = "El número de copias debe estar entre 1 y 20")]
        public int Copias { get; set; }
    }
}
