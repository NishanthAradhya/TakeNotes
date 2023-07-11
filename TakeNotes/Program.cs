using BLL.Services.NoteService;
using DAL.DALServices.NoteDAL;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TakeNotes.Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //Swagger for API documentation
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Description = "A simple ASP.NET WEB API application to Create, Read, Edit and Delete Notes",
                Title = "Take Note",
                Contact= new OpenApiContact { Name="Nishanth Aradhya",Email="nishanth.aradhya@gmail.com",Url=new Uri("https://www.linkedin.com/in/nishanth-aradhya-648911108/") }
            });
            var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
        builder.Logging.AddFilter("System", LogLevel.Debug);

        //register services for Dependency injection
        builder.Services.AddTransient<INoteService, NoteService>();
        builder.Services.AddTransient<INoteDALService, NoteDALService>();

        //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        var app = builder.Build();
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}