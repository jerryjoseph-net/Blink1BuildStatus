using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public class TfsApiClient
    {
        private readonly HttpClient _httpClient;

        public TfsApiClient(string tfsHost, string username, string password)
        {
            _httpClient = new HttpClient {BaseAddress = new Uri(tfsHost)};

            Instance = tfsHost;

            var mediaTypeHeader = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(mediaTypeHeader);

            var credentialBytes = Encoding.ASCII.GetBytes($"{username}:{password}");
            var encodedCredentialBytes = Convert.ToBase64String(credentialBytes);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentialBytes);
        }

        public string Instance { get; set; }

        public async Task<TfsBuildsResponse> GetBuildsAsync(string projectName, IEnumerable<string> definitions = null)
        {
            var url = $"{Instance}/DefaultCollection/{projectName}/_apis/build/builds?api-version=2.0";

            if (definitions != null && definitions.Any())
            {
                url += $"&definitions={string.Join(",", definitions)}";
            }

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TfsBuildsResponse>();
        }
    }
}
