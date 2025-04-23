using KanbanAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtemos a senha segura da variável de ambiente
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = $"Server=localhost;Database=KanbanDb;User Id=sa;Password={dbPassword};TrustServerCertificate=True;";

// Adiciona o contexto de banco com a connection string segura
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
