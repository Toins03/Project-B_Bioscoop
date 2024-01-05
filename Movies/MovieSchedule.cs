using System.Net;
using System.Net.Security;
using Newtonsoft.Json;

public class MovieScheduleInformation: IEquatable<MovieScheduleInformation>
{
    public string? Title { get; set; }
    public Dictionary<DateTime, List<List<string>>> ScreeningTimeAndAuditorium { get; set; } = new Dictionary<DateTime, List<List<string>>>();
    public List<string> ReservationsList { get; set; } = new List<string>();

    public static string FileSaved = "MovieScheduleInformation.json";

    [JsonConstructor]
    public MovieScheduleInformation(string title, Dictionary<DateTime, List<List<string>>> ScreeningTimeAndAuditorium, List<string> reservationList)
    {
        this.Title = title;
        this.ScreeningTimeAndAuditorium = ScreeningTimeAndAuditorium;
        this.ReservationsList = reservationList;
    }

    public MovieScheduleInformation(string title, Dictionary<DateTime, List<List<string>>> ScreeningTimeAndAuditorium)
    {
        this.Title = title;
        this.ScreeningTimeAndAuditorium = ScreeningTimeAndAuditorium;
    }

    public static void AddNewMovieScheduleInfo(MovieScheduleInformation AddToJson, DateTime date)
    {
        List<MovieScheduleInformation> ExistingData = ReadDataFromJson()!;
        if (File.Exists(FileSaved))
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileSaved))
                {
                    bool isTitleInSchedule = false;
                    foreach (MovieScheduleInformation movie in ExistingData)
                    {
                        if (movie.Title == AddToJson.Title)
                        {
                            movie.ScreeningTimeAndAuditorium[date] = AddToJson.ScreeningTimeAndAuditorium[date];
                            isTitleInSchedule = true;
                        }
                    }
                    if (!isTitleInSchedule)
                    {
                        ExistingData.Add(AddToJson);
                    }
                    string List2Json = JsonConvert.SerializeObject(ExistingData, Formatting.Indented);
                    writer.Write(List2Json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }    
    }


    public static void UpdateJsonFile(MovieScheduleInformation toUpdate, DateTime date, List<List<string>> auditorium)
    {

        List<MovieScheduleInformation> ExistingData = ReadDataFromJson()!;
        if (File.Exists(FileSaved))
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileSaved))
                {
                    bool isTitleInSchedule = false;
                    foreach (MovieScheduleInformation movie in ExistingData)
                    {
                        if (movie.Title == toUpdate.Title)
                        {
                            movie.ScreeningTimeAndAuditorium[date] = toUpdate.ScreeningTimeAndAuditorium[date];

                            isTitleInSchedule = true;
                        }
                    }
                    if (!isTitleInSchedule)
                    {
                        ExistingData.Add(toUpdate);
                    }
                    string List2Json = JsonConvert.SerializeObject(ExistingData, Formatting.Indented);
                    writer.Write(List2Json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }

    }

    public void AddTitleAndScreeningTimeAndAuditorium(DateTime Date, List<List<string>> Auditorium, string ConfirmationCode)
    {
        ScreeningTimeAndAuditorium[Date] = Auditorium;
        
        WriteDataToJson(this, ConfirmationCode, Date);
    }

    private static void WriteDataToJson(MovieScheduleInformation AddToJson, string ConfirmationCode, DateTime date)
    {
        List<MovieScheduleInformation> ExistingData = ReadDataFromJson()!;
        if (File.Exists(FileSaved))
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileSaved))
                {
                    bool isTitleInSchedule = false;
                    foreach (MovieScheduleInformation movie in ExistingData)
                    {
                        if (movie.Title == AddToJson.Title)
                        {
                            movie.ScreeningTimeAndAuditorium[date] = AddToJson.ScreeningTimeAndAuditorium[date];
                            if (ConfirmationCode.Contains('-'))
                                movie.ReservationsList.Add(ConfirmationCode);
                            isTitleInSchedule = true;
                        }
                    }
                    if (!isTitleInSchedule)
                    {
                        AddToJson.ReservationsList.Add(ConfirmationCode);
                        ExistingData.Add(AddToJson);
                    }
                    string List2Json = JsonConvert.SerializeObject(ExistingData, Formatting.Indented);
                    writer.Write(List2Json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }
    }

    public static List<MovieScheduleInformation>? ReadDataFromJson()
    {
        if (File.Exists(FileSaved))
        {
            try
            {
                using (StreamReader reader = new StreamReader(FileSaved))
                {
                    string json = reader.ReadToEnd();
                    List<MovieScheduleInformation> ExistingData = JsonConvert.DeserializeObject<List<MovieScheduleInformation>>(json)!;
                    if (ExistingData == null)
                    {
                        ExistingData = new List<MovieScheduleInformation>();
                    }
                    return ExistingData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }
        return null;
    }
    public static List<MovieScheduleInformation>? ToList()
    {
        if (File.Exists(FileSaved))
        {
            try
            {
                using (StreamReader reader = new StreamReader(FileSaved))
                {
                    string json = reader.ReadToEnd();
                    List<MovieScheduleInformation> ExistingData = JsonConvert.DeserializeObject<List<MovieScheduleInformation>>(json)!;
                    if (ExistingData == null)
                    {
                        ExistingData = new List<MovieScheduleInformation>();
                    }
                    return ExistingData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }
        return null;
    }
    public static void RemoveMovieScheduleObject(string title)
    {
        List<MovieScheduleInformation>? existingData = ReadDataFromJson();

        if (existingData != null && existingData.Count > 0)
        {
            MovieScheduleInformation movieToRemove = existingData.FirstOrDefault(movie => movie.Title == title);

            if (movieToRemove != null)
            {
                existingData.Remove(movieToRemove);

                // Update the JSON file
                UpdateJsonFile(existingData);
            }
            else
            {
                Console.WriteLine($"Movie with title '{title}' not found.");
            }
        }
    }
    private static void UpdateJsonFile(List<MovieScheduleInformation> data)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FileSaved))
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                writer.Write(jsonData);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating JSON file: {ex.Message}");
        }
    }

    public static MovieScheduleInformation? findMovieScheduleInfoByTitle(string ToSearchFor)
    {
        List<MovieScheduleInformation>? existingData = ReadDataFromJson();
        if (existingData is null) return null;
        else
        {
            List<MovieScheduleInformation> found = existingData!.Where(film => film.Title == ToSearchFor).ToList();
            if (found is not null && found.Count > 0) return found[0];
            else return null;
        }

    }

    public override bool Equals(object? obj)
    {
        if (obj is MovieScheduleInformation) return this.Equals((MovieScheduleInformation) obj);
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(MovieScheduleInformation? othermovie)
    {
        if (this is null && othermovie is null) return true;
        else if (this is null || othermovie is null) return false;
        else if (this.Title == othermovie.Title) return true;
        else return false;
    }

    public static bool operator ==(MovieScheduleInformation? info1, MovieScheduleInformation? info2)
    {
        if (info1 is null && info2 is null) return true;
        else if (info1 is null || info2 is null) return false;
        else return info1.Equals(info2);
    }

    public static bool operator !=(MovieScheduleInformation? info1, MovieScheduleInformation? info2)
    {
        return !(info1 == info2);
    }
}