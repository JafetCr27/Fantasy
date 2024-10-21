
using System.Text.Json;

namespace Fantasy.Frondend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            {
                var respose = await UnserealizeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(respose, false, responseHttp);
            }
            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON);
            var messageHttp = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, false, messageHttp);
        }
        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON);
            var responsegeHttp = await _httpClient.PostAsync(url, messageContent);
            if (responsegeHttp.IsSuccessStatusCode)
            {
                var response = await UnserealizeAnswer<TActionResponse>(responsegeHttp);
                return new HttpResponseWrapper<TActionResponse>(response, false, responsegeHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, !responsegeHttp.IsSuccessStatusCode, responsegeHttp);
        }
        private async Task<T?> UnserealizeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions);
        }

    }
}
