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
                RecipeId = "1",
                Name = "Pasta",
                TagLine = "Delicious Pasta",
                Summary = "A simple Italian pasta dish.",
                Instructions = new List<string> { "Boil pasta." },
                Ingredients = new List<string> { "Pasta", "Tomato Sauce" },
                Categories = new List<CategoryType>(), // Empty category list
                Media = new List<RecipeMedia>() // Empty media list
            };

            // Assert
            Assert.Equal("1", recipe.RecipeId);
            Assert.Equal("Pasta", recipe.Name);
            Assert.Equal("Delicious Pasta", recipe.TagLine);
            Assert.Equal("A simple Italian pasta dish.", recipe.Summary);
            Assert.Contains("Boil pasta.", recipe.Instructions);
            Assert.Contains("Tomato Sauce", recipe.Ingredients);
        }
    }
}
