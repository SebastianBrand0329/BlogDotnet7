using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
