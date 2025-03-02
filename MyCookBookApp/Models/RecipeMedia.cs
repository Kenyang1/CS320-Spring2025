namespace MyCookBookApp.Models
{
    public class RecipeMedia
    {
        public string Url { get; set; }   // URL for the image or video (e.g., Firebase Storage URL)
        public string Type { get; set; }  // Type of media ("image" or "video")
        public int Order { get; set; }    // Determines the display order if multiple media files are used
    }
}
