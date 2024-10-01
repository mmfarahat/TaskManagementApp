
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TaskManagementApp.API.Chat;
using TaskManagementApp.API.Middlewares;
using TaskManagementApp.API.Services;
using TaskManagementApp.Application;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Infrastructure;
using TaskManagementApp.Infrastructure.Identity;

namespace TaskManagementApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();

            //allow any origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173") // Update with your client URL
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                       .AllowCredentials());
            });


            builder.Services.AddSignalR(options =>
                        {
                            options.KeepAliveInterval = TimeSpan.FromSeconds(10); // Adjust as needed
                            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30); // Adjust as needed
                        });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.Urls.Add("https://localhost:7120");
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred seeding the DB.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigin"); // Apply the CORS policy

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();
            app.MapHub<ChatHub>("/chatHub");
            app.Run();
        }
    }
}
