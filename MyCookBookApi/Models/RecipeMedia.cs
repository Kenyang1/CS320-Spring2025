namespace MyCookBookApi.Models
{
    public class RecipeMedia
    {
        public string Url { get; set; }  // URL for the media (e.g., from Firebase Storage)
        public string Type { get; set; } // "image" or "video"
        public int Order { get; set; }   // Display order
    }
}
