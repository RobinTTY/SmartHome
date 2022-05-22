using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using SmartHome.Database.Utility;

var builder = WebApplication.CreateBuilder(args);

// Setup Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<ApplicationModule>();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // required to deserialize NaN, Infinity, etc.
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var options = new JsonSerializerOptions
{
    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
};


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);
app.MapControllers();

app.Run();
