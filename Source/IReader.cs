namespace KysectAcademyTask;

public interface IReader
{
    List<Submission> Read(string inputDirectoryPath);
}