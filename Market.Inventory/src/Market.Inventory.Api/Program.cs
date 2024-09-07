using Market.Inventory.Api.SeedData;
using Market.Inventory.IoC.Configuration;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isProduction = builder.Environment.IsProduction();

builder.Services
    .ConfigAppConnectionString(builder.Configuration, isProduction)
    .ConfigAppServices()
    .ConfigAppDependencyInjection()
    .ConfigRabbitMQ(builder.Configuration);

builder.Services.AddSingleton(ConfigMapper.ConfigAppMapper());

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedData.Seeder(app, isProduction);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
