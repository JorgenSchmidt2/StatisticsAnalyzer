using EStatAnalyser.Web.Admin.API.API.Extensions.SwashbuckleExtension;
using EStatAnalyser.Web.Admin.API.DAL;
using EStatAnalyser.Web.Admin.API.Services;
using NLog.Web;

// args - загрузка данных конфигурации из appsettings.json
var builder = WebApplication.CreateBuilder(args);

#region Запуск builder'a

// Начальная стадия построения
builder.Logging.ClearProviders();
builder.WebHost.UseNLog();
builder.Services.AddMvc();
builder.Services.AddControllers();

// Разрешённые для приёма реквестов хосты и адреса
builder.Services.AddCors(opt => opt.AddPolicy("Policy",
    builder => builder.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("*")));

// Подключение Services, точка входа Entry
builder.Services.AddServices();

// Подключение DAL, точка входа Entry
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection")); 

// Подключение swashbackle, точка входа SwashBuckleService
builder.Services.AddSwachbackleService();
builder.Services.AddEndpointsApiExplorer();

#endregion

#region Построение приложения

var app = builder.Build();

// Конфигурация проекта
app.Logger.LogInformation("Log");
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseCors("Policy");
app.UseEndpoints(endpoints => endpoints.MapControllers());
if (app.Environment.IsDevelopment())
{
    // Подключение swashbackle, точка входа SwashBuckleApp
    app.SwaggerApp();
}

#endregion

// Запуск приложения
app.Run();