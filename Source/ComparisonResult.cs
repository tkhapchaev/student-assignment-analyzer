namespace KysectAcademyTask;

public class ComparisonResult
{
    public Submission FirstSubmission { get; }

    public Submission SecondSubmission { get; }

    public string Result { get; }

    public ComparisonResult(Submission firstSubmission, Submission secondSubmission,
        IEnumerable<double> similarityPercentages)
    {
        double averagePercentage = similarityPercentages.Average();
        FirstSubmission = firstSubmission;
        SecondSubmission = secondSubmission;

        string group1 = FirstSubmission.Group,
            group2 = SecondSubmission.Group,
            author1 = FirstSubmission.Author,
            author2 = SecondSubmission.Author,
            assignment = FirstSubmission.Assignment,
            path1 = FirstSubmission.File,
            path2 = SecondSubmission.File,
            file1 = Path.GetFileName(path1),
            file2 = Path.GetFileName(path2);

        Result = $"Задание \"{assignment}\": сравнение {author1} ({group1}) и " +
                 $"{author2} ({group2}), файлы {file1} и {file2} " +
                 $"соответственно. Процент схожести: {averagePercentage} %";
    }
}