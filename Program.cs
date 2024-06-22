using Microsoft.EntityFrameworkCore;
using Serilog;
using User_EFC_Interceptor.Database;
using User_EFC_Interceptor.Interceptors;
using User_EFC_Interceptor.Services.Base64;
using User_EFC_Interceptor.Services.User;

bool appState = true;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

string? connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
if (connectionString is null )
{
    logger.LogError("Connection string not found");
    appState = false;
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBase64Service, Base64Service>();
builder.Services.AddSingleton<UserInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>(
    (sp, options) => options
        .UseSqlite(connectionString)
        .AddInterceptors(
            sp.GetRequiredService<UserInterceptor>()
        )
);
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
if (appState)
{
    app.Run();
}
Console.ReadLine();