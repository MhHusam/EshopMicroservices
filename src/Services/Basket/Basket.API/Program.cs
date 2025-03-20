using Basket.API.Data;
using BuildingBlocks.Behaviors;
using Marten;

var builder = WebApplication.CreateBuilder(args);

 


//Add services to the Container 
var assembly=typeof (Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
}
);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
}
).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.Decorate<IBasketRepository, CashBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options => options.Configuration
= builder.Configuration.GetConnectionString("Redis"));

var app = builder.Build();
app.MapCarter();


app.Run();
