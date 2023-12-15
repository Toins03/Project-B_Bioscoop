public static class FilterMovies
{
    // interfaces kunnen geen static methods hebben :(
    public static List<Film> MoviesInTheFuture(List<Film> allMovies, TimeSpan GapOf2Hours, DateTime CurrentTime)
    {
        List<Film> MoviesPlayedInTheFuture = allMovies
                            .Where(movie => movie.DateAndAuditorium.Any(kv => kv.Key > CurrentTime.Add(GapOf2Hours)))
                            .ToList();
        return MoviesPlayedInTheFuture;
    }
}
