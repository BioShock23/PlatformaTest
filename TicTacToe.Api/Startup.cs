using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Hubs;
using TicTacToe.Api.Services;


namespace TicTacToe.Api;

public static class Startup
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder
           .AddControllers()
           .AddSignalR()
           .AddSwagger()
           .AddTicTacToeServices()
           .AddApplicationDbContext()
           .AddIdentity();

        return builder;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseRouting();
        app.UseDevelopmentConfiguration();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static WebApplication UseDevelopmentConfiguration(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;

        const string swaggerPrefix = "api";

        app.UseSwagger(options =>
        {
            options.RouteTemplate = $"{swaggerPrefix}/{{documentName}}/swagger.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = $"{swaggerPrefix}";
            c.SwaggerEndpoint($"/{swaggerPrefix}/v1/swagger.json", $"{nameof(Api)} v1");
        });

        return app;
    }

    private static WebApplicationBuilder AddApplicationDbContext(this WebApplicationBuilder builder)
    {
        builder
           .Services
           .AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder
                   .Configuration
                   .GetConnectionString("ApplicationDbContext")!;

                options.UseNpgsql(connectionString);
            });

        return builder;
    }

    private static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder
           .Services
           .AddSwaggerGen();

        return builder;
    }

    private static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder
           .Services
           .AddControllers();

        return builder;
    }

    private static Task<WebApplicationBuilder> AddIdentity(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services
           .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                return Task.CompletedTask;
            };
        });

        return Task.FromResult(builder);
    }

    private static WebApplicationBuilder AddTicTacToeServices(this WebApplicationBuilder builder)
    {
        builder
           .Services
           .AddScoped<IPlayerService, PlayerService>()
           .AddScoped<IGameService, GameService>()
           .AddScoped<ICellService, CellService>()
           .AddScoped<IBoardService, BoardService>();

        return builder;
    }

    private static WebApplicationBuilder AddSignalR(this WebApplicationBuilder builder)
    {
        builder
           .Services
           .AddSignalR()
           .AddHubOptions<GameHub>(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(15);
            });

        return builder;
    }
}
