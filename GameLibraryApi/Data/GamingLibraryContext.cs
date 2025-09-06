using GameLibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace GameLibraryApi.Data
{
    public class GamingLibraryContext : DbContext
    {
        public DbSet<GameInformation> GameInformations { get; set; }
       
        
        
        public GamingLibraryContext(DbContextOptions<GamingLibraryContext> options) : base(options)
        {
        }


        public void SeedData()
        {
            if (!GameInformations.Any())
            {

                this.GameInformations.Add(new GameInformation
                {

                    Title = "The Legend of Zelda: Breath of the Wild",
                    GameType = "Nintendo Wii",
                    CompanyName = "Nintendo",
                    Genre = "Adventure",
                    AgeRestriction = "E10+",
                    Multiplayer = false,
                    Description = "An open-world action-adventure game set in the kingdom of Hyrule.",
                    Image = "zelda.png",
                    YearPublished = new DateTime(2017, 3, 3)
                });

                GameInformations.Add(new GameInformation
                {
                    Title = "Super Mario Odyssey",
                    GameType = "Nintendo Switch",
                    CompanyName = "Nintendo",
                    Genre = "Platformer",
                    AgeRestriction = "E",
                    Multiplayer = false,
                    Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach.",
                    Image = "mario.png",
                    YearPublished = new DateTime(2017, 10, 27)
                });

                this.SaveChanges();
            }
        }
    }
}
