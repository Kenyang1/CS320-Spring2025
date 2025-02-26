using System.Collections.Generic;
using Xunit;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using MyCookBookApi.Services;

public class RecipeServiceTests
{
    private readonly RecipeService _recipeService;

    public RecipeServiceTests()
    {
        // We use the MockRecipeRepository to simulate our database.
        _recipeService = new RecipeService(new MockRecipeRepository());
    }

    [Fact]
    public void GetAllRecipes_ShouldReturnNonEmptyList()
    {
        // Act
        var recipes = _recipeService.GetAllRecipes();

        // Assert: The list should not be empty.
        Assert.NotEmpty(recipes);
    }

    [Fact]
    public void GetRecipeById_ShouldReturnCorrectRecipe()
    {
        // Arrange: Grab the RecipeId of the first recipe.
        var recipeId = _recipeService.GetAllRecipes()[0].RecipeId;

        // Act: Retrieve the recipe using that ID.
        var recipe = _recipeService.GetRecipeById(recipeId);

        // Assert: The returned recipe should not be null and should match the RecipeId.
        Assert.NotNull(recipe);
        Assert.Equal(recipeId, recipe.RecipeId);
    }
}
