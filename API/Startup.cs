using System.Collections.Generic;
using API.Core;
using API.Core.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repostiroy;

namespace API;

public class Startup {

  public Startup(IConfiguration configuration) {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; }

  public void ConfigureServices(IServiceCollection services) {
    services.AddControllers();
    services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
          Description = "Jwt auth header",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement{
          {
            new OpenApiSecurityScheme {
              Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header
            },
            new List<string>()
          }
        });
      });
      services.AddCors();
      services.Configure<MongoSettings>(
        opt => {
          opt.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
          opt.Database = Configuration.GetSection("MongoDb:Database").Value;
        }
      );

      services.AddSingleton<MongoSettings>();
      services.AddTransient<IAuthorContext, AuthorContext>();
      services.AddTransient<IAuthorRepository, AuthorRepository>();
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    // app.UseMiddleware<ExceptionMiddleware>();
    if (env.IsDevelopment()) {
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
    }

    app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors(opt => {
        opt.AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()
          .WithOrigins("http://localhost:3000");
      });

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
  }

}