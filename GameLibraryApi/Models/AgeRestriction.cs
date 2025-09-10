namespace GameLibraryApi.Models
{
    public class AgeRestriction
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Navigation property
        public virtual ICollection<GameInformation> GameInformations { get; set; } = new List<GameInformation>();
    }
}