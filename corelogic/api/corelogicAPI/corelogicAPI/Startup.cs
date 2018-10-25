using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace corelogicAPI
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
            services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(
                    options => options.WithOrigins("http://localhost:3000").AllowAnyMethod() // ATTENTION!! this allows cross-origin sources from localhost:3000, which is by default gulp local server port, if gulp doesn't run on this port, force it!
                );
            }

            app.Use(async (context, next) => {
                await next();
                // redirect 404 and non-API calls to somewhere, it could be a static page or the main domain...
                if ((context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api/")) || context.Response.StatusCode == 404)
                {
                    // TODO: since this is an API, probably it would be better to response a more RESTFul content with 404 status code and a nicer message saying that the resource doesn't exist
                    // context.Response.StatusCode = 404;
                    // context.Response.ContentType = "application/json";
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            // Configures application for usage as API with default route of '/api/[Controller]'
            app.UseMvcWithDefaultRoute();

            // Configures applcation to serve the index.html file from /wwwroot when you access the server from a web browser
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // app.UseMvc();
        }
    }
}
