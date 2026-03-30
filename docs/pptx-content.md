# PowerPoint Content Extraction

**Source:** KdG - Multi-Tenant Applications.pptx
**Slide dimensions:** 12192000 x 6858000 EMUs (13.3" x 7.5")
**Total slides:** 60

---

## Slide 1
**Layout:** Titeldia

### Title: Building Multi-Tenant Applications

---

## Slide 2
**Layout:** Titel en object

### Title: Agenda

**Text (Content Placeholder 1):**
- Who am I
- What is multi-tenancy
- How to detect a tenant
- Demo time!

---

## Slide 3
**Layout:** Sectiekop

### Title: Who am I?

---

## Slide 4
**Layout:** Titel en object

### Title: Ruben

**Text (Content Placeholder 1):**
- Started on KdG
- 8 Jaar dotnet
  - Full stack developer
  - Architect
- Trainingen
- Sessies
- Internship
- ….

**Text (Content Placeholder 1):**
- 🥁 🎸 🎹
- 🎲 🐉

---

## Slide 5
**Layout:** Sectiekop

### Title: What is multi tenancy?

---

## Slide 6
**Layout:** Titel en object

### Title: What is it?

**Text (Content Placeholder 1):**
- Running your software for multiple clients (aka tenants) at once

---

## Slide 7
**Layout:** Inhoud van twee

### Title: Not all Multi-tenancy is equal

**Text (Content Placeholder 1):**
- What do you think is possible?

---

## Slide 8
**Layout:** Inhoud van twee

---

## Slide 9
**Layout:** Inhoud van twee

**Image (Picture 6):** [image/png, size: 8.4" x 7.3"]

---

## Slide 10
**Layout:** Tekst + foto

### Title: Multi-tenancy by infrastructure

**Image (Picture 6):** [image/png, size: 5.2" x 4.5"]

**Text (Text Placeholder 2):**
- ✅ App has no knowledge of tenants
- ✅ Data is perfectly separated
- ❌ Cross-cutting concerns are harder
- ❌ High cost of operation
- ❌ Cross tenant reporting

---

## Slide 11
**Layout:** Tekst + foto

**Image (Picture 8):** [image/png, size: 5.5" x 6.1"]

---

## Slide 12
**Layout:** Tekst + foto

### Title: Database per tenant

**Text (Text Placeholder 2):**
- ✅ Data is perfectly separated
- 💭 App needs config per tenant
- ❌ (still) high cost of operation
- ❌ Data consistency between tenants
- ❌ Cross tenant reporting

**Image (Picture 8):** [image/png, size: 4.1" x 4.5"]

---

## Slide 13
**Layout:** Tekst + foto

**Image (Picture 2):** [image/png, size: 4.0" x 7.3"]

---

## Slide 14
**Layout:** Tekst + foto

### Title: Schema per tenant

**Text (Text Placeholder 2):**
- ✅ Data is mostly separate
- ✅ Lower operation cost
- ✅ Cross-tenant reporting is possible
- 💭 App needs config per tenant
- ❌ Noisy neighbors

**Image (Picture 2):** [image/png, size: 3.0" x 5.4"]

---

## Slide 15
**Layout:** Titel en tekst 1

**Image (Picture 4):** [image/png, size: 5.1" x 7.6"]

---

## Slide 16
**Layout:** Tekst + foto

### Title: Discriminator column

**Text (Text Placeholder 2):**
- ✅ Lower operation cost
- ✅ Cross-tenant reporting is possible
- ✅ Easy to set up
- ✅ Easy to understand
- 💭 App needs config per tenant
- ❌ Noisy neighbors
- ⚠️ Risk leaky tenant data

**Image (Picture 4):** [image/png, size: 3.6" x 5.3"]

---

## Slide 17
**Layout:** Inhoud van twee

---

## Slide 18
**Layout:** Inhoud van twee

### Title: Best one?

---

## Slide 19
**Layout:** Inhoud van twee

### Title: It depends

**Text (Content Placeholder 2):**
- 🤷

---

## Slide 20
**Layout:** Sectiekop

### Title: Who’s knocking at the door?

**Text (Text Placeholder 2):**
- How do you know which tenant to serve

---

## Slide 21
**Layout:** Titel en object

### Title: What in what ways could we detect which tenant is calling ?

---

## Slide 22
**Layout:** Titel en object

### Title: Query string

**Text (Content Placeholder 1):**
- https://my-website.com?tenantId=customer1

---

## Slide 23
**Layout:** Titel en object

### Title: Header

**Text (TextBox 4):**
- GET http://my-website.com/pagex-TenantId: customer1

---

## Slide 24
**Layout:** Titel en object

### Title: Subdomain

**Text (TextBox 4):**
- http://customer1.my-website.com/page

---

## Slide 25
**Layout:** Sectiekop

### Title: Enough TalkShow me the code 🧑‍💻

**Text (Text Placeholder 2):**
- What tenant is calling

---

## Slide 26
**Layout:** Titel en object

### Title: Demo time!

**Text (Content Placeholder 1):**
- Let’s make an application multi-tenant aware!

---

## Slide 27
**Layout:** Titel en object

### Title: Script

**Text (Content Placeholder 1):**
- Detect a tenant
  - Middleware
  - Subdomain
  - TenantContext + Accessor
- Reflect a tenant
  - Knowing available tenants ->
  - TenantConfig extending with custom info
- Protect a tenant
  - ApplicationUser
  - ApplicationUserStore
  - MultiTenantSigninManager
- Protect the data
  - Introduce Itenant
  - QueryFilter

---

## Slide 28
**Layout:** Sectiekop

### Title: Detecting a tenant

---

## Slide 29
**Layout:** Titel en object

### Title: Detecting a tenant – Tenant.cs

**Code (TextBox 4):**
```csharp
public class Tenant{    public string Id { get; set; }}
```

**Code (TextBox 6):**
```csharp
public class TenantContextAccessor{    public Tenant Tenant { get; set; }}
```

---

## Slide 30
**Layout:** Titel en object

### Title: Detecting a tenant - TenantDependencyExtensions

**Code (TextBox 3):**
```csharp
public static class TenantContextAccessorExtensions{    public static IServiceCollection AddTenantContext(this IServiceCollection services)    {        services.AddScoped<TenantContextAccessor>();        services.AddTransient<Tenant>(p =>            p.GetRequiredService<TenantContextAccessor>().Tenant ?? new Tenant()        );        return services;    }}
```

---

## Slide 31
**Layout:** Titel en object

### Title: Building Middleware

**Code (TextBox 6):**
```csharp
public class TenantMiddleware(TenantContextAccessor tenantContextAccessor) : IMiddleware
{
	public Task InvokeAsync(HttpContext context, RequestDelegate next)
{
var subDomain = context.Request.Host.Host.Split('.')[0];
tenantContextAccessor.Tenant = new()
{
Id = subDomain
};
return next(context);
}
}
```

**Code (TextBox 8):**
```csharp
builder.Services.AddScoped<TenantMiddleware>();
```

---

## Slide 32
**Layout:** Sectiekop

### Title: Reflecting a Tenant

---

## Slide 33
**Layout:** Titel en object

### Title: Reflecting a tenant – Tenant.cs

**Code (TextBox 3):**
```csharp
public class Tenant
{
public string Id { get; set; }
public string Name { get; set; }
public string Color { get; set; }
public string CityImage { get; set; }
}
```

---

## Slide 34
**Layout:** Titel en object

### Title: Reflecting a tenant – appsettings.json

**Text (TextBox 4):**
- "AvailableTenants": {
  - "Tenants": [
    - {
      - "Id": "pallet-town",
      - "Name": "Pallet Town",
      - "Color": "#1a8a78",
      - "CityImage": "https://archives.bulbagarden.net/media/upload/7/77/Pallet_Town_FRLG.png"
      - },
    - {
      - "Id": "vermillion-city",
      - "Name": "Vermillion City",
      - "Color": "#d05535",
      - "CityImage": "https://archives.bulbagarden.net/media/upload/8/8d/Vermilion_City_FRLG.png"
    - }
  - ]
- }

### Speaker Notes

#75d9d7
https://archives.bulbagarden.net/media/upload/7/77/Pallet_Town_FRLG.png

#b956ff
https://archives.bulbagarden.net/media/upload/8/8d/Vermilion_City_FRLG.png

 {      "Id": "pallet-town",      "Name": "Pallet Town",      "BackgroundColor": "#75d9d7",      "BackgroundImage": "https://archives.bulbagarden.net/media/upload/7/77/Pallet_Town_FRLG.png"    },    {      "Id": "vermillion-city",      "Name": "Vermillion City",      "BackgroundColor": "#b956ff ",      "BackgroundImage": "https://archives.bulbagarden.net/media/upload/8/8d/Vermilion_City_FRLG.png"    }

---

## Slide 35
**Layout:** Titel en object

### Title: Reflecting a tenant – program.cs

**Code (TextBox 6):**
```csharp
builder.Services.Configure<AvailableTenants>(    builder.Configuration.GetSection(AvailableTenants.SectionName));
```

**Text (TextBox 7):**
- OR

**Code (TextBox 9):**
```csharp
public class AvailableTenantsSetup(IConfiguration configuration) : IConfigureOptions<AvailableTenants>{    public void Configure(AvailableTenants options)    {        configuration.GetSection(AvailableTenants.SectionName).Bind(options);    }}
```

---

## Slide 36
**Layout:** Titel en object

### Title: Reflecting a tenant – TenantMiddleware.cs

**Code (TextBox 3):**
```csharp
public Task InvokeAsync(HttpContext context, RequestDelegate next)
{
var subDomain = context.Request.Host.Host.Split('.')[0];
var tenant = availableTenants.Value.Tenants.FirstOrDefault(t => t.Id == subDomain);
if(tenant == null)
{
context.Response.StatusCode = (int)HttpStatusCode.NotFound;
return Task.CompletedTask;
}
tenantContextAccessor.Tenant = tenant;
return next(context);
}
```

---

## Slide 37
**Layout:** Titel en object

### Title: Reflecting a tenant – _Layout.cshtml

**Code (TextBox 3):**
```cshtml
<style>
:root {
--tenant-color: @tenant.Color;
--tenant-city-image: url('@tenant.CityImage');
}
</style>
```

---

## Slide 38
**Layout:** Titel en object

### Title: Reflecting a tenant – Index.cshtml

**Code (TextBox 5):**
```cshtml
<div class="gb-city-frame">
	<img src="@tenant.CityImage" />
</div> 
<div class="gb-title-block">
<div class="gb-title-main">POKECENTER</div>
<div class="gb-title-sub">@tenant.Name</div>
</div>
```

**Code (TextBox 7):**
```cshtml
@inject Tenant tenant;
```

---

## Slide 39
**Layout:** Sectiekop

### Title: Protecting a Tenant

---

## Slide 40
**Layout:** Titel en object

### Title: Protecting a tenant – ApplicationUser.cs

**Code (TextBox 3):**
```csharp
public class ApplicationUser : IdentityUser{    public string TenantId { get; set; }}
```

---

## Slide 41
**Layout:** Titel en object

### Title: Protecting a tenant – ApplicationDbContext.cs

**Code (TextBox 3):**
```csharp
public class ApplicationDbContext(    DbContextOptions<ApplicationDbContext> options)    : IdentityDbContext<ApplicationUser>(options){	//…	}
```

---

## Slide 42
**Layout:** Titel en object

### Title: Protecting a tenant – Add Migration

**Code (TextBox 3):**
```bash
dotnet ef migrations add applicationuser -s ../PokeCenter.web  
```

---

## Slide 43
**Layout:** Titel en object

### Title: Protecting a tenant – ApplicationUserStore

**Code (TextBox 3):**
```csharp
public class ApplicationUserStore(    ApplicationDbContext context,    Tenant tenant,    IdentityErrorDescriber? describer = null)    : UserStore<ApplicationUser>(context,        describer){    public override Task<IdentityResult> CreateAsync(ApplicationUser user,        CancellationToken cancellationToken = new CancellationToken())    {        user.TenantId = tenant.Id;        return base.CreateAsync(user,            cancellationToken);    }}
```

---

## Slide 44
**Layout:** Titel en object

### Title: Protecting a tenant – MultiTenantSigninManager

**Code (TextBox 3):**
```csharp
public class MultiTenantSigninManager(    /**/,    Tenant tenant)    : SignInManager<ApplicationUser>(/**/){    public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)    {        if (user.TenantId != tenant.Id)            return Task.FromResult(SignInResult.Failed);        return base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);    }};
```

---

## Slide 45
**Layout:** Titel en object

### Title: Protecting a tenant – Program.cs

**Code (TextBox 3):**
```csharp
builder.Services    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)    .AddEntityFrameworkStores<ApplicationDbContext>()    .AddUserStore<ApplicationUserStore>()    .AddSignInManager<MultiTenantSigninManager>()    ;
```

---

## Slide 46
**Layout:** Titel en object

### Title: Protecting a tenant – LoginPartial.cshtml

**Code (TextBox 3):**
```cshtml
@* @inject SignInManager<ApplicationUser> SignInManager *@@inject MultiTenantSigninManager SignInManager@inject UserManager<ApplicationUser> UserManager
```

---

## Slide 47
**Layout:** Sectiekop

### Title: Protecting the data

**Text (Text Placeholder 2):**
- (of a tenant)

---

## Slide 48
**Layout:** Titel en object

### Title: Protecting the data – Itenanted.cs

**Code (TextBox 4):**
```csharp
public interface ITenanted{    public string TenantId { get; set; }}
```

---

## Slide 49
**Layout:** Titel en object

### Title: Protecting the data – HealingPod.cs

**Code (TextBox 3):**
```csharp
public class HealingPod : ITenanted{    public int Id { get; set; }    public int MaxCapacity { get; set; }    public List<PokemonAdmission> Admission { get; set; }    public string TenantId { get; set; }}
```

---

## Slide 50
**Layout:** Titel en object

### Title: Protecting the data – PokemonAdmission

**Code (TextBox 3):**
```csharp
public class PokemonAdmission : ITenanted{    public int Id { get; set; }//…
    public string TenantId { get; set; }}
```

---

## Slide 51
**Layout:** Titel en object

### Title: Protecting the data –  TenantIdValueGenerator

**Code (TextBox 3):**
```csharp
public class TenantIdValueGenerator : ValueGenerator<string>{    public override string Next(EntityEntry entry)    {        if (entry.Entity is ITenanted && entry.Context is ApplicationDbContext applicationDbContext)        {            return applicationDbContext.Tenant;        }        throw new InvalidOperationException("TenantId is expected to be set on this entity!");    }    public override bool GeneratesTemporaryValues { get; } = false;}
```

---

## Slide 52
**Layout:** Titel en object

### Title: Protecting the data – ApplicationDbContext

**Code (TextBox 4):**
```csharp
builder.Entity<HealingPod>(entity =>
{
entity.HasKey(e => new { e.Id });
entity
	.Property(e => e.TenantId)
	.IsRequired()
	.HasValueGenerator<TenantIdValueGenerator>();
entity.HasIndex(e => e.TenantId);
entity.HasQueryFilter(e => e.TenantId == TenantId);
});
```

---

## Slide 53
**Layout:** Titel en object

### Title: Protecting the data – ApplicationDbContext

**Code (TextBox 4):**
```csharp
builder.Entity<PokemonAdmission>(entity =>
{
entity.HasKey(e => new { e.Id });
entity
	.Property(e => e.TenantId)
.IsRequired()
.HasValueGenerator<TenantIdValueGenerator>();
entity.HasIndex(e => e.TenantId);
entity.HasQueryFilter(e => e.TenantId == TenantId);
});
```

---

## Slide 54
**Layout:** Sectiekop

### Title: Keeping it DRY

**Text (Text Placeholder 2):**
- (with magic 🪄)

---

## Slide 55
**Layout:** Titel en object

### Title: Keeping it DRY (with magic 🪄)

**Code (TextBox 4):**
```csharp
protected override void OnModelCreating(ModelBuilder builder){    var tenantedModels = builder.Model.GetEntityTypes()        .Where(e => typeof(ITenanted).IsAssignableFrom(e.ClrType));    foreach (var tenantedModel in tenantedModels)    {        builder.Entity(tenantedModel.ClrType)            .HasQueryFilter<ITenanted>(e => e.TenantId == Tenant)            .HasIndex(nameof(ITenanted.TenantId))            ;        builder.Entity(tenantedModel.ClrType)            .Property(nameof(ITenanted.TenantId))            .IsRequired()            .HasValueGenerator<TenantIdValueGenerator>();    }    builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);    base.OnModelCreating(builder);}
```

---

## Slide 56
**Layout:** Titel en object

### Title: Keeping it DRY (with magic 🪄)

**Code (TextBox 4):**
```csharp
public static class QueryFilterExtensions
{
public static EntityTypeBuilder HasQueryFilter<T>(
this EntityTypeBuilder entityTypeBuilder,
Expression<Func<T, bool>> filter
)
{	
var param = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
var body = ReplacingExpressionVisitor
.Replace(
	filter.Parameters.Single(), 
	param, 
	filter.Body
);
var lambda = Expression.Lambda(body, param);
return entityTypeBuilder.HasQueryFilter(lambda);
}
}
```

---

## Slide 57
**Layout:** Sectiekop

### Title: Shameless self promotion

**Text (Text Placeholder 2):**
- For charity and profit!

---

## Slide 58
**Layout:** Leeg

**Image (Picture 2):** [image/png, size: 5.3" x 7.5"]

---

## Slide 59
**Layout:** Leeg

**Image (Picture 2):** [image/png, size: 5.3" x 7.5"]

---

## Slide 60
**Layout:** Titel en object

### Title: Where to find stuff

**Text (Content Placeholder 1):**
- Code on github
  - https://github.com/RubMertens/presentation-multi-tenant-apps

---
