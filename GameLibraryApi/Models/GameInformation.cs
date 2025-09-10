using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibraryApi.Models
{
    public class GameInformation
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        // Foreign key for GameType
        public int GameTypeId { get; set; }
        
        // Navigation property
        [ForeignKey("GameTypeId")]
        public virtual GameType? GameType { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        // Foreign key for Genre
        public int GenreId { get; set; }
        
        // Navigation property
        [ForeignKey("GenreId")]
        public virtual Genre? Genre { get; set; }

        // Foreign key for AgeRestriction
        public int AgeRestrictionId { get; set; }
        
        // Navigation property
        [ForeignKey("AgeRestrictionId")]
        public virtual AgeRestriction? AgeRestriction { get; set; }

        public bool Multiplayer { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public DateTime YearPublished { get; set; }

    }
}
