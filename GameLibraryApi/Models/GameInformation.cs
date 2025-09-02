namespace GameLibraryApi.Models
{
    public class GameInformation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string GameType { get; set; }

        public string CompanyName { get; set; }

        public string Genre { get; set; }

        public string AgeRestriction { get; set; }

        public bool Multiplayer { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime YearPublished { get; set; }

    }
}
