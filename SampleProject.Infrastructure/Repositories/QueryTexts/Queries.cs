namespace SampleProject.Infrastructure.Repositories.QueryTexts;

public static class Queries
{
    public static string GetSampleModelTotalCount => "select count(*) from[SampleProjectDB].[dbo].[SampleModels] where IsDeleted = 0";
}
