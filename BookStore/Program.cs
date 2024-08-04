using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStore.Interfaces;
using BookStore.Services;
using Microsoft.OpenApi.Models;
using BookStore.Helpers;
using Microsoft.AspNetCore.Mvc;
using BookStore.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerConfig =>
{
    swaggerConfig.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Online Book Store",
        Version = "v1"
    });
    swaggerConfig.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    swaggerConfig.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IAddressService, AddressService>();
builder.Services.AddSingleton<IOrderHelper, OrderHelper>();
builder.Services.AddSingleton<IUserHelper, UserHelper>();
builder.Services.AddSingleton<ICheckoutHelper, CheckoutHelper>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication();

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(builder.Configuration.GetValue<string>("JWT_Secret"))
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add(ResponseCacheProfiles.CacheCommon,
        new CacheProfile()
        {
            Duration = 180,
            Location = ResponseCacheLocation.Any
        });
    option.CacheProfiles.Add(ResponseCacheProfiles.CacheVaryById,
       new CacheProfile()
       {
           Duration = 180,
           Location = ResponseCacheLocation.Any,
           VaryByQueryKeys = ["id"]
       });

});

builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseResponseCaching();

app.Run();
