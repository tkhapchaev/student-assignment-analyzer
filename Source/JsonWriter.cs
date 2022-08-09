using System.Text.Json;

namespace KysectAcademyTask;

public class JsonWriter : IWriter
{
    private readonly string _path;

    public JsonWriter(string reportFilePath, string reportFileName)
    {
        _path = reportFilePath + Path.DirectorySeparatorChar + reportFileName + ".json";
    }

    public void Write(List<ComparisonResult> contents)
    {
        foreach (ComparisonResult comparisonResult in contents)
        {
            File.AppendAllText(_path, JsonSerializer.Serialize(comparisonResult));
        }
    }
}