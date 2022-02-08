using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MiddlewarePracticeWebApi.Middlewares;
using System.Diagnostics;

namespace MiddlewarePracticeWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewarePracticeWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. (Pipeline: Arka arkaya çalýþan iþlemler, sýra önemlidir)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewarePracticeWebApi v1"));
            }

            app.UseHttpsRedirection(); 

            app.UseRouting(); 

            app.UseAuthorization(); 

            app.UseDemoMiddleware();

            app.Map("/example", internalApp => 
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("/example Route Middleware tetiklendi!"); // https://localhost:44307/example
                    await context.Response.WriteAsync("/example Route Middleware, Context içerisindeki Response'a mesaj yazýlýyor!");
                }));


            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            {
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("MapWhen ile GET isteði yapýlan Metotlar için Middleware tetiklendi!");
                    await context.Response.WriteAsync("MapWhen ile GET isteði yapýlan Metotlar için Middleware tetiklendi ve Context Response'una mesaj yazýlýyor");
                });
            });

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllers();
            });
        }
    }
}