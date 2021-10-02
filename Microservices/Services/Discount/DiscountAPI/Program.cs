using DiscountAPI.Extensions;
using DiscountAPI.Repositories;
using DiscountAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DiscountAPI", Version = "v1" });
});

var app = builder.Build();
app.MigrateDatabase<Program>(50);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiscountAPI v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
