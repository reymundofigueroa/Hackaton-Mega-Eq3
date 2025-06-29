/****************************************************************************************
* SCRIPT DE CONFIGURACIÓN DE BASE DE DATOS "ConfiguradorPromociones"                    *
* VERSIÓN 3.0                                                                           *
*																						*
* Propósito: Crea o reinicia completamente la base de datos y el esquema.               *
* Cambios v3.0:                                                                         *
* * - Creada tabla intermedia 'Promocion_Servicio' para vincular promociones a uno o    *
*	  más servicios específicos.														*
*																						*
* ¡ADVERTENCIA! Si la base de datos ya existe, este script la eliminará por				*
* completo (incluyendo todos sus datos) y la volverá a crear desde cero.				*
****************************************************************************************/

-- === 1. GESTIÓN DE LA BASE DE DATOS ===
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ConfiguradorPromociones')
BEGIN
    CREATE DATABASE ConfiguradorPromociones;
    PRINT 'Base de datos "ConfiguradorPromociones" creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos "ConfiguradorPromociones" ya existe. Se procederá a reiniciarla.';
    ALTER DATABASE ConfiguradorPromociones SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ConfiguradorPromociones;
    PRINT 'Base de datos "ConfiguradorPromociones" eliminada.';
	CREATE DATABASE ConfiguradorPromociones;
    PRINT 'Base de datos "ConfiguradorPromociones" creada exitosamente.';
END
GO

USE ConfiguradorPromociones;
GO

-- === 2. GESTIÓN DEL ESQUEMA (TABLAS E ÍNDICES) ===
BEGIN TRY

    -- --- SECCIÓN DE ELIMINACIÓN DE OBJETOS ---
    PRINT '--------------------------------------------------';
    PRINT 'Iniciando proceso de limpieza de tablas...';

    IF OBJECT_ID('dbo.Movimientos_Cuenta', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Movimientos_Cuenta; PRINT '-> Tabla "Movimientos_Cuenta" eliminada.'; END
    IF OBJECT_ID('dbo.Contrato_Promociones', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Contrato_Promociones; PRINT '-> Tabla "Contrato_Promociones" eliminada.'; END
    IF OBJECT_ID('dbo.Contrato_Servicios', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Contrato_Servicios; PRINT '-> Tabla "Contrato_Servicios" eliminada.'; END
    IF OBJECT_ID('dbo.Sucursal_Colonia', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Sucursal_Colonia; PRINT '-> Tabla "Sucursal_Colonia" eliminada.'; END
    IF OBJECT_ID('dbo.Promocion_Alcance', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Promocion_Alcance; PRINT '-> Tabla "Promocion_Alcance" eliminada.'; END
	IF OBJECT_ID('dbo.Promocion_Servicio', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Promocion_Servicio; PRINT '-> Tabla "Promocion_Servicio" eliminada.'; END -- Nueva tabla
    IF OBJECT_ID('dbo.Contratos', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Contratos; PRINT '-> Tabla "Contratos" eliminada.'; END
    IF OBJECT_ID('dbo.Suscriptores', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Suscriptores; PRINT '-> Tabla "Suscriptores" eliminada.'; END
    IF OBJECT_ID('dbo.Sucursales', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Sucursales; PRINT '-> Tabla "Sucursales" eliminada.'; END
    IF OBJECT_ID('dbo.Domicilios', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Domicilios; PRINT '-> Tabla "Domicilios" eliminada.'; END
    IF OBJECT_ID('dbo.Colonias', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Colonias; PRINT '-> Tabla "Colonias" eliminada.'; END
    IF OBJECT_ID('dbo.Ciudades', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Ciudades; PRINT '-> Tabla "Ciudades" eliminada.'; END
    IF OBJECT_ID('dbo.Municipios', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Municipios; PRINT '-> Tabla "Municipios" eliminada.'; END
    IF OBJECT_ID('dbo.Estados', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Estados; PRINT '-> Tabla "Estados" eliminada.'; END
    IF OBJECT_ID('dbo.Servicios', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Servicios; PRINT '-> Tabla "Servicios" eliminada.'; END
    IF OBJECT_ID('dbo.Promociones', 'U') IS NOT NULL BEGIN DROP TABLE dbo.Promociones; PRINT '-> Tabla "Promociones" eliminada.'; END
    
    PRINT 'Proceso de limpieza finalizado.';
    PRINT '--------------------------------------------------';


    -- --- SECCIÓN DE CREACIÓN DE OBJETOS ---
    PRINT 'Iniciando proceso de creación de tablas...';
    
    -- Tablas de Catálogo (Geografía y Negocio)
    CREATE TABLE Estados (
		id_estado INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(100) NOT NULL UNIQUE
	);
    CREATE TABLE Municipios (
		id_municipio INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(150) NOT NULL, 
		id_estado INT NOT NULL, 
		CONSTRAINT FK_Municipios_Estados FOREIGN KEY (id_estado) REFERENCES Estados(id_estado)
	);
    CREATE TABLE Ciudades (
		id_ciudad INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(150) NOT NULL, 
		id_municipio INT NOT NULL, 
		CONSTRAINT FK_Ciudades_Municipios FOREIGN KEY (id_municipio) REFERENCES Municipios(id_municipio)
	);
    CREATE TABLE Colonias (
		id_colonia INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(150) NOT NULL, 
		codigo_postal VARCHAR(10) NOT NULL, 
		id_ciudad INT NOT NULL, 
		CONSTRAINT FK_Colonias_Ciudades FOREIGN KEY (id_ciudad) REFERENCES Ciudades(id_ciudad)
	);
    PRINT '-> Tablas de Geografía creadas.';

    CREATE TABLE Servicios (
		id_servicio INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(150) NOT NULL, 
		descripcion NVARCHAR(500) NULL, 
		precio_base_actual DECIMAL(10, 2) NOT NULL, -- El precio de lista actual
		activo BIT NOT NULL DEFAULT 1 -- Para poder desactivar servicios sin borrarlos
	);
    
    -- Tabla de Promociones
    CREATE TABLE Promociones (
        id_promocion INT PRIMARY KEY IDENTITY(1,1),
        nombre NVARCHAR(150) NOT NULL,
        descripcion NVARCHAR(500) NULL,
        fecha_inicio DATE NOT NULL,
        fecha_fin DATE NOT NULL,
        tipo_descuento VARCHAR(50) NOT NULL, -- Ej: 'PORCENTAJE', 'MONTO_FIJO', 'MESES_GRATIS'
        valor_descuento DECIMAL(10, 2) NOT NULL, -- Ej: 0.10 para 10%, 100.00 para $100 de descuento
        aplica_a VARCHAR(50) NOT NULL, -- Ej: 'INSTALACION', 'MENSUALIDAD'
        -- NUEVO: Duración en meses para el cliente desde que la activa.
        duracion_meses INT NOT NULL DEFAULT 1,
        CONSTRAINT CHK_Promociones_TipoDescuento CHECK (tipo_descuento IN ('PORCENTAJE', 'MONTO_FIJO', 'MESES_GRATIS')),
        -- NUEVO: Validación para el campo aplica_a.
        CONSTRAINT CHK_Promociones_AplicaA CHECK (aplica_a IN ('INSTALACION', 'MENSUALIDAD')),
        CONSTRAINT CHK_Promociones_Fechas CHECK (fecha_fin >= fecha_inicio)
    );
    PRINT '-> Tablas de Catálogo de Negocio creadas (Promociones modificada).';

    -- Tablas de Entidades Principales
    CREATE TABLE Domicilios (
		id_domicilio INT PRIMARY KEY IDENTITY(1,1), 
		calle NVARCHAR(255) NOT NULL, 
		numero_exterior NVARCHAR(50) NOT NULL, 
		numero_interior NVARCHAR(50) NULL, -- Puede ser nulo (casas)
		referencias NVARCHAR(500) NULL, 
		id_colonia INT NOT NULL, 
		CONSTRAINT FK_Domicilios_Colonias FOREIGN KEY (id_colonia) REFERENCES Colonias(id_colonia)
	);
    CREATE TABLE Sucursales (
		id_sucursal INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(150) NOT NULL UNIQUE, 
		id_domicilio INT NOT NULL, -- Cada sucursal tiene una dirección física
		telefono VARCHAR(20) NULL, 
		CONSTRAINT FK_Sucursales_Domicilios FOREIGN KEY (id_domicilio) REFERENCES Domicilios(id_domicilio)
	);
    CREATE TABLE Suscriptores (
		id_suscriptor INT PRIMARY KEY IDENTITY(1,1), 
		nombre NVARCHAR(100) NOT NULL, 
		apellido_paterno NVARCHAR(100) NOT NULL, 
		apellido_materno NVARCHAR(100) NULL, 
		rfc VARCHAR(13) NULL UNIQUE, 
		email NVARCHAR(255) NOT NULL UNIQUE, 
		telefono_contacto VARCHAR(20) NOT NULL, 
		fecha_registro DATE NOT NULL DEFAULT GETDATE(), 
		id_domicilio INT NOT NULL, 
		CONSTRAINT FK_Suscriptores_Domicilios FOREIGN KEY (id_domicilio) REFERENCES Domicilios(id_domicilio)
	);
    PRINT '-> Tablas de Entidades Principales creadas.';

	-- NUEVA TABLA para vincular promociones a servicios
    CREATE TABLE Promocion_Servicio (
        id_promocion INT NOT NULL,
        id_servicio INT NOT NULL,
        PRIMARY KEY (id_promocion, id_servicio), -- Llave primaria compuesta
        CONSTRAINT FK_PromocionServicio_Promociones FOREIGN KEY (id_promocion) REFERENCES Promociones(id_promocion) ON DELETE CASCADE,
        CONSTRAINT FK_PromocionServicio_Servicios FOREIGN KEY (id_servicio) REFERENCES Servicios(id_servicio) ON DELETE CASCADE
    );
    PRINT '-> Tabla "Promocion_Servicio" creada.';

    -- Tablas de Relación y Transacciones
    CREATE TABLE Contratos (
		id_contrato INT PRIMARY KEY IDENTITY(1,1), 
		id_suscriptor INT NOT NULL, 
		id_sucursal INT NOT NULL, 
		fecha_contratacion DATE NOT NULL, 
		plazo_forzoso_meses INT NOT NULL DEFAULT 0, 
		estado VARCHAR(50) NOT NULL DEFAULT 'Activo', -- 'Activo', 'Cancelado', 'Suspendido'
		CONSTRAINT FK_Contratos_Suscriptores FOREIGN KEY (id_suscriptor) REFERENCES Suscriptores(id_suscriptor), 
		CONSTRAINT FK_Contratos_Sucursales FOREIGN KEY (id_sucursal) REFERENCES Sucursales(id_sucursal), 
		CONSTRAINT CHK_Contratos_Estado CHECK (estado IN ('Activo', 'Cancelado', 'Suspendido', 'Finalizado'))
	);
    CREATE TABLE Sucursal_Colonia (
		id_sucursal INT NOT NULL, 
		id_colonia INT NOT NULL, 
		PRIMARY KEY (id_sucursal, id_colonia), -- Llave primaria compuesta
		CONSTRAINT FK_SucursalColonia_Sucursales FOREIGN KEY (id_sucursal) REFERENCES Sucursales(id_sucursal), 
		CONSTRAINT FK_SucursalColonia_Colonias FOREIGN KEY (id_colonia) REFERENCES Colonias(id_colonia)
	);
    
    -- TABLA para el alcance geográfico de las promociones
    CREATE TABLE Promocion_Alcance (
        id_promocion_alcance INT PRIMARY KEY IDENTITY(1,1),
        id_promocion INT NOT NULL,
        id_estado INT NULL,
        id_municipio INT NULL,
        id_ciudad INT NULL,
        id_colonia INT NULL,
        id_sucursal INT NULL,
        CONSTRAINT FK_PromocionAlcance_Promociones FOREIGN KEY (id_promocion) REFERENCES Promociones(id_promocion) ON DELETE CASCADE,
        CONSTRAINT FK_PromocionAlcance_Estados FOREIGN KEY (id_estado) REFERENCES Estados(id_estado),
        CONSTRAINT FK_PromocionAlcance_Municipios FOREIGN KEY (id_municipio) REFERENCES Municipios(id_municipio),
        CONSTRAINT FK_PromocionAlcance_Ciudades FOREIGN KEY (id_ciudad) REFERENCES Ciudades(id_ciudad),
        CONSTRAINT FK_PromocionAlcance_Colonias FOREIGN KEY (id_colonia) REFERENCES Colonias(id_colonia),
        CONSTRAINT FK_PromocionAlcance_Sucursales FOREIGN KEY (id_sucursal) REFERENCES Sucursales(id_sucursal),
        -- Asegura que cada fila defina al menos un nivel de alcance geográfico.
        CONSTRAINT CHK_PromocionAlcance_MinimoUno CHECK (id_estado IS NOT NULL OR id_municipio IS NOT NULL OR id_ciudad IS NOT NULL OR id_colonia IS NOT NULL OR id_sucursal IS NOT NULL)
    );
    PRINT '-> Tabla "Promocion_Alcance" creada.';
    
    CREATE TABLE Contrato_Servicios (
		id_contrato_servicio INT PRIMARY KEY IDENTITY(1,1), 
		id_contrato INT NOT NULL, 
		id_servicio INT NOT NULL, 
		precio_contratado DECIMAL(10, 2) NOT NULL, 
		fecha_alta DATE NOT NULL DEFAULT GETDATE(), 
		CONSTRAINT FK_ContratoServicios_Contratos FOREIGN KEY (id_contrato) REFERENCES Contratos(id_contrato), 
		CONSTRAINT FK_ContratoServicios_Servicios FOREIGN KEY (id_servicio) REFERENCES Servicios(id_servicio), 
		UNIQUE (id_contrato, id_servicio)
	);
    CREATE TABLE Contrato_Promociones (
		id_contrato_promocion INT PRIMARY KEY IDENTITY(1,1), 
		id_contrato INT NOT NULL, id_promocion INT NOT NULL, 
		fecha_aplicacion DATE NOT NULL, 
		metadata NVARCHAR(500) NULL, -- Campo extra por si se necesita guardar info de la aplicación
		CONSTRAINT FK_ContratoPromociones_Contratos FOREIGN KEY (id_contrato) REFERENCES Contratos(id_contrato), 
		CONSTRAINT FK_ContratoPromociones_Promociones FOREIGN KEY (id_promocion) REFERENCES Promociones(id_promocion)
	);
    CREATE TABLE Movimientos_Cuenta (
		id_movimiento BIGINT PRIMARY KEY IDENTITY(1,1), 
		id_contrato INT NOT NULL, 
		fecha_movimiento DATETIME NOT NULL DEFAULT GETDATE(), 
		concepto NVARCHAR(255) NOT NULL, -- Ej: 'Mensualidad Julio 2025', 'Pago con Tarjeta'
		monto_cargo DECIMAL(10, 2) NOT NULL DEFAULT 0, 
		monto_pago DECIMAL(10, 2) NOT NULL DEFAULT 0, 
		saldo_resultante DECIMAL(12, 2) NOT NULL, -- El saldo del contrato después de este movimiento
		CONSTRAINT FK_MovimientosCuenta_Contratos FOREIGN KEY (id_contrato) REFERENCES Contratos(id_contrato), 
		-- Un movimiento debe ser un cargo o un pago, no puede ser 0 en ambos
		CONSTRAINT CHK_Movimientos_MontoValido CHECK (monto_cargo >= 0 AND monto_pago >= 0 AND (monto_cargo + monto_pago) > 0)
	);
    PRINT '-> Resto de Tablas de Relación y Transacciones creadas.';

    PRINT 'Proceso de creación de tablas finalizado.';
    PRINT '--------------------------------------------------';

    -- --- SECCIÓN DE CREACIÓN DE ÍNDICES ---
    PRINT 'Iniciando creación de índices para optimización de consultas...';
    CREATE INDEX IX_Contratos_IdSuscriptor ON Contratos(id_suscriptor);
    CREATE INDEX IX_MovimientosCuenta_IdContrato ON Movimientos_Cuenta(id_contrato);
    CREATE INDEX IX_ContratoServicios_IdContrato ON Contrato_Servicios(id_contrato);
    CREATE INDEX IX_PromocionAlcance_IdPromocion ON Promocion_Alcance(id_promocion);
    PRINT '-> Índices creados exitosamente.';
    PRINT '--------------------------------------------------';

    PRINT '*** Configuración de la base de datos completada exitosamente (v2.0). ***';

END TRY
BEGIN CATCH
    PRINT '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!';
    PRINT 'Ocurrió un error en la configuración de la base de datos.';
    PRINT 'Error: ' + ERROR_MESSAGE();
    PRINT '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!';
END CATCH;
GO