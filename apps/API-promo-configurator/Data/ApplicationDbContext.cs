using System;
using System.Collections.Generic;
using API_promo_configurator.Models;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Colonia> Colonias { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<ContratoPromocione> ContratoPromociones { get; set; }

    public virtual DbSet<ContratoServicio> ContratoServicios { get; set; }

    public virtual DbSet<Domicilio> Domicilios { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<MovimientosCuentum> MovimientosCuenta { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<PromocionAlcance> PromocionAlcances { get; set; }

    public virtual DbSet<Promocione> Promociones { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Suscriptore> Suscriptores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConexionSql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__Ciudades__B7DC4CD54F10A3BF");

            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciudades_Municipios");
        });

        modelBuilder.Entity<Colonia>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonias__E267D3BA33D918F9");

            entity.Property(e => e.IdColonia).HasColumnName("id_colonia");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Colonias_Ciudades");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__FF5F2A5620ED58FC");

            entity.HasIndex(e => e.IdSuscriptor, "IX_Contratos_IdSuscriptor");

            entity.Property(e => e.IdContrato).HasColumnName("id_contrato");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estado");
            entity.Property(e => e.FechaContratacion).HasColumnName("fecha_contratacion");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.IdSuscriptor).HasColumnName("id_suscriptor");
            entity.Property(e => e.PlazoForzosoMeses).HasColumnName("plazo_forzoso_meses");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratos_Sucursales");

            entity.HasOne(d => d.IdSuscriptorNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdSuscriptor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratos_Suscriptores");
        });

        modelBuilder.Entity<ContratoPromocione>(entity =>
        {
            entity.HasKey(e => e.IdContratoPromocion).HasName("PK__Contrato__9740535EB180635A");

            entity.ToTable("Contrato_Promociones");

            entity.Property(e => e.IdContratoPromocion).HasColumnName("id_contrato_promocion");
            entity.Property(e => e.FechaAplicacion).HasColumnName("fecha_aplicacion");
            entity.Property(e => e.IdContrato).HasColumnName("id_contrato");
            entity.Property(e => e.IdPromocion).HasColumnName("id_promocion");
            entity.Property(e => e.Metadata)
                .HasMaxLength(500)
                .HasColumnName("metadata");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.ContratoPromociones)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoPromociones_Contratos");

            entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.ContratoPromociones)
                .HasForeignKey(d => d.IdPromocion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoPromociones_Promociones");
        });

        modelBuilder.Entity<ContratoServicio>(entity =>
        {
            entity.HasKey(e => e.IdContratoServicio).HasName("PK__Contrato__53A2E8F9BDE44B3F");

            entity.ToTable("Contrato_Servicios");

            entity.HasIndex(e => e.IdContrato, "IX_ContratoServicios_IdContrato");

            entity.HasIndex(e => new { e.IdContrato, e.IdServicio }, "UQ__Contrato__29A22DAA1D47DD9C").IsUnique();

            entity.Property(e => e.IdContratoServicio).HasColumnName("id_contrato_servicio");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_alta");
            entity.Property(e => e.IdContrato).HasColumnName("id_contrato");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.PrecioContratado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_contratado");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.ContratoServicios)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoServicios_Contratos");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ContratoServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoServicios_Servicios");
        });

        modelBuilder.Entity<Domicilio>(entity =>
        {
            entity.HasKey(e => e.IdDomicilio).HasName("PK__Domicili__A0CCE5C2DAA2FB11");

            entity.Property(e => e.IdDomicilio).HasColumnName("id_domicilio");
            entity.Property(e => e.Calle)
                .HasMaxLength(255)
                .HasColumnName("calle");
            entity.Property(e => e.IdColonia).HasColumnName("id_colonia");
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(50)
                .HasColumnName("numero_exterior");
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(50)
                .HasColumnName("numero_interior");
            entity.Property(e => e.Referencias)
                .HasMaxLength(500)
                .HasColumnName("referencias");

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Domicilios)
                .HasForeignKey(d => d.IdColonia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Domicilios_Colonias");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estados__86989FB25C6726AF");

            entity.HasIndex(e => e.Nombre, "UQ__Estados__72AFBCC6FDA2CB49").IsUnique();

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MovimientosCuentum>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C2483EDE900");

            entity.ToTable("Movimientos_Cuenta");

            entity.HasIndex(e => e.IdContrato, "IX_MovimientosCuenta_IdContrato");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Concepto)
                .HasMaxLength(255)
                .HasColumnName("concepto");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.IdContrato).HasColumnName("id_contrato");
            entity.Property(e => e.MontoCargo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_cargo");
            entity.Property(e => e.MontoPago)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_pago");
            entity.Property(e => e.SaldoResultante)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("saldo_resultante");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.MovimientosCuenta)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientosCuenta_Contratos");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__01C9EB9915889AA0");

            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Municipios_Estados");
        });

        modelBuilder.Entity<PromocionAlcance>(entity =>
        {
            entity.HasKey(e => e.IdPromocionAlcance).HasName("PK__Promocio__777E93A2F228E681");

            entity.ToTable("Promocion_Alcance");

            entity.HasIndex(e => e.IdPromocion, "IX_PromocionAlcance_IdPromocion");

            entity.Property(e => e.IdPromocionAlcance).HasColumnName("id_promocion_alcance");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.IdColonia).HasColumnName("id_colonia");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdPromocion).HasColumnName("id_promocion");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.PromocionAlcances)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_PromocionAlcance_Ciudades");

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.PromocionAlcances)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("FK_PromocionAlcance_Colonias");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.PromocionAlcances)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_PromocionAlcance_Estados");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.PromocionAlcances)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("FK_PromocionAlcance_Municipios");

            entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.PromocionAlcances)
                .HasForeignKey(d => d.IdPromocion)
                .HasConstraintName("FK_PromocionAlcance_Promociones");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.PromocionAlcances)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_PromocionAlcance_Sucursales");
        });

        modelBuilder.Entity<Promocione>(entity =>
        {
            entity.HasKey(e => e.IdPromocion).HasName("PK__Promocio__F89308E0DF45E5D7");

            entity.Property(e => e.IdPromocion).HasColumnName("id_promocion");
            entity.Property(e => e.AplicaA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aplica_a");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.DuracionMeses)
                .HasDefaultValue(1)
                .HasColumnName("duracion_meses");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoDescuento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_descuento");
            entity.Property(e => e.ValorDescuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_descuento");

            entity.HasMany(d => d.IdServicios).WithMany(p => p.IdPromocions)
                .UsingEntity<Dictionary<string, object>>(
                    "PromocionServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicio")
                        .HasConstraintName("FK_PromocionServicio_Servicios"),
                    l => l.HasOne<Promocione>().WithMany()
                        .HasForeignKey("IdPromocion")
                        .HasConstraintName("FK_PromocionServicio_Promociones"),
                    j =>
                    {
                        j.HasKey("IdPromocion", "IdServicio").HasName("PK__Promocio__2E6E0F1D86737907");
                        j.ToTable("Promocion_Servicio");
                        j.IndexerProperty<int>("IdPromocion").HasColumnName("id_promocion");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("id_servicio");
                    });
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__6FD07FDC537543F5");

            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioBaseActual)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_base_actual");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__4C758013D1AA07CD");

            entity.HasIndex(e => e.Nombre, "UQ__Sucursal__72AFBCC66DDA1C7D").IsUnique();

            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.IdDomicilio).HasColumnName("id_domicilio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdDomicilioNavigation).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.IdDomicilio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursales_Domicilios");

            entity.HasMany(d => d.IdColonia).WithMany(p => p.IdSucursals)
                .UsingEntity<Dictionary<string, object>>(
                    "SucursalColonium",
                    r => r.HasOne<Colonia>().WithMany()
                        .HasForeignKey("IdColonia")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SucursalColonia_Colonias"),
                    l => l.HasOne<Sucursale>().WithMany()
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SucursalColonia_Sucursales"),
                    j =>
                    {
                        j.HasKey("IdSucursal", "IdColonia").HasName("PK__Sucursal__F253FD28CA01B7F7");
                        j.ToTable("Sucursal_Colonia");
                        j.IndexerProperty<int>("IdSucursal").HasColumnName("id_sucursal");
                        j.IndexerProperty<int>("IdColonia").HasColumnName("id_colonia");
                    });
        });

        modelBuilder.Entity<Suscriptore>(entity =>
        {
            entity.HasKey(e => e.IdSuscriptor).HasName("PK__Suscript__DD03358F7650BD7F");

            entity.HasIndex(e => e.Email, "UQ__Suscript__AB6E616416CE10D0").IsUnique();

            entity.HasIndex(e => e.Rfc, "UQ__Suscript__C2B034949CFD4F79").IsUnique();

            entity.Property(e => e.IdSuscriptor).HasColumnName("id_suscriptor");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdDomicilio).HasColumnName("id_domicilio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("rfc");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono_contacto");

            entity.HasOne(d => d.IdDomicilioNavigation).WithMany(p => p.Suscriptores)
                .HasForeignKey(d => d.IdDomicilio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suscriptores_Domicilios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
