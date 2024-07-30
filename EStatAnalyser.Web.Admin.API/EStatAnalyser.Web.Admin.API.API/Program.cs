using EStatAnalyser.Web.Admin.API.API.Extensions.SwashbuckleExtension;
using EStatAnalyser.Web.Admin.API.DAL;
using EStatAnalyser.Web.Admin.API.Services;
using NLog.Web;

// args - �������� ������ ������������ �� appsettings.json
var builder = WebApplication.CreateBuilder(args);

#region ������ builder'a

// ��������� ������ ����������
builder.Logging.ClearProviders();
builder.WebHost.UseNLog();
builder.Services.AddMvc();
builder.Services.AddControllers();

// ����������� ��� ����� ��������� ����� � ������
builder.Services.AddCors(opt => opt.AddPolicy("Policy",
    builder => builder.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("*")));

// ����������� Services, ����� ����� Entry
builder.Services.AddServices();

// ����������� DAL, ����� ����� Entry
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection")); 

// ����������� swashbackle, ����� ����� SwashBuckleService
builder.Services.AddSwachbackleService();
builder.Services.AddEndpointsApiExplorer();

#endregion

#region ���������� ����������

var app = builder.Build();

// ������������ �������
app.Logger.LogInformation("Log");
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseCors("Policy");
app.UseEndpoints(endpoints => endpoints.MapControllers());
if (app.Environment.IsDevelopment())
{
    // ����������� swashbackle, ����� ����� SwashBuckleApp
    app.SwaggerApp();
}

#endregion

// ������ ����������
app.Run();