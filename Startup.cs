using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using localmock.Mock;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace localmock
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(b =>
            {
                b.AllowAnyHeader();
                b.AllowAnyMethod();
                b.AllowAnyOrigin();
                b.AllowCredentials();
            });


            app.MapWhen(context => context.Request.Path.ToString().Contains("/user/login"), configuration =>
            {

                configuration.Run(ctx =>
              {
                  ctx.Response.ContentType = "application/json";
                  return ctx.Response.WriteAsync(JsonConvert.SerializeObject(new { code = 20000, data = new { token = "admin" } }));
              });
            });
            app.MapWhen(context => context.Request.Path.ToString().Contains("/user/logout"), configuration =>
            {

                configuration.Run(ctx =>
                {
                    ctx.Response.ContentType = "application/json";
                    return ctx.Response.WriteAsync(JsonConvert.SerializeObject(new { code = 20000 }));
                });
            });


            app.MapWhen(context => context.Request.Path.ToString().Contains("/user/info"), configuration =>
            {
                configuration.Run(ctx =>
              {
                  var str = "{\"code\":20000,\"data\":{\"roles\":[\"admin\"],\"name\":\"admin\",\"avatar\":\"https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif\"}}";
                  return ctx.Response.WriteAsync(str);
              });
            });

            app.MapWhen(context => context.Request.Path.ToString().Contains("/article/list"), configuration =>
            {

                configuration.Run(ctx =>
                {
                    ctx.Response.ContentType = "application/json";
                    var page = int.Parse(ctx.Request.Query["page"]);
                    var limit = int.Parse(ctx.Request.Query["limit"]);
                    var title = ctx.Request.Query["title"];
                    return ctx.Response.WriteAsync(JsonConvert.SerializeObject(new { code = 20000, data = new {
                         total = ArticleMock.Total(),
                         items = ArticleMock.List(page, limit, title) } }));
                });
            });


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
