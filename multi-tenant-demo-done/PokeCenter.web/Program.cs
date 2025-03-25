using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using PokeCenter.web;
using PokeCenter.web.Identities;
using PokeCenter.web.Middleware;
using Pokedex.Data.Models;
using Pokedex.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
    )
    ;
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddScoped<ImportPokemonData>()
    ;

builder.Services
    .AddTenantContext()
    .AddScoped<TenantMiddleware>();


builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserStore<ApplicationUserStore>()
    .AddSignInManager<MultiTenantSigninManager>()
    ;
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
    await scope.ServiceProvider.GetRequiredService<ImportPokemonData>().Import();
}


app.UseHttpsRedirection();
app.UseMiddleware<TenantMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets()
    .RequireAuthorization()
    ;

app.Run();