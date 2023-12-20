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

    public static List<MovieScheduleInformation> FutureMoviesScheduled(List<MovieScheduleInformation> movieSchedules, DateTime currentTime)
    {
        List<MovieScheduleInformation> futuremovies = movieSchedules
        .Where(movie => {
            foreach (DateTime schedule in  movie.ScreeningTimeAndAuditorium.Keys)
            {
                if (schedule > currentTime) return true;
            }
            return false;
            }).ToList();
        return futuremovies;
    }

    // public static List<Film>
}
