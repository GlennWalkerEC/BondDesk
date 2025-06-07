using BondDesk.BondProvider;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Services;
using BondDesk.GiltsInIssueRepo;
using Microsoft.OpenApi.Models;
using Portfolio.QuoteProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "BondDesk API", Version = "v1" });
});

builder.Services.AddScoped<IGiltsService, GiltsService>();
builder.Services.AddScoped<IGiltRepo, GiltSymbols>();
builder.Services.AddScoped<IQuoteRepo, LseQuoteRepo>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
