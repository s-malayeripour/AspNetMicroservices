using BasketAPI.Repositories;
using BasketAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();

builder.Services.AddControllers();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
string cacheConnectionString = configuration.GetValue<string>("CacheSettings:ConnectionString");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheConnectionString;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BasketAPI", Version = "v1" });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketAPI v1"));
}
app.UseAuthorization();
app.MapControllers();
app.Run();