namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Steps { get; set; }
    }
}
