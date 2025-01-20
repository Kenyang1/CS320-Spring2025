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
                Name = "Pasta",
                Ingredients = new List<string> { "Pasta", "Tomato Sauce" },
                Steps = "Boil pasta."
            };

            // Assert
            Assert.Equal("Pasta", recipe.Name);
            Assert.Contains("Tomato Sauce", recipe.Ingredients);
            Assert.Equal("Boil pasta.", recipe.Steps);
        }
    }
}
