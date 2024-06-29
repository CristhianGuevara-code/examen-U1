using System.ComponentModel.DataAnnotations;

namespace ExamenPOOU1.Dtos.Categories
{
    public class CategoryCreateDto
    {
        [Display(Name = "Descripcion")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres")]
        public string Description { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; }

        
    }
}
