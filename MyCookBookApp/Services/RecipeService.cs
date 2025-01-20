using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyCookBookApp.Models;

namespace MyCookBookApp.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            // Perform the HTTP GET request
            var response = await _httpClient.GetAsync("http://localhost:5165/api/recipe");

            // Ensure the response was successful (throws exception for non-success codes)
            response.EnsureSuccessStatusCode();

            // Read and deserialize the JSON response
            var json = await response.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);

            // Return the deserialized recipes or an empty list if null
            return recipes ?? new List<Recipe>();
        }
    }
}
