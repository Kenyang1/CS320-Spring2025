using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyCookBookApp.Models;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MyCookBookApp.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;  

        public RecipeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

public async Task<List<Recipe>> GetRecipesAsync()
{
    var response = await _httpClient.GetAsync($"{_baseUrl}/recipe");

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Error fetching recipes: {response.StatusCode}");
        return new List<Recipe>(); // ✅ Return empty list instead of null
    }

    var json = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Fetched Recipes: {json}"); // ✅ Debugging output
    return JsonConvert.DeserializeObject<List<Recipe>>(json) ?? new List<Recipe>();
}


        public async Task<Recipe> GetRecipeByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/recipe/{id}");
            if (!response.IsSuccessStatusCode)
                return null;
             
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Recipe>(json);
        }

        public async Task<List<Recipe>> SearchRecipesAsync(RecipeSearchRequest searchRequest)
        {
            var allRecipes = await GetRecipesAsync(); // ✅ Fetch all recipes first

            Console.WriteLine($"🔍 Searching for: {searchRequest.Keyword}");

            // ✅ Ensure keyword is not empty and convert to lowercase for case-insensitivity
            string keyword = searchRequest.Keyword.ToLower();

            var filteredRecipes = allRecipes
                .Where(r => 
                    (!string.IsNullOrEmpty(r.Name) && r.Name.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(r.Summary) && r.Summary.ToLower().Contains(keyword))
                ).ToList();

            Console.WriteLine($"✅ Found Recipes: {JsonConvert.SerializeObject(filteredRecipes)}");
            return filteredRecipes;
        }



        public async Task<bool> UpdateRecipeAsync(Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(recipe.RecipeId))
                return false;

            var encodedId = Uri.EscapeDataString(recipe.RecipeId);
            var content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/recipe/{encodedId}", content);
            
            return response.IsSuccessStatusCode;
        }
         
        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            var json = JsonConvert.SerializeObject(recipe, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/recipe", content);
            return response.IsSuccessStatusCode;
        }
    }
}
