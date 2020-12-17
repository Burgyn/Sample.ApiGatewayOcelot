using Kros.AspNetCore.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MMLib.Ocelot.Provider.AppConfiguration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Sample.ApiGateway.Aggregators;

namespace Sample.ApiGateway
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
            services.AddOcelot()
                .AddSingletonDefinedAggregator<BasketAggregator>()
                .AddAppConfiguration();
            services.AddSwaggerForOcelot(Configuration,
            (o) =>
            {
                o.GenerateDocsForAggregates = true;
                o.GenerateDocsForGatewayItSelf = true;
                o.AggregateDocsGeneratorPostProcess = (aggregateRoute, routesDocs, pathItemDoc, documentation) =>
                {
                    if (aggregateRoute.UpstreamPathTemplate == "/gateway/api/basketwithuser/{id}")
                    {
                        pathItemDoc.Operations[OperationType.Get].Parameters.Add(new OpenApiParameter()
                        {
                            Name = "customParameter",
                            Description = "Demo only. This parameter doesn't realy exist.",
                            Schema = new OpenApiSchema() { Type = "string"},
                            In = ParameterLocation.Header
                        });
                    }
                };
            },
            (o) =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo() { Description = "dsfsd fsd fsdf", Version = "v1", Contact = new OpenApiContact() { Name = "ffff" } });
            });
            services.AddServiceDiscovery();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerForOcelotUI();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot()
                .Wait();
        }
    }
}
