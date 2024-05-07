namespace SampleProject.Infrastructure.Repositories.QueryTexts;

public static class Queries
{

    internal static string GetSampleModelTotalCount = GetQuery(nameof(GetSampleModelTotalCount));

    private static string GetQuery(string name)
    {
#if DEBUG
        return File.ReadAllText($"../SampleProject.Infrastructure/Repositories/QueryTexts/{name}.sql");
#else
        return File.ReadAllText($"Repositories/QueryTexts/{name}.sql");
#endif
    }

}
