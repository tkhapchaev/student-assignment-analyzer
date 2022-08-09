namespace KysectAcademyTask;

public class Submission
{
    public string Group { get; }

    public string Author { get; }

    public string Assignment { get; }

    public SubmissionDate SubmissionDate { get; }

    public string File { get; }

    public Submission(string group, string author, string assignment, SubmissionDate submissionDate, string file)
    {
        Group = group;
        Author = author;
        Assignment = assignment;
        SubmissionDate = submissionDate;
        File = file;
    }

    public Submission(string group, string author, string assignment, string file)
    {
        Group = group;
        Author = author;
        Assignment = assignment;
        SubmissionDate = new SubmissionDate("UNDATED");
        File = file;
    }
}