using Microsoft.OpenApi.Models;
using Repositories;
using System.Reflection;
using TransportersApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TransporterAPI",
        Version = "v1",
        Description = "Distribución de artículos desde centros de acopio",
        Contact = new OpenApiContact{
            Name = "Gilmar Ocampo Nieves",
            Email = "gilmarocamponieves@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gilmar-ocampo-nieves/")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Inject dependences
builder.Services.AddDependencyRepositories();
builder.Services.AddDependencyBusiness();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
