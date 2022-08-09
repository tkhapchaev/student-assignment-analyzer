namespace KysectAcademyTask;

public class SubmissionDate
{
    public DateTime ConvertedDate { get; }

    public string RawDate { get; }

    public SubmissionDate(string date)
    {
        RawDate = date;
        DateTime.TryParseExact(date, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime convertedDate);

        ConvertedDate = convertedDate;
    }
}