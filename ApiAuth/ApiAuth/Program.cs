using ApiAuth;
using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

// Isso já tava
var builder = WebApplication.CreateBuilder(args);

// Configuração para habilitar autenticação
var key = Encoding.ASCII.GetBytes(Setting.Secret);

builder.Services.AddAuthentication(x =>
{
    //Definir o esquema de autenticação
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // É a forma como ele vai interrogar a requisiação para saber como lidar e saber onde está o token
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    // Configurar os parametros do Token
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // Validar a chave de assinatura
        IssuerSigningKey = new SymmetricSecurityKey(key), // Como ele valida essa chave
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Configuração para tratar autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("gerente"));
    options.AddPolicy("Estagiario", policy => policy.RequireRole("estagiario"));
});

// Isso já tava
var app = builder.Build();

// Precisa addd nessa ordem
app.UseAuthentication();
app.UseAuthorization();


// Metodos
app.MapPost("/login", (User UmUsuario) => {
    // ir no banco buscar se o usuario existe e se a senha é igual

    var usuario = UserRepository.Get(UmUsuario.UserName, UmUsuario.Password);

    if (usuario == null)
    {
        return Results.NotFound(new {message = "Usuario ou senha inválido"});
    }
    var token = TokenService.GenerateToken(usuario);

    return Results.Ok(token);
});

app.MapGet("/teste", () => 
{
    return Results.Ok("Funcionou sem autenticar!");

});

app.MapGet("/autenticado", () =>
{
    return Results.Ok("Funcionou Autenticado!");

}).RequireAuthorization();

app.MapGet("/saldo", (ClaimsPrincipal user) =>
{
    //Buscar algo no banco referente ao usuário,
    // posso usar esse argumento ClaimsPrincipal para obter
    // Informações do usuário contidas no TOken
    // EX: user.Identity.Name;
    return Results.Ok("Saldo Atual: R$ 230,00");

}).RequireAuthorization("Admin");


// Isso já tava
app.Run();