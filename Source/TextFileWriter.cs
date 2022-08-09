namespace KysectAcademyTask;

public class TextFileWriter : IWriter
{
    private readonly string _path;

    public TextFileWriter(string reportFilePath, string reportFileName)
    {
        _path = reportFilePath + Path.DirectorySeparatorChar + reportFileName + ".txt";
    }

    public void Write(List<ComparisonResult> contents)
    {
        foreach (ComparisonResult comparisonResult in contents)
        {
            File.AppendAllText(_path, comparisonResult.Result + Environment.NewLine);
        }
    }
}