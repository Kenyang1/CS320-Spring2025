document.addEventListener("DOMContentLoaded", function () {
    const BASE_URL = `${window.location.origin}/Recipe`;
    let selectedCategories = [];

    // ✅ Define category mapping (Text Labels)
    const categoryMapping = {
        0: "Breakfast", 1: "Lunch", 2: "Dinner",
        3: "Dessert", 4: "Snack", 5: "Vegan",
        6: "Vegetarian", 7: "GlutenFree", 8: "Keto",
        9: "LowCarb", 10: "HighProtein"
    };

    fetchRecipes(); // ✅ Load recipes on page load

    // ✅ Handle category selection in dropdown
    document.querySelectorAll("#categoryList .dropdown-item").forEach(item => {
        item.addEventListener("click", function (event) {
            event.preventDefault();
            let categoryValue = this.getAttribute("data-value");

            if (selectedCategories.includes(categoryValue)) {
                selectedCategories = selectedCategories.filter(c => c !== categoryValue);
            } else {
                selectedCategories.push(categoryValue);
            }

            // ✅ Update button text
            document.getElementById("categoryDropdown").innerText =
                selectedCategories.length > 0 ? selectedCategories.join(", ") : "Select Categories";

            // ✅ Update hidden input
            document.getElementById("categories").value = JSON.stringify(selectedCategories);
            console.log("✅ Selected Categories:", selectedCategories);
        });
    });

    // ✅ Open Add Recipe Modal
    document.getElementById("addRecipeBtn")?.addEventListener("click", function () {
        document.getElementById("addRecipeForm").reset();
        selectedCategories = [];
        document.getElementById("categories").value = "[]"; // ✅ Reset categories
        document.getElementById("categoryDropdown").innerText = "Select Categories"; 
        $('#addRecipeModal').modal('show');
    });

    // ✅ Handle Recipe Submission (Add Recipe)
    document.getElementById("saveRecipe").addEventListener("click", function (event) {
        event.preventDefault();
        let recipe = {
            recipeId: "NewRecipe_" + Date.now(),
            name: document.getElementById("recipeName").value.trim(),
            tagLine: document.getElementById("tagLine").value.trim(),
            summary: document.getElementById("summary").value.trim(),
            ingredients: document.getElementById("ingredients").value.split(",").map(i => i.trim()),
            instructions: document.getElementById("instructions").value.split("\n").map(i => i.trim()),

            // ✅ Convert categories from string to enum values
            categories: selectedCategories.map(cat => categoryMapping[parseInt(cat)] || "Uncategorized"),
            
            media: []
        };

        console.log("🔍 Final Recipe Object:", JSON.stringify(recipe, null, 2));

        fetch(`${BASE_URL}/Add`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(recipe)
        })
        .then(response => response.json())
        .then(data => {
            console.log("📜 Server Response:", data);

            if (data.success) {
                console.log("✅ Recipe added successfully!");
                $('#addRecipeModal').modal('hide');
                setTimeout(() => { location.reload(); }, 500);
            } else {
                alert("❌ Failed to add recipe: " + (data.message || "Unknown error"));
            }
        })
        .catch(error => {
            console.error("❌ Error adding recipe:", error);
            alert("❌ Failed to add recipe. Check console for details.");
        });
    });

    // ✅ Fetch All Recipes
    function fetchRecipes() {
        console.log("📥 Fetching all recipes...");

        fetch(`${BASE_URL}/GetAll`)
        .then(response => response.json())
        .then(recipes => {
            console.log("📜 Recipes Loaded:", recipes);
            displayRecipes(recipes);
        })
        .catch(error => console.error("❌ Fetch error:", error));
    }

    // ✅ Add Event Listener for Search Button
    document.getElementById("searchButton")?.addEventListener("click", function () {
        searchRecipes();
    });

    // ✅ Handle Enter Key for Search
    document.getElementById("searchInput").addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            searchRecipes();
        }
    });

    // ✅ Function to Search Recipes
    window.searchRecipes = function () {
        let keyword = document.getElementById("searchInput").value.trim();
        if (!keyword) {
            alert("Please enter a keyword to search.");
            return;
        }

        let searchRequest = { keyword: keyword };

        console.log("🔍 Sending Search Request:", JSON.stringify(searchRequest));

        fetch(`${BASE_URL}/Search`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(searchRequest)
        })
        .then(response => response.json())
        .then(data => {
            console.log("📜 Search Response:", data);

            if (data.success) {
                displayRecipes(data.recipes);
            } else {
                document.getElementById("recipeCards").innerHTML = `<p class="text-center text-muted">${data.message}</p>`;
            }
        })
        .catch(error => console.error("❌ Search error:", error));
    };

    // ✅ Display Recipes Dynamically
    function displayRecipes(recipes) {
        let cardContainer = document.getElementById("recipeCards");
        cardContainer.innerHTML = "";

        recipes.forEach(recipe => {
            let categoriesText = (recipe.categories || []).join(" | ");

            let card = `
                <div class="mb-3">
                    <div class="card shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title mb-0">${recipe.name}</h5>
                            <p class="text-muted mb-2">${recipe.tagLine || ""}</p>
                            <h6>Chef's Note</h6>
                            <p class="card-text">${recipe.summary || "No summary available."}</p>
                            <h6>Ingredients</h6>
                            <p class="card-text">${recipe.ingredients ? recipe.ingredients.join(", ") : "N/A"}</p>
                            <h6>Instructions</h6>
                            <p class="card-text">${recipe.instructions ? recipe.instructions.join("<br>") : "N/A"}</p>
                        </div>
                        <div class="card-footer text-muted text-left" style="font-size: 0.85rem;">
                            <b>Categories: </b>${categoriesText}
                        </div>
                    </div>
                </div>`;
            cardContainer.innerHTML += card;
        });
    }
});
