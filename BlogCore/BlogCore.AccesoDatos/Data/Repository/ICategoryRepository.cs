using BlogCore.Models;
using System.Web.Mvc;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetListCategory();

        void Update(Category category);
    }
}
