/* SPDX-License-Identifier: MIT
 * Copyright (c) 2026 Florian Wilhelm
 * Description: ASP.NET application startup and HTTP pipeline configuration.
 */
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

// Create the application builder and load config, logging, and DI defaults.
var builder = WebApplication.CreateBuilder(args);

// Register the Entity Framework Core database context
builder.Services.AddDbContext<Server.Data.ApplicationDbContext>(options =>
{
    // Tell EF Core to use PostgreSQL for this database context with the connection string from config.
    var conn = builder.Configuration.GetConnectionString("Default");
    options.UseNpgsql(conn);
});

// Configure JSON responses returned by minimal API endpoints.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Omit null properties from JSON output to keep responses smaller and cleaner.
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Build the application pipeline from the configured services and middleware.
var app = builder.Build();

// Serve static files from wwwroot (exported frontend)
app.UseDefaultFiles();
app.UseStaticFiles();
// Send all unmatched non-file routes to index.html so frontend routing works.
app.MapFallbackToFile("index.html");

// Lightweight health check endpoint for local testing and container probes.
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// Start listening for incoming HTTP requests.
app.Run();
