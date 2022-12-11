using DesafioAutoGlass.API.Configuration;
using DesafioAutoGlass.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddServicesConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x=> 
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        x.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
