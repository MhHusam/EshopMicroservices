var builder = WebApplication.CreateBuilder(args);


//Add services to the Container 
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
}
);

//Before Building
var app = builder.Build();
//After Building



//Configure the http request pipeline 

app.MapCarter();



app.Run();
