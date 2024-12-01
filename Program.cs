using Core;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ������������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� ������� � ���������

builder.Services.AddControllers();

// ������������ Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ������������ PlayerService
builder.Services.AddScoped<PlayerService>();

// ������������ ����������� � �������������� ����������
builder.Services.AddScoped<IPlayerRepository>(provider => new ItemRepository(connectionString));

var app = builder.Build();

// ��������� HTTP ���������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
