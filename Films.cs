namespace Videoclub
{
    internal class Films
    {
        public int FilmId { get; set; }
        public string Title { get; set; }

        public string Plot { get; set; }

        public int AgeRate { get; set; }

        public char Rented { get; set; }

        public Films(int filmId, string title, string plot, int ageRate, char rented)
        {
            FilmId = filmId;
            Title = title;
            Plot = plot;
            AgeRate = ageRate;
            Rented = rented;
        }

        
    }
}
