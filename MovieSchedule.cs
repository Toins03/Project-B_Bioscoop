using Newtonsoft.Json;

public class MovieScheduleInformation
{
    public string? Title { get; set; }
    public Dictionary<string, List<List<string>>> ScreeningTimeAndAuditorium { get; set; } = new Dictionary<string, List<List<string>>>();
    public List<string> ReservationsList { get; set; } = new List<string>();

    public void AddTitleAndScreeningTimeAndAuditorium(string AddTitle, string Date, List<List<string>> Auditorium, string ConfirmationCode)
    {
        ScreeningTimeAndAuditorium.Add(Date, Auditorium);
        List<MovieScheduleInformation> AddToJson = new List<MovieScheduleInformation>
        {
            new MovieScheduleInformation
            {
                Title = AddTitle,
                ScreeningTimeAndAuditorium = new Dictionary<string, List<List<string>>>(ScreeningTimeAndAuditorium),
                ReservationsList = new List<string>(ReservationsList)
            }
        };

        WriteDataFromJsin(AddToJson, ConfirmationCode);
    }

    private void WriteDataFromJsin(List<MovieScheduleInformation> AddToJson, string ConfirmationCode)
    {
        List<MovieScheduleInformation> ExistingData = ReadDataFromJson()!;
        if (File.Exists("MovieScheduleInformation.json"))
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("MovieScheduleInformation.json"))
                {
                    foreach (MovieScheduleInformation movie in ExistingData)
                    {
                        if (movie.Title == AddToJson[0].Title)
                        {
                            movie.ScreeningTimeAndAuditorium["11-11-2023"] = AddToJson[0].ScreeningTimeAndAuditorium["11-11-2023"];
                            if (ConfirmationCode.Contains(':'))
                                movie.ReservationsList.Add(ConfirmationCode);
                            string List3Json = JsonConvert.SerializeObject(ExistingData, Formatting.Indented);
                            writer.Write(List3Json);
                            return;
                        }
                    }



                    foreach (MovieScheduleInformation movieSchedule in AddToJson)
                    {
                        movieSchedule.ReservationsList.Add(ConfirmationCode);
                        ExistingData.Add(movieSchedule);

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

    private List<MovieScheduleInformation>? ReadDataFromJson()
    {
        if (File.Exists("MovieScheduleInformation.json"))
        {
            try
            {
                using (StreamReader reader = new StreamReader("MovieScheduleInformation.json"))
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


    public void AddReservation(string Confirmationcode)
    {
        if (Confirmationcode.Contains(':'))
            ReservationsList.Add(Confirmationcode);
    }
}