namespace KysectAcademyTask;

public static class Program
{
    public static void Main()
    {
        try
        {
            var configuration = new ConfigurationParser();

            var executor = new Executor(configuration.GetInputDirectoryPath(), configuration.GetComparisonAlgorithms(),
                configuration.GetReportType(), configuration.GetExtensionWhitelist(),
                configuration.GetDirectoryBlacklist(), configuration.GetAuthorWhitelist(),
                configuration.GetAuthorBlacklist());

            executor.Analyze();
        }

        catch (Exception exception)
        {
            Console.WriteLine("Кажется, что-то пошло не так..." + Environment.NewLine + exception.Message);
        }
    }
}