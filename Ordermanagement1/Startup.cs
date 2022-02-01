using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordermanagement1
{
    public class Startup
    {
        private IConfiguration conf;
        public Startup(IConfiguration conf)
        {
            this.conf = conf;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("MiddleWare1: Incoming request\n");
                await next();
                await context.Response.WriteAsync("MiddleWare1: Outgoing request\n");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("MiddleWare2: Incoming request\n");
                await next();
                await context.Response.WriteAsync("MiddleWare2: Outgoing request\n");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("MiddleWare3: Incoming request\n");
                await next();
                await context.Response.WriteAsync("MiddleWare3: Outgoing request\n");
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(conf.GetConnectionString("MYDB")+"\n");
                    //await context.Response.WriteAsync("Hi SUDHEER, Have a very nice day\n " + System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                });
            });
        }
    }
}
