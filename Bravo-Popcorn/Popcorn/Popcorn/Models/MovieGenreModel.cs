namespace Popcorn.Models
{
    public class MovieGenreModel
    {
        public int Id { get; set; }
        public int MovieModelId { get; set; }
        public MovieModel MovieModel { get; set; }
        public int GenreModelId { get; set; }
        public GenreModel GenreModel { get; set; }
    }
}
