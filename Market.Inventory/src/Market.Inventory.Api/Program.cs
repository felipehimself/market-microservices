using Market.Inventory.CrossCutting.Configs;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigAppConnectionString(builder.Configuration, builder.Environment.IsProduction());
builder.Services.ConfigAppServices();
builder.Services.ConfigAppDependencyInjection();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
