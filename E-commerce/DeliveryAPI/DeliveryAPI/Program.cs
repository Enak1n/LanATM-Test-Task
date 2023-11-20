using DeliveryAPI.Consumers;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Helpers;
using DeliveryAPI.Infrastructure.DataBase;
using DeliveryAPI.Infrastructure.UnitOfWork;
using DeliveryAPI.Service.Business;
using DeliveryAPI.Service.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rabbit;
using System.Data;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var databaseConnection = builder.Configuration.GetConnectionString("DbConnection");

var secretKey = builder.Configuration.GetSection("JwtSettings:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value;
var audience = builder.Configuration.GetSection("JwtSettings:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(databaseConnection, b => b.MigrationsAssembly("DeliveryAPI")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AccessToDeliveries", builder =>
    {
        builder.RequireRole(
            Role.Salesman.ToString(),
            Role.Courier.ToString(),
            Role.Buyer.ToString());
    });

    options.AddPolicy("ChangingOfDeliveries", builder =>
    {
        builder.RequireRole(Role.Salesman.ToString());
    });

    options.AddPolicy("Courier", builder =>
    {
        builder.RequireRole(Role.Courier.ToString());
    });
});

var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateCourierConsumer>();
    x.AddConsumer<CreateDeliveryConsumer>();
    x.AddConsumer<CancelDeliveryConsumer>();

    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(rabbitMQSettings["Host"], settings =>
        {
            settings.Username(rabbitMQSettings["Username"]);
            settings.Password(rabbitMQSettings["Password"]);
        });

        cfg.ReceiveEndpoint("createCourierQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 3000));
            ep.ConfigureConsumer<CreateCourierConsumer>(provider);
        });

        cfg.ReceiveEndpoint("cancelDeliveryQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 3000));
            ep.ConfigureConsumer<CancelDeliveryConsumer>(provider);
        });

        cfg.ReceiveEndpoint("createDeliveryQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 3000));
            ep.ConfigureConsumer<CreateDeliveryConsumer>(provider);
        });

    }));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<ICourierService, CourierService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter access token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
          },
          new List<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
