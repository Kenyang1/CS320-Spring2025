using Xunit;
using MyCookBookApi.Models; // Namespace for the Recipe model
using System.Collections.Generic;

namespace MyCookBookApi.Tests
{
    public class RecipeModelTests
    {
        [Fact]
        public void RecipeModel_ShouldStoreDataCorrectly()
        {
            
            // Arrange
            var recipe = new Recipe
            {
                RecipeId = "123",
                Name = "Pasta",
                TagLine = "Classic Italian Delight",
                Summary = "A simple yet delicious pasta dish",
                Ingredients = new List<string> { "Pasta", "Tomato Sauce" },
                Instructions = new List<string> { "Boil water.", "Cook pasta until al dente.", "Mix with sauce." },
            };

            // Assert
            Assert.Equal("123", recipe.RecipeId);
            Assert.Equal("Pasta", recipe.Name);
            Assert.Contains("Tomato Sauce", recipe.Ingredients);
            Assert.Contains("Boil pasta.", recipe.Instructions);
        }
    }
}
