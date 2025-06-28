-- Usar la base de datos "ConfiguradorPromociones"
USE ConfiguradorPromociones;
GO

-- ====================================================
-- Bloque 1: Limpieza de datos y reseed de identidades
-- ====================================================
BEGIN TRY
    PRINT '--- INICIO: LIMPIEZA DE DATOS EXISTENTES ---';

    -- Eliminar en orden de dependencias
    DELETE FROM dbo.Movimientos_Cuenta;
    DELETE FROM dbo.Contrato_Promociones;
    DELETE FROM dbo.Contrato_Servicios;
    DELETE FROM dbo.Promocion_Alcance;
    DELETE FROM dbo.Sucursal_Colonia;
    DELETE FROM dbo.Contratos;
    DELETE FROM dbo.Suscriptores;
    DELETE FROM dbo.Sucursales;
    DELETE FROM dbo.Domicilios;
    DELETE FROM dbo.Promocion_Servicio;
    DELETE FROM dbo.Promociones;
    DELETE FROM dbo.Servicios;
    DELETE FROM dbo.Colonias;
    DELETE FROM dbo.Ciudades;
    DELETE FROM dbo.Municipios;
    DELETE FROM dbo.Estados;

    PRINT 'Registros eliminados de todas las tablas.';

    -- Reseed de identidades solo en tablas con IDENTITY
    DBCC CHECKIDENT ('dbo.Estados',             RESEED, 0);
    DBCC CHECKIDENT ('dbo.Municipios',          RESEED, 0);
    DBCC CHECKIDENT ('dbo.Ciudades',            RESEED, 0);
    DBCC CHECKIDENT ('dbo.Colonias',            RESEED, 0);
    DBCC CHECKIDENT ('dbo.Servicios',           RESEED, 0);
    DBCC CHECKIDENT ('dbo.Promociones',         RESEED, 0);
    DBCC CHECKIDENT ('dbo.Domicilios',          RESEED, 0);
    DBCC CHECKIDENT ('dbo.Sucursales',          RESEED, 0);
    DBCC CHECKIDENT ('dbo.Suscriptores',        RESEED, 0);
    DBCC CHECKIDENT ('dbo.Contratos',           RESEED, 0);
    DBCC CHECKIDENT ('dbo.Contrato_Servicios',  RESEED, 0);
    DBCC CHECKIDENT ('dbo.Contrato_Promociones',RESEED, 0);
    DBCC CHECKIDENT ('dbo.Promocion_Alcance',   RESEED, 0);
    DBCC CHECKIDENT ('dbo.Movimientos_Cuenta',  RESEED, 0);

    PRINT 'Contadores de identidad reiniciados.';
    PRINT '--- FIN: LIMPIEZA DE DATOS EXISTENTES ---';
END TRY
BEGIN CATCH
    PRINT 'Ocurrió un error durante la limpieza de datos.';
    PRINT 'Error Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Error Mensaje: ' + ERROR_MESSAGE();
END CATCH;
GO

-- ====================================================
-- Bloque 2: Inserción de datos de prueba
-- ====================================================
BEGIN TRY
    PRINT '--- INICIO: INSERCIÓN DE DATOS ---';

    -- Variables para IDs
    DECLARE
      @id_estado1 INT, @id_estado2 INT, @id_estado3 INT,
      @id_mun1 INT,    @id_mun2 INT,    @id_mun3 INT,
      @id_ciud1 INT,   @id_ciud2 INT,   @id_ciud3 INT,
      @id_col1 INT,    @id_col2 INT,    @id_col3 INT,
      @id_serv_internet INT, @id_serv_telefonia INT, @id_serv_tv INT,
      @id_prom_pct INT, @id_prom_fijo INT, @id_prom_meses INT, @id_prom_geo INT,
      @id_domic1 INT, @id_domic2 INT, @id_domic3 INT, @id_domic4 INT,
      @id_suc1 INT, @id_suc2 INT,
      @id_sub1 INT, @id_sub2 INT, @id_sub3 INT, @id_sub4 INT,
      @id_contrato1 INT, @id_contrato2 INT, @id_contrato3 INT, @id_contrato4 INT;

    -- 1. Geografía
    INSERT INTO Estados (nombre) VALUES 
      ('Jalisco');          SET @id_estado1 = SCOPE_IDENTITY();
    INSERT INTO Estados (nombre) VALUES 
      ('Nuevo León');       SET @id_estado2 = SCOPE_IDENTITY();
    INSERT INTO Estados (nombre) VALUES 
      ('Ciudad de México'); SET @id_estado3 = SCOPE_IDENTITY();

    INSERT INTO Municipios (nombre, id_estado) VALUES 
      ('Guadalajara', @id_estado1); SET @id_mun1 = SCOPE_IDENTITY();
    INSERT INTO Municipios (nombre, id_estado) VALUES 
      ('Monterrey',   @id_estado2); SET @id_mun2 = SCOPE_IDENTITY();
    INSERT INTO Municipios (nombre, id_estado) VALUES 
      ('Coyoacán',    @id_estado3); SET @id_mun3 = SCOPE_IDENTITY();

    INSERT INTO Ciudades (nombre, id_municipio) VALUES
      ('Zapopan',               @id_mun1); SET @id_ciud1 = SCOPE_IDENTITY();
    INSERT INTO Ciudades (nombre, id_municipio) VALUES
      ('San Pedro Garza García',@id_mun2); SET @id_ciud2 = SCOPE_IDENTITY();
    INSERT INTO Ciudades (nombre, id_municipio) VALUES
      ('Ciudad de México',      @id_mun3); SET @id_ciud3 = SCOPE_IDENTITY();

    INSERT INTO Colonias (nombre, codigo_postal, id_ciudad) VALUES
      ('Chapalita',    '45010', @id_ciud1); SET @id_col1 = SCOPE_IDENTITY();
    INSERT INTO Colonias (nombre, codigo_postal, id_ciudad) VALUES
      ('Valle Oriente','66269', @id_ciud2); SET @id_col2 = SCOPE_IDENTITY();
    INSERT INTO Colonias (nombre, codigo_postal, id_ciudad) VALUES
      ('Del Carmen',   '04100', @id_ciud3); SET @id_col3 = SCOPE_IDENTITY();

    PRINT '3 estados, municipios, ciudades y colonias insertados.';

    -- 2. Servicios
    INSERT INTO Servicios (nombre, descripcion, precio_base_actual) VALUES
      ('Internet 100 Mbps', 'Internet residencial 100 Mbps', 650.00);   SET @id_serv_internet = SCOPE_IDENTITY();
    INSERT INTO Servicios (nombre, descripcion, precio_base_actual) VALUES
      ('Telefonía Ilimitada', 'Llamadas nacionales ilimitadas', 200.00);SET @id_serv_telefonia = SCOPE_IDENTITY();
    INSERT INTO Servicios (nombre, descripcion, precio_base_actual) VALUES
      ('TV HD Interactiva', 'Más de 80 canales en HD', 300.00);         SET @id_serv_tv = SCOPE_IDENTITY();

    PRINT '3 servicios insertados.';

    -- 3. Promociones
    INSERT INTO Promociones (nombre, descripcion, fecha_inicio, fecha_fin, tipo_descuento, valor_descuento, aplica_a, duracion_meses)
      VALUES ('Promo 10% Mensualidad','10% de descuento en mensualidad','2025-01-01','2025-12-31','PORCENTAJE',0.10,'MENSUALIDAD',6);
    SET @id_prom_pct = SCOPE_IDENTITY();

    INSERT INTO Promociones (nombre, descripcion, fecha_inicio, fecha_fin, tipo_descuento, valor_descuento, aplica_a, duracion_meses)
      VALUES ('Descarga $200 Instalación','200 pesos de descuento','2025-03-01','2025-09-30','MONTO_FIJO',200.00,'INSTALACION',1);
    SET @id_prom_fijo = SCOPE_IDENTITY();

    INSERT INTO Promociones (nombre, descripcion, fecha_inicio, fecha_fin, tipo_descuento, valor_descuento, aplica_a, duracion_meses)
      VALUES ('2 Meses Gratis','Dos meses de servicio gratis','2025-06-01','2026-06-01','MESES_GRATIS',2,'MENSUALIDAD',2);
    SET @id_prom_meses = SCOPE_IDENTITY();

    INSERT INTO Promociones (nombre, descripcion, fecha_inicio, fecha_fin, tipo_descuento, valor_descuento, aplica_a, duracion_meses)
      VALUES ('Promo Zona Coyoacán','15% para Del Carmen','2025-05-01','2025-11-30','PORCENTAJE',0.15,'MENSUALIDAD',6);
    SET @id_prom_geo = SCOPE_IDENTITY();

    PRINT '4 promociones insertadas.';

    -- 4. Vincular promociones a servicios
    INSERT INTO Promocion_Servicio (id_promocion, id_servicio) VALUES
      (@id_prom_pct,   @id_serv_internet),
      (@id_prom_fijo,  @id_serv_telefonia),
      (@id_prom_meses, @id_serv_internet),
      (@id_prom_meses, @id_serv_tv),
      (@id_prom_geo,   @id_serv_internet);

    PRINT 'Promociones vinculadas a servicios.';

    -- 5. Domicilios y Sucursales
    INSERT INTO Domicilios (calle, numero_exterior, numero_interior, referencias, id_colonia)
      VALUES ('Av. Patria',                '1234', NULL,           'Frente a Parque', @id_col1); SET @id_domic1 = SCOPE_IDENTITY();
    INSERT INTO Domicilios (calle, numero_exterior, numero_interior, referencias, id_colonia)
      VALUES ('Blvd. Díaz Ordaz',          '4321', 'A',            'Local 5',         @id_col2); SET @id_domic2 = SCOPE_IDENTITY();
    INSERT INTO Domicilios (calle, numero_exterior, numero_interior, referencias, id_colonia)
      VALUES ('Calle Falsa',               '321',  NULL,           'Esquina',         @id_col3); SET @id_domic3 = SCOPE_IDENTITY();
    INSERT INTO Domicilios (calle, numero_exterior, numero_interior, referencias, id_colonia)
      VALUES ('Av. Miguel Ángel de Quevedo','150', NULL,           NULL,               @id_col3); SET @id_domic4 = SCOPE_IDENTITY();

    INSERT INTO Sucursales (nombre, id_domicilio, telefono)
      VALUES ('Sucursal Guadalajara', @id_domic1, '33-1234-5678'); SET @id_suc1 = SCOPE_IDENTITY();
    INSERT INTO Sucursales (nombre, id_domicilio, telefono)
      VALUES ('Sucursal Monterrey',   @id_domic2, '81-8765-4321'); SET @id_suc2 = SCOPE_IDENTITY();

    PRINT 'Domicilios y sucursales insertados.';

    -- 6. Suscriptores y Contratos (con RFC único)
    INSERT INTO Suscriptores (
      nombre, apellido_paterno, apellido_materno, rfc,               email,                      telefono_contacto, id_domicilio
    ) VALUES
    ('Luis',   'Pérez',   'Gómez',   'PEGL850101ABC', 'luis.perez@mail.com',   '33-1111-2222', @id_domic1);
    SET @id_sub1 = SCOPE_IDENTITY();

    INSERT INTO Suscriptores (
      nombre, apellido_paterno, apellido_materno, rfc,               email,                      telefono_contacto, id_domicilio
    ) VALUES
    ('María',  'López',   NULL,      'LOML900202DEF', 'maria.lopez@mail.com',   '81-3333-4444', @id_domic2);
    SET @id_sub2 = SCOPE_IDENTITY();

    INSERT INTO Suscriptores (
      nombre, apellido_paterno, apellido_materno, rfc,               email,                         telefono_contacto, id_domicilio
    ) VALUES
    ('Carlos', 'Ramírez', 'Díaz',    'CARR950303GHI','carlos.ramirez@mail.com','55-5555-6666', @id_domic3);
    SET @id_sub3 = SCOPE_IDENTITY();

    INSERT INTO Suscriptores (
      nombre, apellido_paterno, apellido_materno, rfc,               email,                       telefono_contacto, id_domicilio
    ) VALUES
    ('Sofía',  'Herrera', 'Ortiz',   'SOHO970404JKL','sofia.herrera@mail.com','55-7777-8888', @id_domic4);
    SET @id_sub4 = SCOPE_IDENTITY();

    INSERT INTO Contratos (id_suscriptor, id_sucursal, fecha_contratacion, plazo_forzoso_meses, estado)
      VALUES (@id_sub1, @id_suc1, '2025-01-15', 12, 'Activo'); SET @id_contrato1 = SCOPE_IDENTITY();
    INSERT INTO Contratos (id_suscriptor, id_sucursal, fecha_contratacion, plazo_forzoso_meses, estado)
      VALUES (@id_sub2, @id_suc2, '2025-03-20', 6,  'Activo'); SET @id_contrato2 = SCOPE_IDENTITY();
    INSERT INTO Contratos (id_suscriptor, id_sucursal, fecha_contratacion, plazo_forzoso_meses, estado)
      VALUES (@id_sub3, @id_suc1, '2025-06-05', 0,  'Activo'); SET @id_contrato3 = SCOPE_IDENTITY();
    INSERT INTO Contratos (id_suscriptor, id_sucursal, fecha_contratacion, plazo_forzoso_meses, estado)
      VALUES (@id_sub4, @id_suc2, '2025-05-10', 3,  'Activo'); SET @id_contrato4 = SCOPE_IDENTITY();

    PRINT 'Suscriptores y contratos insertados.';

    -- 7. Servicios por contrato
    INSERT INTO Contrato_Servicios (id_contrato, id_servicio, precio_contratado)
      VALUES 
        (@id_contrato1, @id_serv_internet, 650.00),
        (@id_contrato2, @id_serv_telefonia, 200.00),
        (@id_contrato2, @id_serv_tv,        300.00),
        (@id_contrato3, @id_serv_internet, 650.00),
        (@id_contrato3, @id_serv_tv,        300.00),
        (@id_contrato4, @id_serv_internet, 650.00);
    PRINT 'Contrato_Servicios insertados.';

    -- 8. Aplicación de promociones a contratos
    INSERT INTO Contrato_Promociones (id_contrato, id_promocion, fecha_aplicacion, metadata)
      VALUES 
        (@id_contrato1, @id_prom_pct,   '2025-01-15', NULL),
        (@id_contrato2, @id_prom_fijo,  '2025-03-20', NULL),
        (@id_contrato3, @id_prom_meses, '2025-06-05', NULL),
        (@id_contrato4, @id_prom_geo,   '2025-05-10', '{"zona":"Del Carmen"}');
    PRINT 'Contrato_Promociones insertados.';

    -- 9. Alcance geográfico para la Promo Zona Coyoacán
    INSERT INTO Promocion_Alcance (id_promocion, id_estado, id_municipio, id_ciudad, id_colonia, id_sucursal)
      VALUES (@id_prom_geo, NULL, NULL, NULL, @id_col3, NULL);
    PRINT 'Promocion_Alcance insertado para Promo Zona Coyoacán.';

    -- 10. Movimientos de cuenta
    INSERT INTO Movimientos_Cuenta (id_contrato, concepto, monto_cargo, monto_pago, saldo_resultante)
      VALUES
        (@id_contrato1, 'Mensualidad Ene 2025', 650.00, 0.00, 650.00),
        (@id_contrato1, 'Pago con tarjeta',      0.00, 300.00, 350.00),
        (@id_contrato2, 'Mensualidad Abr 2025', 500.00, 0.00, 500.00),
        (@id_contrato2, 'Pago con efectivo',     0.00, 500.00,   0.00),
        (@id_contrato3, 'Mensualidad Jun 2025', 950.00, 0.00, 950.00),
        (@id_contrato4, 'Mensualidad May 2025', 650.00 * 0.85, 0.00, 552.50);
    PRINT 'Movimientos_Cuenta insertados.';

    PRINT '--- FIN: INSERCIÓN DE DATOS COMPLETADA ---';
END TRY
BEGIN CATCH
    PRINT 'Ocurrió un error durante la inserción de datos.';
    PRINT 'Error Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Error Mensaje: ' + ERROR_MESSAGE();
END CATCH;
GO