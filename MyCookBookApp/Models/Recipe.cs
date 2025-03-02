namespace MyCookBookApp.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Summary { get; set; }
        public List<string> Instructions { get; set; } = new List<string>();
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<CategoryType> Categories { get; set; } = new List<CategoryType>();
        public List<RecipeMedia> Media { get; set; } = new List<RecipeMedia>();

        public Recipe() { }
    }
}
