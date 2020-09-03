using CicekSepeti.Repository;
using CicekSepeti.Repository.MongoRepository;
using CicekSepeti.Repository.MongoRepository.BasketRepository;
using CicekSepeti.Repository.MSSQLRepository;
using CicekSepeti.Service;
using Common.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CicekSepeti.Api
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


            services.AddDbContext<SQLContext>(x=> x.UseInMemoryDatabase(Configuration.GetSection("SQLConnection:ConnectionString").Value),ServiceLifetime.Singleton);

            services.AddSingleton<RedisContext>(x => new RedisContext(Configuration.GetSection("RedisConnection").Get<RedisConnection>()));
            services.AddSingleton<MongoContext>(x => new MongoContext(Configuration.GetSection("MongoConnection").Get<MongoConnection>()));


            services.AddMvc(options =>
            {
                options.Filters.Add(new RemoteFilterAttribute());
            });
              
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new  Microsoft.OpenApi.Models.OpenApiInfo
                {
                    
                    Title = "Çiçek Sepeti API",
                    Version = "v1",
                    Description = "Çiçek Sepeti API tutorial using MongoDB & Redis & MSSQL",
                });
            });



            services.AddScoped(typeof(ISQLRepository<>), typeof(SQLRepository<>));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IRedisRepository, RedisRepository>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RedisContext redisContext)
        {

            //redisContext.Connect(Configuration.GetSection("RedisConnection:ConnectionString").Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Supplement V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
