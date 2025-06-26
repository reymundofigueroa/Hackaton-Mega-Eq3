using API_promo_configurator.Data;
using API_promo_configurator.Repository;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexion a la BD (DBContext)
var activeConn = builder.Configuration["ActiveConnection"];
var dbConnectionString = builder.Configuration.GetConnectionString(activeConn);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));

// Inyección de dependencias
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();

// Solo se agrega una vez
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
