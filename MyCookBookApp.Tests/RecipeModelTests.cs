using Xunit;
using MyCookBookApp.Models; // Namespace for the Recipe model
using System.Collections.Generic;

namespace MyCookBookApp.Tests
{
    public class RecipeModelTests
    {
        [Fact]
        public void RecipeModel_ShouldStoreDataCorrectly()
        {
            // Arrange
            var recipe = new Recipe
            {
                Name = "Salad",
                Ingredients = new List<string> { "Lettuce", "Tomatoes", "Dressing" },
                Steps = "Mix ingredients together."
            };

            // Assert
            Assert.Equal("Salad", recipe.Name);
            Assert.Contains("Tomatoes", recipe.Ingredients);
            Assert.Equal("Mix ingredients together.", recipe.Steps);
        }
    }
}
