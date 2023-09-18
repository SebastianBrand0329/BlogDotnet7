using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
   
        [Display(Name = "Estado")]
        public bool State { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImage { get; set; }
    }
}
