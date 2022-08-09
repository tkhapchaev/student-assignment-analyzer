namespace KysectAcademyTask;

public class LevenshteinDistance : IComparator
{
    public double CompareFiles(string firstFilePath, string secondFilePath)
    {
        string firstFile = File.ReadAllText(firstFilePath), secondFile = File.ReadAllText(secondFilePath);
        int greaterLength = Math.Max(firstFile.Length, secondFile.Length);

        int firstFileLength = firstFile.Length, secondFileLength = secondFile.Length;
        int[,] matrix = new int[firstFileLength + 1, secondFileLength + 1];

        if (firstFileLength == 0)
        {
            return secondFileLength;
        }

        if (secondFileLength == 0)
        {
            return firstFileLength;
        }

        for (int i = 0; i <= firstFileLength; matrix[i, 0] = i++) { }

        for (int j = 0; j <= secondFileLength; matrix[0, j] = j++) { }

        for (int i = 1; i <= firstFileLength; i++)
        {
            for (int j = 1; j <= secondFileLength; j++)
            {
                int cost = (secondFile[j - 1] == firstFile[i - 1]) ? 0 : 1;
                matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost);
            }
        }

        int levenshteinDistance = matrix[firstFileLength, secondFileLength];
        double result = (double)levenshteinDistance / greaterLength;

        return Math.Round(100 - result * 100, 2);
    }
}