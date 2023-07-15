using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Noon.Api.Extensions;
using Noon.Api.Middlewares;
using Noon.Core.Entities.Identity;
using Noon.Repository.Data;
using Noon.Repository.Identity;
using StackExchange.Redis;


namespace Noon.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", configurePolicy =>
                {
                    configurePolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connectionString);
            });







            var app = builder.Build();

            //update database 
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = services.GetRequiredService<DataContext>();
                var identityContext = services.GetRequiredService<IdentityContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                await dbContext.Database.MigrateAsync();
                await identityContext.Database.MigrateAsync();

                await ProductDataSeeding.SeedDataAsync(dbContext);
                //await IdentityContextSeed.SeedAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);

            }

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}