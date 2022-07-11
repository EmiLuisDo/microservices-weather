using CloudWeather.Precipitation.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PrecipDbContext>(
    opts => {
        opts.EnableSensitiveDataLogging ();
        opts.EnableDetailedErrors();
        opts.UseNpgsql(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);

var app = builder.Build();

app.MapGet("/observation/{zip}", async (string zip, [FromQuery] ushort  ? days, PrecipDbContext db) => {
    if (days == null || days > 30 ){
        return Results.BadRequest("Please provide a 'days' query parameter between 1 and 30");
    }  
    DateTime startDate = DateTime.UtcNow - TimeSpan.FromDays(days.Value);
    List <Precipitation> results = await db.Precipitation
        .Where( precip => precip.ZipCode == zip && precip.CreateOn > startDate ).ToListAsync();
    return Results.Ok(results);
} );

app.Run();
