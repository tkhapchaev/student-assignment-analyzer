namespace KysectAcademyTask;

public class AuthorWhitelist
{
    private readonly List<string> _authors;

    public AuthorWhitelist(List<string> authors)
    {
        _authors = authors;
    }

    public bool Contains(string author)
    {
        if (_authors.Any(item => item == author))
        {
            return true;
        }

        return _authors.Count == 0;
    }
}