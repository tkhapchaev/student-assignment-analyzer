namespace KysectAcademyTask;

public class ConsoleWriter : IWriter
{
    public void Write(List<ComparisonResult> contents)
    {
        foreach (ComparisonResult comparisonResult in contents)
        {
            Console.WriteLine(comparisonResult.Result);
        }
    }
}