using BondDesk.BondProvider;
using BondDesk.DateTimeProvider;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Services;
using BondDesk.GiltsInIssueRepo;
using BondDesk.QuoteProvider;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy => policy
            .WithOrigins("http://localhost:5084")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "BondDesk API", Version = "v1" });
});

builder.Services.AddScoped<IGiltsService, GiltsService>();
builder.Services.AddScoped<IGiltRepo, Gilts>();
builder.Services.AddSingleton<IQuoteRepo, CachedRepo>();
builder.Services.AddScoped<IDateTimeProvider, SimpleDateTimeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "BondDesk API v1");
		});
	}
}

// Enable CORS before authorization
app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
