using System.ComponentModel.DataAnnotations;

namespace GameLibraryApi.Models
{
    public class GameType
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        // Navigation property
        public virtual ICollection<GameInformation> GameInformations { get; set; } = new List<GameInformation>();
    }
}