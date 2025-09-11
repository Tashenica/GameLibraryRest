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

        // Image storage - store actual image data
        public byte[]? ImageData { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageContentType { get; set; }
        
        // Keep the Image property for backwards compatibility (can be used for display names)
        public string Image { get; set; } = string.Empty;

        public DateTime YearPublished { get; set; }

    }
}
