using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }

    }
}
