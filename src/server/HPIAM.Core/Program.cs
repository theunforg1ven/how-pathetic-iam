using HPIAM.Core.Extensions;

namespace HPIAM.Core;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors(x => x.AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithOrigins("http://localhost:4200", "https://localhost:4200"));

        app.UseAuthentication(); // 1 - order is important
        app.UseAuthorization(); // 2

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();
        app.Run();
    }
}
