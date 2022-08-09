namespace KysectAcademyTask;

public class DirectoryBlacklist
{
    private readonly List<string> _directories;

    public DirectoryBlacklist(List<string> directories)
    {
        _directories = directories;
    }

    public bool Contains(string directory)
    {
        return _directories.Any(item => item == directory);
    }
}