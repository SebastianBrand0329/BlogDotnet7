using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

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
             var obj = _context.Articles.FirstOrDefault(a => a.Id == article.Id)!;
            obj.Name = article.Name;
            obj.Description = article.Description;
            obj.UrlImage = article.UrlImage;
            obj.DateCreation = article.DateCreation;
            obj.DateCreation = DateTime.UtcNow.ToString();
            _context.Update(obj);
            //_context.SaveChanges();
        }
    }
}
