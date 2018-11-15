using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddScoped<IAuthRepository, AuthRepository>();
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

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        //app.UseHsts();
      }

      //app.UseHttpsRedirection();
      app.UseCors(builder =>
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
