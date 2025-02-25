namespace MyCookBookApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<string> Ingredients { get; set; }
        public required string Steps { get; set; }
    }
}
