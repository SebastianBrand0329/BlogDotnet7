using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void BlockUser(string IdUser);

        void UnlockUser(string IdUser);
    }
}
