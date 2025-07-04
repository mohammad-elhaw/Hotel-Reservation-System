using Hangfire;
using HotelReservation.API;
using HotelReservation.Application.Jobs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // Disable default model state validation
    })
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddApplicationPart(typeof(AssemblyReference).Assembly);

builder.Services.AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<RoomAvailabilityChecker>(
    "checkAndFreeRooms",
    job => job.CheckAndFreeRooms(),
    "*/15 * * * *");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
