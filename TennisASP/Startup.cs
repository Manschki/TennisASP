using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisDB;
using TennisASP.Services;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace TennisASP
{
    public class Startup
    {
        private readonly string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var dataDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var conString = Configuration.GetConnectionString("Tennis");

            if (conString.Contains("|DataDirectory|"))
                conString = conString.Replace("|DataDirectory|", dataDirectory);

            services.AddDbContext<TennisContext>(options =>
                options.UseSqlServer(conString, o => o.EnableRetryOnFailure())
            );
            services.AddScoped<PersonService>();
            services.AddScoped<BookingService>();

            services.AddCors(options =>
            {
                options.AddPolicy(myAllowSpecificOrigins,
              x => x.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(myAllowSpecificOrigins);

            app.UseMvc();

            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        await context.Response.WriteAsync(
                            $"Exception:{ error.Error.Message} { error.Error?.InnerException.Message} "
                                );
                    }
                });
            });
        }
    }
}
