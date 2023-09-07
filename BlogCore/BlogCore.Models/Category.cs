using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Categoría")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Orden")]
        //[Required(ErrorMessage ="El campo {0} es obligatorio")]
        public int Order { get; set; }
    }
}
