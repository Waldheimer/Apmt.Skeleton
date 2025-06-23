using appointmenting;
using appointmenting.AspNetCore;
using appointmenting.API.Extensions;
using Scalar.AspNetCore;
using Serilog;
using System.Text;
using appointmenting.application;
using appointmenting.data_access;

var builder = WebApplication.CreateBuilder(args);

//  Add OpenApi Support
builder.Services.AddOpenApi();
//  --------------------

//  --- Configure Serilog ---
Console.OutputEncoding = Encoding.UTF8;
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
var logger = builder.Services.CurrentLogger<Program>();
logger.LogInformation("Starting Appointmenting API v1");
//  --------------------

//  Add Custom DIs
builder.Services.AddApiServices();
builder.Services.AddApplication();
builder.Services.AddDataAccess();
//  --------------------


var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseApiEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //  Use the new Scalar instead of Swagger
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.Run();

