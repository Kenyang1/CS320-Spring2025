namespace MyCookBookApp.Models
{
    public class RecipeSearchRequest
    {
        public string? Keyword { get; set; } // The keyword for searching recipes
        public List<CategoryType> Categories { get; set; } = new List<CategoryType>(); // Filter by categories
    }
}

