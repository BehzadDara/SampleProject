namespace AnotherSampleProject.API.Model;

public class TestModel
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public static TestModel Create(string name)
    {
        return new TestModel { Name = name };
    }
}
