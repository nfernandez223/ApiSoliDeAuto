using Application.Interfaces;
using Domain.Entities;
using InfraEstructure.Persistence;
using InfraEstructure.Service;
using InfraEstructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Solicitud Payments", Version = "v1" });

    c.MapType<Solicitud>(() => new OpenApiSchema
    {
        Type = "object",
        Required = new HashSet<string> { "Cliente", "Tipo", "Monto", "TipoCliente"},
        Properties = new Dictionary<string, OpenApiSchema>
        {
            { "Cliente", new OpenApiSchema { Type = "string", Example = new OpenApiString("Cliente") } },
            { "Tipo", new OpenApiSchema { Type = "string", Example = new OpenApiString("Cobro")  } },
            { "Monto", new OpenApiSchema { Type = "number", Example = new OpenApiDouble(100)} },
            { "TipoCliente", new OpenApiSchema { Type = "integer", Example = new OpenApiInteger(1) } }
        }
    });
});

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

builder.Configuration.AddConfiguration(configuration);

var rabbitMQConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMQConfig>();

builder.Services.AddSingleton<IMessageService>(new MessageService(rabbitMQConfig.HostName, rabbitMQConfig.QueueName));
builder.Services.AddSingleton<IContextMgmt, ContextMgmt>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
