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

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E8SC11C;Database=ConfiguradorPromociones;Trusted_Connection=True;TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__Ciudades__B7DC4CD56A9E221C");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Ciudades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciudades_Municipios");
        });

        modelBuilder.Entity<Colonia>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonias__E267D3BA2A37A603");
            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Colonia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Colonias_Ciudades");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__FF5F2A56A63040D6");

            entity.Property(e => e.Estado).HasDefaultValue("Activo");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Contratos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratos_Sucursales");

            entity.HasOne(d => d.IdSuscriptorNavigation).WithMany(p => p.Contratos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratos_Suscriptores");
        });

        modelBuilder.Entity<ContratoPromocione>(entity =>
        {
            entity.HasKey(e => e.IdContratoPromocion).HasName("PK__Contrato__9740535E43843939");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.ContratoPromociones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoPromociones_Contratos");

            entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.ContratoPromociones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoPromociones_Promociones");
        });

        modelBuilder.Entity<ContratoServicio>(entity =>
        {
            entity.HasKey(e => e.IdContratoServicio).HasName("PK__Contrato__53A2E8F95C09092A");

            entity.Property(e => e.FechaAlta).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.ContratoServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoServicios_Contratos");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ContratoServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContratoServicios_Servicios");
        });

        modelBuilder.Entity<Domicilio>(entity =>
        {
            entity.HasKey(e => e.IdDomicilio).HasName("PK__Domicili__A0CCE5C2CE33EDC7");

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Domicilios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Domicilios_Colonias");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estados__86989FB23758ED80");
        });

        modelBuilder.Entity<MovimientosCuentum>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C242C639EE6");

            entity.Property(e => e.FechaMovimiento).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.MovimientosCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientosCuenta_Contratos");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__01C9EB99AE16FD61");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Municipios_Estados");
        });

        modelBuilder.Entity<PromocionAlcance>(entity =>
        {
            entity.HasKey(e => e.IdPromocionAlcance).HasName("PK__Promocio__777E93A28B1AF2AE");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.PromocionAlcances).HasConstraintName("FK_PromocionAlcance_Ciudades");

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.PromocionAlcances).HasConstraintName("FK_PromocionAlcance_Colonias");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.PromocionAlcances).HasConstraintName("FK_PromocionAlcance_Estados");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.PromocionAlcances).HasConstraintName("FK_PromocionAlcance_Municipios");

            entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.PromocionAlcances).HasConstraintName("FK_PromocionAlcance_Promociones");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.PromocionAlcances).HasConstraintName("FK_PromocionAlcance_Sucursales");
        });

        modelBuilder.Entity<Promocione>(entity =>
        {
            entity.HasKey(e => e.IdPromocion).HasName("PK__Promocio__F89308E0603D7BF5");

            entity.Property(e => e.DuracionMeses).HasDefaultValue(1);

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
                        j.ToTable("Promocion_Servicio");
                        j.IndexerProperty<int>("IdPromocion").HasColumnName("id_promocion");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("id_servicio");
                    });
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__6FD07FDC38BBE607");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__4C758013EF48213B");

            entity.HasOne(d => d.IdDomicilioNavigation).WithMany(p => p.Sucursales)
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
                        j.HasKey("IdSucursal", "IdColonia").HasName("PK__Sucursal__F253FD289F584129");

                        j.ToTable("Sucursal_Colonia");
                        j.IndexerProperty<int>("IdSucursal").HasColumnName("id_sucursal");
                        j.IndexerProperty<int>("IdColonia").HasColumnName("id_colonia");
                    });
        });

        modelBuilder.Entity<Suscriptore>(entity =>
        {
            entity.HasKey(e => e.IdSuscriptor).HasName("PK__Suscript__DD03358F481557A8");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdDomicilioNavigation).WithMany(p => p.Suscriptores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suscriptores_Domicilios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
