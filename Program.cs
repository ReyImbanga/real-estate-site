using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Security;
using RealEstateWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

//un nouveau builder pour la validation
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireClientProfile", policy =>
        policy.Requirements.Add(new ClientProfileRequirement()));

    options.AddPolicy("RequireOwnerProfile", policy =>
        policy.Requirements.Add(new OwnerProfileRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, ClientProfileHandler>();
builder.Services.AddScoped<IAuthorizationHandler, OwnerProfileHandler>();
builder.Services.AddScoped<IClaimsTransformation, OwnerClaimsTransformation>();
builder.Services.AddScoped<ListingPriceService>();


//====================================

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

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
