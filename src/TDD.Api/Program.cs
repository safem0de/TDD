using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);

// ✅ เปิด SelfLog ช่วย debug
SelfLog.Enable(Console.Error);

// ✅ สร้าง Logger ด้วย overload ที่เวอร์ชัน 8.3.1 รองรับ
var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki(
        builder.Configuration["Loki:Url"]!,
        labels: new[]
        {
            new LokiLabel { Key = "app", Value = "tdd-api" },
            new LokiLabel { Key = "env", Value = "dev" } // ใช้ label แบบ static เพื่อป้องกัน null
        })
    .CreateLogger();

// ✅ Assign Logger ให้กับระบบ
Log.Logger = logger;
builder.Host.UseSerilog();

// 🔥 log test ไป Loki
Log.Information("🔥 Serilog with Loki v8.3.1 started at {Time}", DateTime.UtcNow);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
