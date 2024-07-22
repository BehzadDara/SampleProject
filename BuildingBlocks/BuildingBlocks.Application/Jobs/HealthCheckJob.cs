using Polly;

namespace BuildingBlocks.Application.Jobs;

public class HealthCheckJob
{
    public static async Task CheckStatus(string healthCheckURL)
    {
        var policy = Policy
                   .Handle<Exception>()
                   .WaitAndRetryAsync(
                       3,
                       retryAttempt => TimeSpan.FromSeconds(5 * retryAttempt),
                       (exception, timeSpan, retryCount, context) =>
                       {
                           Console.WriteLine($"Retry CheckStatus: At {DateTime.Now}" +
                               $", Attempt {retryCount}, Message = {exception.Message}");
                       }
                   );

        try
        {
            await policy.ExecuteAsync(async () =>
            {
                using var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(healthCheckURL);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Health check response: {responseBody}");
            });
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Health check Error: {e.Message}");
        }
    }
}
