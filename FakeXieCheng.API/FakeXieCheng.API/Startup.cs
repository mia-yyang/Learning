using FakeXieCheng.API.Database;
using FakeXieCheng.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FakeXieCheng.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = false;
                // ¿œ–¥∑®
                //setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            })
            .AddXmlDataContractSerializerFormatters();

            //services.AddTransient<ITouristRouteRepostitory, MockTouristRouteRepostitory>();
            services.AddTransient<ITouristRouteRepostitory, TouristRouteRepostitory>();

            //services.AddDbContext<AppDbContext>(option => { },);

            //)



            //UseMySql(Configuration["ConnectionStrings:Default"])
            //services.AddDbContextPool<AppDbContext>(options => options
            // .UseMySql("Server=localhost;Database=test;User=root;Password=woshishui;", mySqlOptions => mySqlOptions
            // .ServerVersion(new ServerVersion(new Version(8, 0, 25), ServerType.MySql))
            //));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapControllers();
            });
        }
    }
}
