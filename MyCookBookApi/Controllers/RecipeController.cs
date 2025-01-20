using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRecipes()
    {
        return Ok(new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Pasta", Ingredients = new List<string> { "Pasta", "Tomato Sauce" }, Steps = "Boil pasta." },
            new Recipe { Id = 2, Name = "Salad", Ingredients = new List<string> { "Lettuce", "Tomatoes" }, Steps = "Mix all ingredients." }
        });
    }
}
