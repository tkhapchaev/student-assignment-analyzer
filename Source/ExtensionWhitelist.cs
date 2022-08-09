namespace KysectAcademyTask;

public class ExtensionWhitelist
{
    private readonly List<string> _extensions;

    public ExtensionWhitelist(List<string> extensions)
    {
        _extensions = extensions;
    }

    public bool Contains(string extension)
    {
        if (_extensions.Any(item => "." + item == extension))
        {
            return true;
        }

        return _extensions.Count == 0;
    }
}