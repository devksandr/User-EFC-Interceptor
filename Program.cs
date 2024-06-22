using Microsoft.EntityFrameworkCore;
using User_EFC_Interceptor.Database;
using User_EFC_Interceptor.Interceptors;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<UserPasswordInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>(
    (sp, options) => options
        .UseSqlite()
        .AddInterceptors(
            sp.GetRequiredService<UserPasswordInterceptor>()
        )
    );

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
