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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. (Pipeline: Arka arkaya �al��an i�lemler, s�ra �nemlidir)
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
                    await context.Response.WriteAsync("/example Route Middleware, Context i�erisindeki Response'a mesaj yaz�l�yor!");
                }));


            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            {
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("MapWhen ile GET iste�i yap�lan Metotlar i�in Middleware tetiklendi!");
                    await context.Response.WriteAsync("MapWhen ile GET iste�i yap�lan Metotlar i�in Middleware tetiklendi ve Context Response'una mesaj yaz�l�yor");
                });
            });

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllers();
            });
        }
    }
}