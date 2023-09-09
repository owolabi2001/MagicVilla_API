using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa() {
                    Id=1,
                    Name = "Owolabi Temi Olamdie Villa",
                    Details = "This is Shola's Villa",
                    Occupancy =2,
                    Rate=3,
                    ImageUrl= "dkdjlfjsldkjlakjflsdjf.com",
                    Sqft=4550,
                    Amenity= "",
                    CreatedDate= DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Shola's",
                    Details= "This is Shola's Villa",
                    Occupancy = 2,
                    Rate = 3,
                    ImageUrl = "dkdjlfjsldkjlakjflsdjf.com",
                    Sqft = 950950,
                    Amenity = "",
                    CreatedDate= DateTime.Now,
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Johson's Villa",
                    Details = "This is Shola's Villa",
                    Occupancy = 2,
                    Rate = 3,
                    ImageUrl = "dkdjlfjsldkjlakjflsdjf.com",
                    Sqft = 55880,
                    Amenity = "",
                    CreatedDate= DateTime.Now,
                }
                ) ;

        }
    }
}
