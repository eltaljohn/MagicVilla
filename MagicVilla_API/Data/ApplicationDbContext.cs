using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
		{

		}

		public DbSet<Villa> Villas { get; set; }
		public DbSet<VillaNumber> VillaNumber { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Villa>().HasData(
				new Villa() {
					Id = 1,
					Name = "Villa Real",
					Detail = "Detalle de la villa..",
					ImageUrl = "",
					Occupants = 5,
					SquareMeters = 50,
					Price = 200,
					Amenity = "",
					CreationDate = DateTime.Now,
					UpdateDate = DateTime.Now
				},
                new Villa()
                {
                    Id = 2,
                    Name = "Premium vista a la piscina",
                    Detail = "Detalle de la villa..",
                    ImageUrl = "",
                    Occupants = 4,
                    SquareMeters = 40,
                    Price = 150,
                    Amenity = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
        }
    }
}

