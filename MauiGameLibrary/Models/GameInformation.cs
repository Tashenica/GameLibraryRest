using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGameLibrary.Models
{
    public class GameInformation
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        // Foreign key for GameType
        public int GameTypeId { get; set; }
        
        // Navigation property
        public virtual GameType? GameType { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        // Foreign key for Genre
        public int GenreId { get; set; }
        
        // Navigation property
        public virtual Genre? Genre { get; set; }

        // Foreign key for AgeRestriction
        public int AgeRestrictionId { get; set; }
        
        // Navigation property
        public virtual AgeRestriction? AgeRestriction { get; set; }

        public bool Multiplayer { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        // Image storage properties
        public byte[]? ImageData { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageContentType { get; set; }

        public DateTime YearPublished { get; set; }
    }
}
