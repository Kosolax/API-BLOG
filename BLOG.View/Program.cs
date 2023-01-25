using BLOG.DataAccess;
using BLOG.Services.Interfaces;
using BLOG.Services;

using Microsoft.EntityFrameworkCore;
using BLOG.Services.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BLOG.Entities;

var builder = WebApplication.CreateBuilder(args);

// Make the connection to the database with the connection string
ConfigurationManager configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("BlogDbConnnection");
builder.Services.AddDbContext<BlogContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure Identity rules
builder.Services.AddIdentity<UserEntity, IdentityRole>(opt =>
{
    // TODO
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = true;
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<BlogContext>();

// Configure JWT Token
var jwtSettings = builder.Configuration.GetSection("JWTSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});


// Register Handlers
builder.Services.AddScoped<JwtHandler>();

// Register Services
builder.Services.AddScoped<IAccountsService, AccountsService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Create database en play seeds
using IServiceScope serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
bool isProduction = Convert.ToBoolean(configuration["IsProduction"]);
serviceScope.ServiceProvider.GetService<BlogContext>().Database.Migrate();
serviceScope.ServiceProvider.GetService<BlogContext>().EnsureSeedData(isProduction).GetAwaiter().GetResult();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();