using GymHelper.Infrastructure.Data;
using GymHelper.Application.Interfaces;
using GymHelper.Application.Services;
using GymHelper.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymHelper.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=gymhelper.db";

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<UserService>();

        builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
        builder.Services.AddScoped<ExerciseService>();

        builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        builder.Services.AddScoped<WorkoutService>();

        builder.Services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
        builder.Services.AddScoped<WorkoutExerciseService>();

        builder.Services.AddScoped<ISetRepository, SetRepository>();
        builder.Services.AddScoped<SetService>();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();
        app.UseCors("AllowFrontend");
        app.UseAuthorization();

        app.MapControllers();

        app.UseDeveloperExceptionPage();

        app.Run();
    }
}