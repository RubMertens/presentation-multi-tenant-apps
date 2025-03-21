using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Framework.Tenants.DependencyInjection;
using Pokedex.Framework.Tenants.Options;
using Pokedex.web;
using Pokedex.web.MultiTenancy;

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
    .AddSingleton<ImportDefaultUsers>()
    ;


builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    ;
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.Configure<AvailableTenants>(builder.Configuration.GetSection(AvailableTenants.SectionName));
builder.Services.AddTenantContext();
builder.Services.AddScoped<MultiTenantMiddleware>();


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

await app.Services.GetRequiredService<ImportDefaultUsers>().Import();


app.UseHttpsRedirection();
app.UseMiddleware<MultiTenantMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets()
    .RequireAuthorization()
    ;
// .RequireAuthorization();

app.Run();