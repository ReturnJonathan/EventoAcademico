using EventoAcademico.Api.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
namespace EventoAcademico.Api { 
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuración de DbContext con PostgreSQL
          builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(
                  builder.Configuration.GetConnectionString("AppDbContext")
             )
             );
        // Configuracion de DBContext con MySQL
       //       builder.Services.AddDbContext<AppDbContext>(options =>
        //options.UseMySql(
         // builder.Configuration.GetConnectionString("AppDbContext"),
         //ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("AppDbContext"))
       // )
        //);

        //builder.Services.AddDbContext<AppDbContext>(options =>
        //options.UseSqlServer(
        //builder.Configuration.GetConnectionString("AppDbContext")
         //   )
          //  );

        builder.Services
        .AddControllers()
        .AddNewtonsoftJson(
            opts => opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        );


        // Agregar los servicios necesarios para los controladores de API
        builder.Services.AddControllers();  // Añadido AddControllers

        // Configuración de Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configurar autorización
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configurar el middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Autoriza las peticiones
        app.UseAuthorization();

        // Mapea los controladores
        app.MapControllers();  // Asegúrate de que los controladores se mapeen correctamente

        app.Run();
    }
}
}