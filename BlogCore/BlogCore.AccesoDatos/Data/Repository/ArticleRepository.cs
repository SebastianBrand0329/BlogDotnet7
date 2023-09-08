using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Article article)
        {
            var articleUpdate = _context.Articles.FirstOrDefault(a => a.Id == article.Id);
            //articleUpdate!.Name = article.Name;
            //articleUpdate.Description = article.Description;
            //articleUpdate.UrlImage = article.UrlImage;
            //articleUpdate.CategoriaId = article.CategoriaId;
            _context.Articles.Update(articleUpdate!);
            //_context.SaveChanges();
        }
    }
}
