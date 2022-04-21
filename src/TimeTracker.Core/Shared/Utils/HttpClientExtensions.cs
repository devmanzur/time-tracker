using System.Net.Http.Json;
using System.Text;
using CSharpFunctionalExtensions;

namespace TimeTracker.Core.Shared.Utils
{
    public static class HttpClientExtensions
    {
        public static async Task<Result<TResponse>> GetAsync<TResponse>(this HttpClient httpClient, string path)
        {
            var response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var output = await response.Content.ReadFromJsonAsync<TResponse>();
                if (output.HasValue())
                {
                    return Result.Success(output);
                }

                return Result.Failure<TResponse>("Invalid response");
            }

            return Result.Failure<TResponse>("Bad request!");
        }

        public static async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(this HttpClient httpClient,
            string path,
            TRequest request)
            where TRequest : class
        {
            var requestBody = new StringContent(
                AppJsonConverter.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(path, requestBody);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var output = await response.Content.ReadFromJsonAsync<TResponse>();
                    if (output.HasValue())
                    {
                        return Result.Success(output!);
                    }
                }
                catch (Exception e)
                {
                    return Result.Failure<TResponse>(e.Message);
                }

                return Result.Failure<TResponse>("Invalid response");
            }

            return Result.Failure<TResponse>($"{response.StatusCode}");
        }

        public static async Task<Result<HttpResponseMessage>> PostRawAsync<TRequest>(this HttpClient httpClient,
            string path,
            TRequest request)
            where TRequest : class
        {
            var requestBody = new StringContent(
                AppJsonConverter.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(path, requestBody);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success(response);
            }

            return Result.Failure<HttpResponseMessage>($"{response.StatusCode}");
        }

        public static async Task<Result<HttpResponseMessage>> PatchRawAsync<TRequest>(this HttpClient httpClient,
            string path,
            TRequest request)
            where TRequest : class
        {
            var requestBody = new StringContent(
                AppJsonConverter.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PatchAsync(path, requestBody);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success(response);
            }

            return Result.Failure<HttpResponseMessage>($"{response.StatusCode}");
        }
    }
}