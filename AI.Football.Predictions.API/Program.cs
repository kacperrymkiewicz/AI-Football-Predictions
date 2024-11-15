using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.API.Repositories.Authentication;
using AI.Football.Predictions.API.Services.FootballDataService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        Description = "Standard Authorization header using the Bearer scheme. Example: bearer {token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<DataContext>();

builder.Services.Configure<FootballDataSettings>(
    builder.Configuration.GetSection("FootballData"));

builder.Services.AddHttpClient("FootballData", client =>
{
    var footballDataSettings = builder.Configuration.GetSection("FootballData").Get<FootballDataSettings>();
    client.BaseAddress = new Uri(footballDataSettings!.BaseUrl);
    client.DefaultRequestHeaders.Add("X-Auth-Token", footballDataSettings.ApiKey);
});

builder.Services.AddScoped<IFootballDataService, FootballDataService>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(x => x
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
