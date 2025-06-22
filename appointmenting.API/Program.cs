using appointmenting;
using appointmenting.AspNetCore;
using appointmenting.API.Extensions;
using Scalar.AspNetCore;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//var logger = builder.Services.CurrentLogger<Program>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

Console.OutputEncoding = Encoding.UTF8;
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
var logger = builder.Services.CurrentLogger<Program>();
logger.LogInformation("Starting Appointmenting API v1");

builder.Services.AddApiServices();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseApiEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.Run();

