using AutoMapper;
using JWTAuthenticationTest.Data;
using JWTAuthenticationTest.Models;
using JWTAuthenticationTest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AuthenticationTest>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProFile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<UserService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//JWT Authentication start
var jwtIssuer = builder.Configuration.GetSection("JWT:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("JWT:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {      
options.TokenValidationParameters = new TokenValidationParameters
{
  ValidateIssuer = true,
  ValidateAudience = true,
  ValidateLifetime = true,
  ValidateIssuerSigningKey = true,
  ValidIssuer = jwtIssuer,
  ValidAudience = jwtIssuer,
  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
};
});

//JWT Authorize start
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
Description = "Enter like vbnmbgvhj",
Name = "Authorization",
In = ParameterLocation.Header,
Type = SecuritySchemeType.ApiKey,
Scheme = "Bearer",
});
x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference=new OpenApiReference
               {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer",
               },
            },
            Array.Empty<string>()
        },
    });
});
builder.Services.AddCors(opt =>
{
opt.AddPolicy("CorsPolicy", builder =>
builder.AllowAnyOrigin().AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
