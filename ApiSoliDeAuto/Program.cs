using Application.Interfaces;
using Domain.Entities;
using InfraEstructure.Service;
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

// Cargar la configuración desde appsettings.Development.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

builder.Configuration.AddConfiguration(configuration);

// Obtener la configuración de RabbitMQ
var rabbitMQConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMQConfig>();
// Registrar el servicio de RabbitMQ con la configuración proporcionada
builder.Services.AddSingleton<IMessageService>(new MessageService(rabbitMQConfig.HostName, rabbitMQConfig.QueueName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
