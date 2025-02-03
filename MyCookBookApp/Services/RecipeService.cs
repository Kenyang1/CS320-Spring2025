using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyCookBookApp.Models;
using System.Text;

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
            var response = await _httpClient.GetAsync("http://localhost:5090/api/recipe");

            response.EnsureSuccessStatusCode();

            // Read and deserialize the JSON response
            var json = await response.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);

            return recipes ?? new List<Recipe>();
        }

        public async Task<List<Recipe>> SearchRecipesAsync(string query)
        {
            // Perform the HTTP POST request
            var payload = new { Query = query };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5090/api/recipe/search", content);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) 
            {
                return new List<Recipe>();
            }
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Recipe>>(responseString) ?? new List<Recipe>();
        }
    }
}
