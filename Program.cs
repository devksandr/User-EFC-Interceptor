using Microsoft.EntityFrameworkCore;
using User_EFC_Interceptor.Database;
using User_EFC_Interceptor.Interceptors;
using User_EFC_Interceptor.Services;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
if (connectionString is null )
{
    // TODO log
    return;
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<UserPasswordInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>(
    (sp, options) => options
        .UseSqlite(connectionString)
        .AddInterceptors(
            sp.GetRequiredService<UserPasswordInterceptor>()
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
app.Run();
