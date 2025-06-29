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

    -- Reseed de identidades
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
      @id_estado1 INT, @id_estado2 INT, @id_estado3 INT, @id_estado4 INT, @id_estado5 INT,
      @id_mun1 INT, @id_mun2 INT, @id_mun3 INT, @id_mun4 INT, @id_mun5 INT,
      @id_ciud1 INT, @id_ciud2 INT, @id_ciud3 INT, @id_ciud4 INT, @id_ciud5 INT,
      @id_col1 INT, @id_col2 INT, @id_col3 INT, @id_col4 INT, @id_col5 INT,
      @id_serv_internet INT, @id_serv_telefonia INT, @id_serv_tv INT,
      @id_prom_pct INT, @id_prom_fijo INT, @id_prom_meses INT, @id_prom_geo INT, @id_prom_verano INT, @id_prom_lealtad INT,
      @id_domic1 INT, @id_domic2 INT, @id_domic3 INT, @id_domic4 INT, @id_domic5 INT, @id_domic6 INT,
      @id_suc1 INT, @id_suc2 INT, @id_suc3 INT,
      @id_sub1 INT, @id_sub2 INT, @id_sub3 INT, @id_sub4 INT, @id_sub5 INT, @id_sub6 INT,
      @id_contrato1 INT, @id_contrato2 INT, @id_contrato3 INT, @id_contrato4 INT, @id_contrato5 INT, @id_contrato6 INT;

    -- 1. Geografía (AMPLIADO)
    INSERT INTO Estados (nombre) VALUES ('Jalisco'), ('Nuevo León'), ('Ciudad de México'), ('Querétaro'), ('Yucatán');
    SET @id_estado1 = 1; SET @id_estado2 = 2; SET @id_estado3 = 3; SET @id_estado4 = 4; SET @id_estado5 = 5;
    
    INSERT INTO Municipios (nombre, id_estado) VALUES ('Guadalajara',@id_estado1), ('Monterrey',@id_estado2), ('Coyoacán',@id_estado3), ('Querétaro', @id_estado4), ('Mérida', @id_estado5);
    SET @id_mun1 = 1; SET @id_mun2 = 2; SET @id_mun3 = 3; SET @id_mun4 = 4; SET @id_mun5 = 5;

    INSERT INTO Ciudades (nombre, id_municipio) VALUES ('Zapopan',@id_mun1), ('San Pedro Garza García',@id_mun2), ('Ciudad de México',@id_mun3), ('Querétaro', @id_mun4), ('Mérida', @id_mun5);
    SET @id_ciud1 = 1; SET @id_ciud2 = 2; SET @id_ciud3 = 3; SET @id_ciud4 = 4; SET @id_ciud5 = 5;

    INSERT INTO Colonias (nombre, codigo_postal, id_ciudad) VALUES ('Chapalita', '45010', @id_ciud1), ('Valle Oriente','66269', @id_ciud2), ('Del Carmen', '04100', @id_ciud3), ('Juriquilla', '76226', @id_ciud4), ('Altabrisa', '97130', @id_ciud5);
    SET @id_col1 = 1; SET @id_col2 = 2; SET @id_col3 = 3; SET @id_col4 = 4; SET @id_col5 = 5;
    PRINT '5 estados, municipios, ciudades y colonias insertados.';

    -- 2. Servicios (SIN CAMBIOS)
    INSERT INTO Servicios (nombre, descripcion, precio_base_actual) VALUES ('Internet 100 Mbps', 'Internet residencial 100 Mbps', 650.00), ('Telefonía Ilimitada', 'Llamadas nacionales ilimitadas', 200.00), ('TV HD Interactiva', 'Más de 80 canales en HD', 300.00);
    SET @id_serv_internet = 1; SET @id_serv_telefonia = 2; SET @id_serv_tv = 3;
    PRINT '3 servicios insertados.';

    -- 3. Promociones (AMPLIADO)
    INSERT INTO Promociones (nombre, descripcion, fecha_inicio, fecha_fin, tipo_descuento, valor_descuento, aplica_a, duracion_meses) VALUES ('Promo 10% Mensualidad','10% de descuento en mensualidad','2025-01-01','2025-12-31','PORCENTAJE',0.10,'MENSUALIDAD',6), ('Descarga $200 Instalación','200 pesos de descuento','2025-03-01','2025-09-30','MONTO_FIJO',200.00,'INSTALACION',1), ('2 Meses Gratis','Dos meses de servicio gratis','2025-06-01','2026-06-01','MESES_GRATIS',2,'MENSUALIDAD',2), ('Promo Zona Coyoacán','15% para Del Carmen','2025-05-01','2025-11-30','PORCENTAJE',0.15,'MENSUALIDAD',6), ('Promo Verano TV','20% en tu paquete de TV por 3 meses','2025-06-20','2025-08-20','PORCENTAJE',0.20,'MENSUALIDAD',3), ('Promo Lealtad Total','Descuento especial para clientes leales','2025-01-01','2025-12-31','MONTO_FIJO',50.00,'MENSUALIDAD',12);
    SET @id_prom_pct = 1; SET @id_prom_fijo = 2; SET @id_prom_meses = 3; SET @id_prom_geo = 4; SET @id_prom_verano = 5; SET @id_prom_lealtad = 6;
    PRINT '6 promociones insertadas.';

    -- 4. Vincular promociones a servicios (AMPLIADO)
    INSERT INTO Promocion_Servicio (id_promocion, id_servicio) VALUES (@id_prom_pct,@id_serv_internet), (@id_prom_fijo,@id_serv_telefonia), (@id_prom_meses,@id_serv_internet), (@id_prom_meses,@id_serv_tv), (@id_prom_geo,@id_serv_internet), (@id_prom_verano, @id_serv_tv), (@id_prom_lealtad, @id_serv_internet), (@id_prom_lealtad, @id_serv_telefonia), (@id_prom_lealtad, @id_serv_tv);
    PRINT 'Promociones vinculadas a servicios.';

    -- 5. Domicilios y Sucursales (AMPLIADO)
    INSERT INTO Domicilios (calle, numero_exterior, id_colonia) VALUES ('Av. Patria','1234',@id_col1), ('Blvd. Díaz Ordaz','4321',@id_col2), ('Calle Falsa','321',@id_col3), ('Av. Miguel Ángel de Quevedo','150',@id_col3), ('Blvd. de la Campana','500',@id_col4), ('Calle 20','311',@id_col5);
    SET @id_domic1=1; SET @id_domic2=2; SET @id_domic3=3; SET @id_domic4=4; SET @id_domic5=5; SET @id_domic6=6;

    INSERT INTO Sucursales (nombre, id_domicilio, telefono) VALUES ('Sucursal Guadalajara', @id_domic1, '33-1234-5678'), ('Sucursal Monterrey', @id_domic2, '81-8765-4321'), ('Sucursal Querétaro', @id_domic5, '44-2123-2123');
    SET @id_suc1=1; SET @id_suc2=2; SET @id_suc3=3;
    PRINT 'Domicilios y sucursales insertados.';

    -- 6. Cobertura de Sucursales (NUEVO)
    INSERT INTO Sucursal_Colonia (id_sucursal, id_colonia) VALUES (@id_suc1, @id_col1), (@id_suc2, @id_col2), (@id_suc3, @id_col4), (@id_suc1, @id_col3); -- Suc. GDL opera en Chapalita y Del Carmen
    PRINT 'Cobertura de Sucursal_Colonia insertada.';

    -- 7. Suscriptores y Contratos (AMPLIADO)
    INSERT INTO Suscriptores (nombre,apellido_paterno,rfc,email,telefono_contacto,id_domicilio) VALUES ('Luis','Pérez','PEGL850101ABC','luis.perez@mail.com','33-1111-2222',@id_domic1), ('María','López','LOML900202DEF','maria.lopez@mail.com','81-3333-4444',@id_domic2), ('Carlos','Ramírez','CARR950303GHI','carlos.ramirez@mail.com','55-5555-6666',@id_domic3), ('Sofía','Herrera','SOHO970404JKL','sofia.herrera@mail.com','55-7777-8888',@id_domic4), ('Ana','García','GAAA920505MNO','ana.garcia@mail.com','44-2999-8888',@id_domic5), ('David','Martínez','MADM880606PQR','david.martinez@mail.com','99-9111-2222',@id_domic6);
    SET @id_sub1=1; SET @id_sub2=2; SET @id_sub3=3; SET @id_sub4=4; SET @id_sub5=5; SET @id_sub6=6;

    INSERT INTO Contratos (id_suscriptor,id_sucursal,fecha_contratacion,plazo_forzoso_meses,estado) VALUES (@id_sub1,@id_suc1,'2025-01-15',12,'Activo'), (@id_sub2,@id_suc2,'2025-03-20',6,'Activo'), (@id_sub3,@id_suc1,'2025-06-05',0,'Activo'), (@id_sub4,@id_suc2,'2025-05-10',3,'Activo'), (@id_sub5,@id_suc3,'2025-06-25',12,'Activo'), (@id_sub6,@id_suc3,'2025-04-01',0,'Suspendido');
    SET @id_contrato1=1; SET @id_contrato2=2; SET @id_contrato3=3; SET @id_contrato4=4; SET @id_contrato5=5; SET @id_contrato6=6;
    PRINT 'Suscriptores y contratos insertados.';

    -- 8. Servicios por contrato (AMPLIADO)
    INSERT INTO Contrato_Servicios (id_contrato,id_servicio,precio_contratado) VALUES (@id_contrato1,@id_serv_internet,650.00), (@id_contrato2,@id_serv_telefonia,200.00), (@id_contrato2,@id_serv_tv,300.00), (@id_contrato3,@id_serv_internet,650.00), (@id_contrato3,@id_serv_tv,300.00), (@id_contrato4,@id_serv_internet,650.00), (@id_contrato5,@id_serv_tv,300.00), (@id_contrato6,@id_serv_internet,650.00), (@id_contrato6,@id_serv_telefonia,200.00);
    PRINT 'Contrato_Servicios insertados.';

    -- 9. Aplicación de promociones a contratos (AMPLIADO CON REGLAS)
    INSERT INTO Contrato_Promociones (id_contrato,id_promocion,fecha_aplicacion) VALUES (@id_contrato1,@id_prom_pct,'2025-01-15'), (@id_contrato2,@id_prom_fijo,'2025-03-20'), (@id_contrato3,@id_prom_meses,'2025-06-05'), (@id_contrato4,@id_prom_geo,'2025-05-10'), (@id_contrato5,@id_prom_verano,'2025-06-25'); -- Solo se agrega promo a un contrato nuevo
    PRINT 'Contrato_Promociones insertados.';

    -- 10. Alcance geográfico
    INSERT INTO Promocion_Alcance (id_promocion,id_colonia) VALUES (@id_prom_geo, @id_col3);
    PRINT 'Promocion_Alcance insertado.';

    -- 11. Movimientos de cuenta (AMPLIADO CON REGLAS)
    INSERT INTO Movimientos_Cuenta (id_contrato,concepto,monto_cargo,monto_pago,saldo_resultante) VALUES (@id_contrato1,'Mensualidad Ene 2025',650.00,0.00,650.00), (@id_contrato1,'Pago con tarjeta',0.00,300.00,350.00), (@id_contrato2,'Mensualidad Abr 2025',500.00,0.00,500.00), (@id_contrato2,'Pago con efectivo',0.00,500.00,0.00), (@id_contrato3,'Mensualidad Jun 2025',950.00,0.00,950.00), (@id_contrato4,'Mensualidad May 2025',650.00 * 0.85,0.00,552.50), (@id_contrato5,'Mensualidad Jul 2025',300.00 * 0.80,0.00,240.00); -- Movimiento solo para el nuevo contrato con promo
    PRINT 'Movimientos_Cuenta insertados.';

    PRINT '--- FIN: INSERCIÓN DE DATOS COMPLETADA ---';
END TRY
BEGIN CATCH
    PRINT 'Ocurrió un error durante la inserción de datos.';
    PRINT 'Error Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Error Mensaje: ' + ERROR_MESSAGE();
END CATCH;
GO