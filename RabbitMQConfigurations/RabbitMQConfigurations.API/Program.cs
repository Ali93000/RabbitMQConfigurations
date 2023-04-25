using RabbitMQConfigurations.API.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register app Settings
builder.Services.AddAppSettingsConfigurations(builder.Configuration);
// Add Infrastructure Configurations
builder.Services.AddInfrastructureConfigurations();
// Add BLL Services
builder.Services.AddServicesConfigurations();
// Add DB Context
builder.Services.AddDBContextConfigurations(builder.Configuration);
// Add Mediator
builder.Services.AddMediatorConfigurations();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
