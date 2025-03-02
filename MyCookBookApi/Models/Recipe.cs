using System.Text.Json.Serialization;
namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        public  string Name { get; set; }
        public  string TagLine {get; set;}
        public  string Summary {get; set;}
        public  List<string> Instructions {get; set;} = new List<string>();
        public  List<string> Ingredients { get; set; } = new List<string>();

        // Optional: Categories to organize recipes (e.g., Breakfast, Dinner)
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public  List<CategoryType>? Categories { get; set; } = new List<CategoryType>();

        // Optional: Media such as images or videos with the recipe
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public  List<RecipeMedia>? Media { get; set; } = new List<RecipeMedia>();

        public Recipe() { }
    }
}
