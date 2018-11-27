using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DatingApp.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
        .ConfigureWarnings(warnings =>
          warnings.Ignore(CoreEventId.IncludeIgnoredWarning))
      );

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddJsonOptions(opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling =
            ReferenceLoopHandling.Ignore;
        });

      services.AddAutoMapper();
      services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
      services.AddTransient<Seed>();
      services.AddScoped<IAuthRepository, AuthRepository>();
      services.AddScoped<IDatingRepository, DatingRepository>();
      services.AddScoped<LogUserActivity>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
          };
        });
      services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler(builder =>
        {
          builder.Run(async context =>
          {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
              context.Response.AddApplicationError(error.Error.Message);
              await context.Response.WriteAsync(error.Error.Message);
            }
          });
        });
        //app.UseHsts();
      }

      // seeder.SeedUsers();
      // app.UseHttpsRedirection();

      // app.UseCors(x =>
      //   x.WithOrigins("http://localhost:4200")
      //   .AllowAnyHeader()
      //   .AllowAnyMethod());

      app.UsePathBase("/datingapp");
      app.UseAuthentication();
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseForwardedHeaders (new ForwardedHeadersOptions {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });

      app.UseMvc(routes =>
      {
        routes.MapSpaFallbackRoute(
          name: "spa-fallback",
          defaults : new { controller = "Fallback", action = "index" }
        );
      });
    }
  }
}
