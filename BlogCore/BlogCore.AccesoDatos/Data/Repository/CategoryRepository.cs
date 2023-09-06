using BlogCore.Data;
using BlogCore.Models;
using System.Web.Mvc;

namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListCategory()
        {
            return _context.Categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
        }

        public void Update(Category category)
        {
            var objec = _context.Categories.FirstOrDefault(c => c.Id == category.Id)!;
            objec.Name = category.Name;
            objec.Order = category.Order;
            _context.SaveChanges();
        }
    }
}
