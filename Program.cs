using Core;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляем сервисы в контейнер

builder.Services.AddControllers();

// Конфигурация Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем PlayerService
builder.Services.AddScoped<PlayerService>();

// Регистрируем репозиторий с использованием интерфейса
builder.Services.AddScoped<IPlayerRepository>(provider => new ItemRepository(connectionString));

var app = builder.Build();

// Настройка HTTP конвейера
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
