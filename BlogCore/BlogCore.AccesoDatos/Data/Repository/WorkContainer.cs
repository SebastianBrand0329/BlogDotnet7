using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _context;

        public WorkContainer(ApplicationDbContext context)
        {
            _context = context;
            categoryRepository = new CategoryRepository(_context);
        }

        public ICategoryRepository categoryRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
