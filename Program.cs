using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlogContext")));
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BlogContext>();

var app = builder.Build();

// Rôle Admin et compte administrateur créés au démarrage s'ils n'existent pas
using (var scope = app.Services.CreateScope())
{
    // Crée ou met à jour la base au démarrage (indispensable sur le serveur)
    scope.ServiceProvider.GetRequiredService<BlogContext>().Database.Migrate();

    var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var users = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roles.RoleExistsAsync("Admin"))
    {
        await roles.CreateAsync(new IdentityRole("Admin"));
    }

    var motDePasse = app.Configuration["Admin:MotDePasse"];
    var admin = await users.FindByEmailAsync("admin@blog.fr");
    if (admin == null && !string.IsNullOrEmpty(motDePasse))
    {
        admin = new IdentityUser { UserName = "admin@blog.fr", Email = "admin@blog.fr" };
        await users.CreateAsync(admin, motDePasse);
        await users.AddToRoleAsync(admin, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Dates et nombres en français, même sur un serveur réglé en anglais
app.UseRequestLocalization("fr-FR");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.MapRazorPages();

app.Run();
