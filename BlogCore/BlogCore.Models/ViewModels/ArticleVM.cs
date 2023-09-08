using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.Models.ViewModels
{
    public class ArticleVM
    {
        public Article Article { get; set; }
        public IEnumerable<SelectListItem> ListArticles { get; set; }
    }
}
