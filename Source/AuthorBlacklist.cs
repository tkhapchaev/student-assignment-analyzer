namespace KysectAcademyTask;

public class AuthorBlacklist
{
    private readonly List<string> _authors;

    public AuthorBlacklist(List<string> authors)
    {
        _authors = authors;
    }

    public bool Contains(string author)
    {
        return _authors.Any(item => item == author);
    }
}