using System;
using System.Text;
using Gestion.Application.Services.Implementations;
using Gestion.Application.Services.Interfaces;
using Gestion.Core.Entities;
using Gestion.Core.Interfaces;
using Gestion.Core.Services.Implementations;
using Gestion.Core.Services.Interfaces;
using Gestion.Infrastructure;
using Gestion.Infrastructure.Data;
using Gestion.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//configuration de la base de données
builder.Services.AddDbContext<GestionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql =>
        {
            sql.MigrationsAssembly("Gestion.Infrastructure");
            //sql.EnableRetryOnFailure();
        })
    //Log console 

    .EnableSensitiveDataLogging() // Affiche les valeurs dans les logs (⚠️ à désactiver en prod)
    .LogTo(Console.WriteLine, LogLevel.Information); // Affiche les requêtes SQL

});

//configuration de l'identité
builder.Services.AddIdentity<User, Role>()
       .AddRoles<Role>()
       .AddEntityFrameworkStores<GestionDbContext>() //stockage des données dans la base de données
       .AddDefaultTokenProviders(); //ajout des jetons par défaut
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
//builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IRoomsRepository, RoomsRepository>();
builder.Services.AddScoped<IRoomService, RoomsService>();
builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));




//.AddDefaultTokenProviders();
//configuration jwt
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


// Add services to the container.

var app = builder.Build();
// Initialisation des rôles au démarrage de l'application
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await SeedRole.Initialize(scope.ServiceProvider, roleManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
