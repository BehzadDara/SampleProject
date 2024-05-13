namespace SampleProject.Infrastructure.QueryTexts;

public static class Queries
{

    internal static string GetSampleModelTotalCount = GetQuery(nameof(GetSampleModelTotalCount));
    internal static string GetAnotherSampleModelTotalCount = GetQuery(nameof(GetAnotherSampleModelTotalCount));

    private static string GetQuery(string name)
    {
#if DEBUG
        return File.ReadAllText($"../SampleProject.Infrastructure/QueryTexts/{name}.sql");
#else
        return File.ReadAllText($"QueryTexts/{name}.sql");
#endif
    }

}
