namespace KysectAcademyTask;

public class Executor
{
    private readonly string _inputDirectoryPath;

    private readonly List<IComparator> _comparator;

    private readonly IWriter _writer;

    private readonly List<string> _extensionWhitelist;

    private readonly List<string> _directoryBlacklist;

    private readonly List<string> _authorWhitelist;

    private readonly List<string> _authorBlacklist;

    public Executor(string inputDirectoryPath, List<IComparator> comparator, IWriter writer,
        List<string> extensionWhitelist,
        List<string> directoryBlacklist,
        List<string> authorWhitelist, List<string> authorBlacklist
    )
    {
        _inputDirectoryPath = inputDirectoryPath;
        _comparator = comparator;
        _writer = writer;
        _extensionWhitelist = extensionWhitelist;
        _directoryBlacklist = directoryBlacklist;
        _authorWhitelist = authorWhitelist;
        _authorBlacklist = authorBlacklist;
    }

    public void Analyze()
    {
        var directoryReader =
            new DirectoryReader(_authorWhitelist, _authorBlacklist, _extensionWhitelist, _directoryBlacklist);

        List<Submission> submissions = directoryReader.Read(_inputDirectoryPath);
        var assignments = new HashSet<string>();
        var result = new List<ComparisonResult>();

        foreach (Submission submission in submissions)
        {
            assignments.Add(submission.Assignment);
        }

        var progressTracker = new ProgressTracker(submissions, assignments);
        int iterationCounter = 0;

        foreach (List<Submission> submissionsToCompare in assignments.Select(assignment =>
                     submissions.Where(submission => submission.Assignment == assignment).ToList()))
        {
            for (int i = 0; i < submissionsToCompare.Count; ++i)
            {
                for (int j = i + 1; j < submissionsToCompare.Count; ++j)
                {
                    string author1 = submissionsToCompare[i].Author,
                        author2 = submissionsToCompare[j].Author,
                        assignment1 = submissionsToCompare[i].Assignment,
                        assignment2 = submissionsToCompare[j].Assignment,
                        path1 = submissionsToCompare[i].File,
                        path2 = submissionsToCompare[j].File,
                        file1 = Path.GetFileName(path1),
                        file2 = Path.GetFileName(path2),
                        extension1 = Path.GetExtension(file1),
                        extension2 = Path.GetExtension(file2);

                    if (author1 == author2 || extension1 != extension2 || assignment1 != assignment2)
                    {
                        break;
                    }

                    var similarityPercentages =
                        _comparator.Select(comparator => comparator.CompareFiles(path1, path2)).ToList();

                    result.Add(new ComparisonResult(submissionsToCompare[i], submissionsToCompare[j],
                        similarityPercentages));

                    ++iterationCounter;
                    int currentProgress = progressTracker.Track(iterationCounter);

                    const int step = 5;

                    if (currentProgress % step == 0)
                    {
                        Console.WriteLine($"Текущий прогресс: {currentProgress} %");
                    }
                }
            }
        }

        _writer.Write(result);
    }
}