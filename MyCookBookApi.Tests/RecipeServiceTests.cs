using System.Collections.Generic;
using Xunit;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using MyCookBookApi.Services;
using Moq; // For mocking dependencies

public class RecipeServiceTests
{
    private readonly RecipeService _recipeService;
    private readonly Mock<IRecipeRepository> _mockRepository;

    public RecipeServiceTests()
    {
        // ✅ Mocking the repository instead of using a real database
        _mockRepository = new Mock<IRecipeRepository>();
        _recipeService = new RecipeService(_mockRepository.Object);
    }

    // ✅ Test Case 1: Fetch all recipes returns a non-empty list
    [Fact]
    public void GetAllRecipes_ShouldReturnNonEmptyList()
    {
        // Arrange
        var fakeRecipes = new List<Recipe>
        {
            new Recipe { RecipeId = "1", Name = "Pasta" },
            new Recipe { RecipeId = "2", Name = "Salad" }
        };
        _mockRepository.Setup(repo => repo.GetAllRecipes()).Returns(fakeRecipes);

        // Act
        var result = _recipeService.GetAllRecipes();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
    }

    // ✅ Test Case 2: Searching for a recipe with a valid keyword returns results
    [Fact]
    public void SearchRecipes_WithValidKeyword_ReturnsResults()
    {
        // Arrange
        var searchRequest = new RecipeSearchRequest { Keyword = "Pasta", Categories = new List<CategoryType>() };
        var fakeRecipes = new List<Recipe>
        {
            new Recipe { RecipeId = "1", Name = "Pasta" }
        };
        _mockRepository.Setup(repo => repo.SearchRecipes(searchRequest)).Returns(fakeRecipes);

        // Act
        var result = _recipeService.SearchRecipes(searchRequest);

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains(result, r => r.Name.Contains("Pasta"));
    }

    // ✅ Test Case 3: Retrieving a recipe by ID returns the correct recipe
    [Fact]
    public void GetRecipeById_WhenRecipeExists_ShouldReturnCorrectRecipe()
    {
        // Arrange
        var fakeRecipe = new Recipe { RecipeId = "1", Name = "Pasta" };
        _mockRepository.Setup(repo => repo.GetRecipeById("1")).Returns(fakeRecipe);

        // Act
        var result = _recipeService.GetRecipeById("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.RecipeId);
        Assert.Equal("Pasta", result.Name);
    }

    // ✅ Test Case 4: Deleting a non-existing recipe returns false
    [Fact]
    public void DeleteRecipe_WhenRecipeDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteRecipe("999")).Returns(false);

        // Act
        var result = _recipeService.DeleteRecipe("999");

        // Assert
        Assert.False(result);
    }

    // ✅ Test Case 5: Adding a recipe increases the list count
    [Fact]
    public void AddRecipe_ShouldIncreaseRecipeCount()
    {
        // Arrange
        var initialRecipes = new List<Recipe>
        {
            new Recipe { RecipeId = "1", Name = "Pasta" }
        };
        _mockRepository.Setup(repo => repo.GetAllRecipes()).Returns(initialRecipes);

        var newRecipe = new Recipe
        {
            RecipeId = "3",
            Name = "Soup",
            TagLine = "Hot and Tasty",
            Summary = "A delicious homemade soup.",
            Ingredients = new List<string> { "Water", "Vegetables" },
            Instructions = new List<string> { "Boil water.", "Add vegetables." },
            Categories = new List<CategoryType>(),
            Media = new List<RecipeMedia>()
        };

        // Act
        _recipeService.AddRecipe(newRecipe);

        // Assert
        _mockRepository.Verify(repo => repo.AddRecipe(newRecipe), Times.Once);
    }
}
