using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetListCategory();

        void Update(Category category);
    }
}
