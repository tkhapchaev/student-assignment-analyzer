namespace KysectAcademyTask;

public class ProgressTracker
{
    private readonly int _numberOfIterations;

    public ProgressTracker(IReadOnlyCollection<Submission> submissions, IEnumerable<string> assignments)
    {
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

                    ++iterationCounter;
                }
            }
        }

        _numberOfIterations = iterationCounter;
    }

    public int Track(int iterationsCompleted)
    {
        return Convert.ToInt32((double)iterationsCompleted / _numberOfIterations * 100);
    }
}