using Microsoft.Extensions.Logging.ApplicationInsights;
using Unidas.MS.Maintenance.Services.Api.Configuration;
using Unidas.MS.Maintenance.Services.Api.Filters.Traces;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    string key = hostingContext.Configuration
                               .GetSection("ApplicationInsights:InstrumentationKey")
                               .Value;
    if (!String.IsNullOrEmpty(key))
    {
        logging.AddApplicationInsights(key);
        logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
        logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
    }
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddDependecyInjectionConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationInsightsConfiguration();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddMvc(config =>
{
    config.Filters.Add(typeof(GlobalControllerAppInsightsAttribute));
});

var app = builder.Build();

app.UseExceptionHandler("/error");

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

