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
            articleRepository = new ArticleRepository(_context);   
            sliderRepository = new SliderRepository(_context);
            userRepository = new UserRepository(_context);   
        }

        public ICategoryRepository categoryRepository { get; private set; }

        public IArticleRepository articleRepository { get; private set; }

        public ISliderRepository sliderRepository { get; private set; }

        public IUserRepository userRepository { get; private set; } 

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
