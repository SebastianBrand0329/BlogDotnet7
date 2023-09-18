using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _context;

        public SliderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Slider slider)
        {
            var sliderId = _context.Sliders.FirstOrDefault(s => s.Id == slider.Id)!;
            sliderId.Name = slider.Name;
            sliderId.State = slider.State;
            sliderId.UrlImage = slider.UrlImage;    
            _context.Sliders.Update(sliderId!);
            _context.SaveChanges(); 
        }
    }
}
