using HospitalAppointmentSystem.Api.Extension;
using HospitalAppointmentSystem.CrossCutting.Logger.Abstract;
using HospitalAppointmentSystem.CrossCutting.Logger.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddDatabaseServices()
    .AddDataAccessServices()
    .AddApplicationServices()
    .AddCrossCuttingServices();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var loggerService = app.Services.GetRequiredService<ILoggerService>();
GlobalLogger.Configure(loggerService);

app.UseApplicationMiddleware();
app.MapControllers();

app.Run();