using API_promo_configurator.Data;
using API_promo_configurator.Repository;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexion a la BD (DBContext)
var activeConn = builder.Configuration["ActiveConnection"]!;
var dbConnectionString = builder.Configuration.GetConnectionString(activeConn)!;

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));

// Inyección de dependencias
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IPromocionRepository, PromocionRepository>();
builder.Services.AddScoped<IContratoRepository, ContratoRepository>();
builder.Services.AddScoped<IContratoServicioRepository, ContratoServicioRepository>();
builder.Services.AddScoped<IContratoPromocionRepository, ContratoPromocionRepository>();
builder.Services.AddScoped<IMovimientosCuentaRepository, MovimientosCuentaRepository>();
builder.Services.AddScoped<ISuscriptorRepository, SuscriptorRepository>();
builder.Services.AddScoped<IDomicilioRepository, DomicilioRepository>();
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddScoped<IColoniaRepository, ColoniaRepository>();
builder.Services.AddScoped<ICiudadRepository, CiudadRepository>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IPromocionAlcanceRepository, PromocionAlcanceRepository>();

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
