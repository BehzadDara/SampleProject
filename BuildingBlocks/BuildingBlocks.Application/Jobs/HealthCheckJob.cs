namespace BuildingBlocks.Application.Jobs;

public class HealthCheckJob
{
    public static async Task CheckStatus()
    {
        string healthCheckUrl = "http://localhost:5274/healthz";

        using var client = new HttpClient();
        try
        {
            HttpResponseMessage response = await client.GetAsync(healthCheckUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Health check response: {responseBody}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Health check Error: {e.Message}");
        }
    }
}
