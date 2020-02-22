using Newtonsoft.Json;
using Omdb.API.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Omdb.API
{
    public class OmdbRepository : IOmdbRepository
    {
        public async Task<MovieResponse> Search(string title)
        {

            var httpClient = new HttpClient();
            var baseApiUrl = "http://www.omdbapi.com/";
            var key = "dd0fa8bc";

            var queryUri = baseApiUrl + "?s=&apiKey=";                  // "http://www.omdbapi.com/?s=&apiKey="

            var uriBuilder = new UriBuilder(queryUri);

            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryString.Set("s", title);                                // s= => s=nameMovie
            queryString.Set("apiKey", key);                             // apiKey= => apiKey=dd0fa8bc

            uriBuilder.Query = queryString.ToString();

            HttpResponseMessage httpResponse = await httpClient.GetAsync(uriBuilder.Uri);

            MovieResponse response = new MovieResponse();
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<MovieResponse>(json);

            }

            return response;

        }
    }
}
