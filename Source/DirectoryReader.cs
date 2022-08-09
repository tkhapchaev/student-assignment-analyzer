namespace KysectAcademyTask;

public class DirectoryReader : IReader
{
    private readonly List<string> _authorWhitelist;

    private readonly List<string> _authorBlacklist;

    private readonly List<string> _extensionWhitelist;

    private readonly List<string> _directoryBlacklist;

    private int _pathDepthToFiles;

    public DirectoryReader(List<string> authorWhitelist, List<string> authorBlacklist,
        List<string> extensionWhitelist, List<string> directoryBlacklist)
    {
        _authorWhitelist = authorWhitelist;
        _authorBlacklist = authorBlacklist;
        _extensionWhitelist = extensionWhitelist;
        _directoryBlacklist = directoryBlacklist;
    }

    public List<Submission> Read(string inputDirectoryPath)
    {
        string inputDirectoryName = Path.GetFileName(inputDirectoryPath);
        IEnumerable<string> files = Parse(inputDirectoryPath, 0);

        var formattedPaths = files.Select(file => file.Split(Path.DirectorySeparatorChar)).ToList();
        var result = new List<List<string>>();

        var inputDirectoryNamePositions = new List<int>();
        var fileNames = new List<string>();

        foreach (string[] formattedPath in formattedPaths)
        {
            for (int j = 0; j < formattedPath.Length; ++j)
            {
                if (formattedPath[j] == inputDirectoryName)
                {
                    inputDirectoryNamePositions.Add(j);
                }
            }

            fileNames.Add(string.Join(Path.DirectorySeparatorChar, formattedPath));
        }

        for (int i = 0; i < formattedPaths.Count; ++i)
        {
            var newPath = new List<string>();
            result.Add(newPath);

            for (int j = inputDirectoryNamePositions[i] + 1; j < formattedPaths[i].Length; ++j)
            {
                result[i].Add(formattedPaths[i][j]);
            }

            result[i].RemoveAt(result[i].Count - 1);
            result[i].Add(fileNames[i]);
        }

        var submissions = result.Select(formattedPath => formattedPath.Count == _pathDepthToFiles
                ? new Submission(formattedPath[0], formattedPath[1], formattedPath[2],
                    new SubmissionDate(formattedPath[3]),
                    formattedPath[4])
                : new Submission(formattedPath[0], formattedPath[1], formattedPath[2], formattedPath[3]))
            .ToList();

        return Filter(submissions);
    }

    private List<Submission> Filter(List<Submission> submissions)
    {
        var result = submissions.ToList();
        var authorWhitelist = new AuthorWhitelist(_authorWhitelist);
        var authorBlacklist = new AuthorBlacklist(_authorBlacklist);
        var extensionWhitelist = new ExtensionWhitelist(_extensionWhitelist);
        var directoryBlacklist = new DirectoryBlacklist(_directoryBlacklist);

        foreach (Submission submission in submissions)
        {
            string author = submission.Author, extension = Path.GetExtension(submission.File);

            if (!authorWhitelist.Contains(author) || authorBlacklist.Contains(author) ||
                !extensionWhitelist.Contains(extension))
            {
                result.Remove(submission);
            }

            if (directoryBlacklist.Contains(submission.Group) ||
                directoryBlacklist.Contains(submission.Author) ||
                directoryBlacklist.Contains(submission.Assignment) ||
                directoryBlacklist.Contains(submission.SubmissionDate.RawDate) ||
                directoryBlacklist.Contains(submission.File))
            {
                result.Remove(submission);
            }
        }

        return result;
    }

    private IEnumerable<string> Parse(string directoryPath, int currentDepth)
    {
        var files = new List<string>();
        ++currentDepth;

        string[] subdirectories = Directory.GetDirectories(directoryPath);
        files.AddRange(Directory.GetFiles(directoryPath));

        if (subdirectories.Length == 0 && currentDepth > _pathDepthToFiles)
        {
            _pathDepthToFiles = currentDepth;
            currentDepth = 0;
        }

        foreach (string subdirectory in subdirectories)
        {
            files.AddRange(Parse(subdirectory, currentDepth));
        }

        return files;
    }
}