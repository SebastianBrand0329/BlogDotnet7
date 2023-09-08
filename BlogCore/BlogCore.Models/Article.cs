using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Nombre del Artículo")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name ="Descripción del Artículo")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Description{ get; set; }

        [Display(Name ="Fecha de Creación")]
        public string DateCreation { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Imagen")]
        public string UrlImage { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Category Category { get; set; }
    }
}
