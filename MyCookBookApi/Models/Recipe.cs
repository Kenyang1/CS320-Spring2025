using System.Text.Json.Serialization;
namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public required string RecipeId { get; set; }
        public required string Name { get; set; }
        public required string TagLine {get; set;}
        public required string Summary {get; set;}
        public required List<string> Instructions {get; set;} = new List<string>();
        public required List<string> Ingredients { get; set; } = new List<string>();

        // Optional: Categories to organize recipes (e.g., Breakfast, Dinner)
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required List<CategoryType>? Categories { get; set; } = new List<CategoryType>();

        // Optional: Media such as images or videos with the recipe
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required List<RecipeMedia>? Media { get; set; } = new List<RecipeMedia>();

        public Recipe() { }
    }
}
