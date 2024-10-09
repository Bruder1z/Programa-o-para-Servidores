using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinhaLoja;
using MinhaLoja.Data;
using MinhaLoja.Services;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(); // Já estava
builder.Services.AddTransient<TokenService>();


var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();



app.MapGet("/", () => "Hello World!");

app.Run();
