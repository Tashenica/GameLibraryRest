using GameLibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace GameLibraryApi.Data
{
    public class GamingLibraryContext : DbContext
    {
        public DbSet<GameInformation> GameInformations { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AgeRestriction> AgeRestrictions { get; set; }
        
        
        public GamingLibraryContext(DbContextOptions<GamingLibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between GameInformation and GameType
            modelBuilder.Entity<GameInformation>()
                .HasOne(g => g.GameType)
                .WithMany(gt => gt.GameInformations)
                .HasForeignKey(g => g.GameTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between GameInformation and Genre
            modelBuilder.Entity<GameInformation>()
                .HasOne(g => g.Genre)
                .WithMany(gr => gr.GameInformations)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between GameInformation and AgeRestriction
            modelBuilder.Entity<GameInformation>()
                .HasOne(g => g.AgeRestriction)
                .WithMany(ar => ar.GameInformations)
                .HasForeignKey(g => g.AgeRestrictionId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public void SeedData()
        {
            // Seed GameTypes first (let EF auto-generate IDs)
            if (!GameTypes.Any())
            {
                var gameTypes = new List<GameType>
                {
                    new GameType { Name = "Nintendo Wii", Description = "Games for Nintendo Wii console" },
                    new GameType { Name = "Nintendo Switch", Description = "Games for Nintendo Switch console" },
                    new GameType { Name = "PlayStation 5", Description = "Games for Sony PlayStation 5 console" },
                    new GameType { Name = "Xbox Series X", Description = "Games for Microsoft Xbox Series X console" },
                    new GameType { Name = "PC", Description = "Games for Personal Computer" }
                };

                GameTypes.AddRange(gameTypes);
                SaveChanges();
            }

            // Seed Genres (let EF auto-generate IDs)
            if (!Genres.Any())
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Adventure", Description = "Adventure and exploration games" },
                    new Genre { Name = "Platformer", Description = "Platform jumping games" },
                    new Genre { Name = "Action", Description = "Fast-paced action games" },
                    new Genre { Name = "RPG", Description = "Role-playing games" },
                    new Genre { Name = "Strategy", Description = "Strategic thinking games" },
                    new Genre { Name = "Sports", Description = "Sports simulation games" },
                    new Genre { Name = "Racing", Description = "Car and bike racing games" },
                    new Genre { Name = "Shooter", Description = "First and third person shooter games" }
                };

                Genres.AddRange(genres);
                SaveChanges();
            }

            // Seed AgeRestrictions (let EF auto-generate IDs)
            if (!AgeRestrictions.Any())
            {
                var ageRestrictions = new List<AgeRestriction>
                {
                    new AgeRestriction { Code = "E", Description = "Everyone - Content suitable for all ages" },
                    new AgeRestriction { Code = "E10+", Description = "Everyone 10+ - Content suitable for ages 10 and older" },
                    new AgeRestriction { Code = "T", Description = "Teen - Content suitable for ages 13 and older" },
                    new AgeRestriction { Code = "M", Description = "Mature 17+ - Content suitable for ages 17 and older" },
                    new AgeRestriction { Code = "AO", Description = "Adults Only 18+ - Content suitable only for adults" },
                    new AgeRestriction { Code = "RP", Description = "Rating Pending - Not yet assigned a final rating" }
                };

                AgeRestrictions.AddRange(ageRestrictions);
                SaveChanges();
            }

            // Seed GameInformations with references to the created GameTypes, Genres, and AgeRestrictions
            if (!GameInformations.Any())
            {
                // Get the first few game types, genres, and age restrictions that were just created
                var wiiGameType = GameTypes.First(gt => gt.Name == "Nintendo Wii");
                var switchGameType = GameTypes.First(gt => gt.Name == "Nintendo Switch");
                var adventureGenre = Genres.First(g => g.Name == "Adventure");
                var platformerGenre = Genres.First(g => g.Name == "Platformer");
                var e10PlusRating = AgeRestrictions.First(ar => ar.Code == "E10+");
                var everyoneRating = AgeRestrictions.First(ar => ar.Code == "E");

                var gameInformations = new List<GameInformation>
                {
                    new GameInformation
                    {
                        Title = "The Legend of Zelda: Breath of the Wild",
                        GameTypeId = wiiGameType.Id,
                        GenreId = adventureGenre.Id,
                        AgeRestrictionId = e10PlusRating.Id,
                        CompanyName = "Nintendo",
                        Multiplayer = false,
                        Description = "An open-world action-adventure game set in the kingdom of Hyrule.",
                        Image = "zelda.png",
                        YearPublished = new DateTime(2017, 3, 3)
                    },
                    new GameInformation
                    {
                        Title = "Super Mario Odyssey",
                        GameTypeId = switchGameType.Id,
                        GenreId = platformerGenre.Id,
                        AgeRestrictionId = everyoneRating.Id,
                        CompanyName = "Nintendo",
                        Multiplayer = false,
                        Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach.",
                        Image = "mario.png",
                        YearPublished = new DateTime(2017, 10, 27)
                    }
                };

                GameInformations.AddRange(gameInformations);
                SaveChanges();
            }
        }
    }
}
