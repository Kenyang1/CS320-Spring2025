using MyCookBookApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container BEFORE calling Build()
builder.Services.AddControllersWithViews();

// Register the RecipeService with HttpClient
builder.Services.AddHttpClient<RecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
