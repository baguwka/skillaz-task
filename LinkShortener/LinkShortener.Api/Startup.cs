using LinkShortener.Api.Identity;
using LinkShortener.Api.IdGenerator;
using LinkShortener.Api.Middlewares;
using LinkShortener.Api.Repo;
using LinkShortener.Api.Repo.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Lib;
using Shortener.Lib.Ids;
using Shortener.Lib.Shorten;
using Swashbuckle.AspNetCore.Swagger;

namespace LinkShortener.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IHttpContextIdentifier, CookiesIdentifier>();
            services.AddSingleton<IMongoDbProvider, LocalhostShortenerMongoDbProvider>();
            services.AddScoped<ILinksRepository, MongoLinksRepository>();
            services.AddScoped<ILinksShortener, Base62ByIdLinksShortener>();

            services.AddScoped<IUrlValidator, BclUrlValidator>();
            services.AddScoped<ICounterRepository, MongoCounterRepository>();
            services.AddScoped<ILinksIdGenerator, MongoIncrementorIdGenerator>();
            //services.AddTransient<MongoIncrementorIdGenerator>();
            //services.AddSingleton<ILinksIdGenerator>(provider => new CachingLinksGenerator(
            //    provider.GetService<IMemoryCache>(),
            //    provider.GetService<MongoIncrementorIdGenerator>()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Link shortener API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<JsonAllErrorHandlingMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Link shortener API v1"); });
        }
    }
}
