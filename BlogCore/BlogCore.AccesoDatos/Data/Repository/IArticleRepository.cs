using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface IArticleRepository : IRepository<Article>
    {
        void Update(Article article);
    }
}
