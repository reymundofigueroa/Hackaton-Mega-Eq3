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
    PRINT 'Ocurri� un error durante la limpieza de datos.';
    PRINT 'Error N�mero: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Error Mensaje: ' + ERROR_MESSAGE();
END CATCH;
GO

-- ====================================================
-- Bloque 2: Inserci�n de datos de prueba
-- ====================================================
BEGIN TRY
    PRINT '--- INICIO: INSERCI�N DE DATOS ---';

    -- Variables para IDs
    DECLARE
      @id_estado1 INT, @id_estado2 INT, @id_estado3 INT, @id_estado4 INT, @id_estado5 INT,
      @id_mun1 INT, @id_mun2 INT, @id_mun3 INT, @id_mun4 INT, @id_mun5 INT,
      @id_ciud1 INT, @id_ciud2 INT, @id_ciud3 INT, @id_ciud4 INT, @id_ciud5 INT,
      @id_col1 INT, @id_col2 INT, @id_col3 INT, @id_col4 INT, @id_col5 INT,
      @id_serv_internet INT, @id_serv_telefonia INT, @id_serv_tv INT,
      @id_prom_pct INT, @id_prom_fijo INT, @id_prom_meses INT, @id_prom_geo INT, @id_prom_verano INT, @id_prom_lealtad INT,
      @id_domic1 INT, @id_domic2 INT, @id_domic3 INT, @id_domic4 INT, @id_domic5 INT, @id_domic6 INT, @id_domic7 INT, @id_domic8 INT, @id_domic9 INT, @id_domic10 INT, @id_domic11 INT, @id_domic12 INT,
      @id_suc1 INT, @id_suc2 INT, @id_suc3 INT,
      @id_sub1 INT, @id_sub2 INT, @id_sub3 INT, @id_sub4 INT, @id_sub5 INT, @id_sub6 INT, @id_sub7 INT, @id_sub8 INT, @id_sub9 INT, @id_sub10 INT, @id_sub11 INT, @id_sub12 INT,
      @id_contrato1 INT, @id_contrato2 INT, @id_contrato3 INT, @id_contrato4 INT, @id_contrato5 INT, @id_contrato6 INT, @id_contrato7 INT, @id_contrato8 INT, @id_contrato9 INT, @id_contrato10 INT, @id_contrato11 INT, @id_contrato12 INT;

    -- 1. Geograf�a
    INSERT INTO Estados (nombre) VALUES ('Jalisco'), ('Nuevo Le�n'), ('Ciudad de M�xico'), ('Quer�taro'), ('Yucat�n');
    SET @id_estado1 = 1; SET @id_estado2 = 2; SET @id_estado3 = 3; SET @id_estado4 = 4; SET @id_estado5 = 5;
    
    INSERT INTO Municipios (nombre, id_estado) VALUES ('Guadalajara',@id_estado1), ('Monterrey',@id_estado2), ('Coyoac�n',@id_estado3), ('Quer�taro', @id_estado4), ('M�rida', @id_estado5);
    SET @id_mun1 = 1; SET @id_mun2 = 2; SET @id_mun3 = 3; SET @id_mun4 = 4; SET @id_mun5 = 5;

    INSERT INTO Ciudades (nombre, id_municipio) VALUES ('Zapopan',@id_mun1), ('San Pedro Garza Garc�a',@id_mun2), ('Ciudad de M�xico',@id_mun3), ('Quer�taro', @id_mun4), ('M�rida', @id_mun5);
    SET @id_ciud1 = 1; SET @id_ciud2 = 2; SET @id_ciud3 = 3; SET @id_ciud4 = 4; SET @id_ciud5 = 5;

    INSERT INTO Colonias (nombre, codigo_postal, id_ciudad) VALUES ('Chapalita', '45010', @id_ciud1), ('Valle Oriente','66269', @id_ciud2), ('Del Carmen', '04100', @id_ciud3), ('Juriquilla', '76226', @id_ciud4), ('Altabrisa', '97130', @id_ciud5);
    SET @id_col1 = 1; SET @id_col2 = 2; SET @id_col3 = 3; SET @id_col4 = 4; SET @id_col5 = 5;
    PRINT '5 estados, municipios, ciudades y colonias insertados.';

    -- 2. Servicios
    INSERT INTO Servicios (nombre, descripcion, precio_base_actual) VALUES ('Internet 100 Mbps', 'Internet residencial 100 Mbps', 650.00), ('Telefon�a Ilimitada', 'Llamadas nacionales ilimitadas', 200.00), ('TV HD Interactiva', 'M�s de 80 canales en HD', 300.00);
    SET @id_serv_internet = 1; SET @id_serv_telefonia = 2; SET @id_serv_tv = 3;
    PRINT '3 servicios insertados.';

    -- 3. Promociones
    INSERT INTO Promociones (nombre, descripcion, fecha_inicio, fecha_fin, tipo_descuento, valor_descuento, aplica_a, duracion_meses) VALUES ('Promo 10% Mensualidad','10% de descuento en mensualidad','2025-01-01','2025-12-31','PORCENTAJE',0.10,'MENSUALIDAD',6), ('Descarga $200 Instalaci�n','200 pesos de descuento','2025-03-01','2025-09-30','MONTO_FIJO',200.00,'INSTALACION',1), ('2 Meses Gratis','Dos meses de servicio gratis','2025-06-01','2026-06-01','MESES_GRATIS',2,'MENSUALIDAD',2), ('Promo Zona Coyoac�n','15% para Del Carmen','2025-05-01','2025-11-30','PORCENTAJE',0.15,'MENSUALIDAD',6), ('Promo Verano TV','20% en tu paquete de TV por 3 meses','2025-06-20','2025-08-20','PORCENTAJE',0.20,'MENSUALIDAD',3), ('Promo Lealtad Total','Descuento especial para clientes leales','2025-01-01','2025-12-31','MONTO_FIJO',50.00,'MENSUALIDAD',12);
    SET @id_prom_pct = 1; SET @id_prom_fijo = 2; SET @id_prom_meses = 3; SET @id_prom_geo = 4; SET @id_prom_verano = 5; SET @id_prom_lealtad = 6;
    PRINT '6 promociones insertadas.';

    -- 4. Vincular promociones a servicios
    INSERT INTO Promocion_Servicio (id_promocion, id_servicio) VALUES (@id_prom_pct,@id_serv_internet), (@id_prom_fijo,@id_serv_telefonia), (@id_prom_meses,@id_serv_tv), (@id_prom_geo,@id_serv_internet), (@id_prom_verano, @id_serv_tv), (@id_prom_lealtad, @id_serv_internet);
    PRINT 'Promociones vinculadas a servicios.';

    -- 5. Domicilios y Sucursales (AMPLIADO)
    INSERT INTO Domicilios (calle, numero_exterior, id_colonia) VALUES 
      ('Av. Patria','1234',@id_col1), ('Blvd. D�az Ordaz','4321',@id_col2), ('Calle Falsa','321',@id_col3), ('Av. Miguel �ngel de Quevedo','150',@id_col3), ('Blvd. de la Campana','500',@id_col4), ('Calle 20','311',@id_col5),
      -- (6 Domicilios Adicionales)
      ('Paseo de la Reforma', '222', @id_col3), ('Av. L�zaro C�rdenas', '2305', @id_col2), ('Calle Hidalgo', '56', @id_col1), ('Circuito Juriquilla', '1030', @id_col4), ('Calle 60', '455', @id_col5), ('Av. de los Insurgentes', '2021', @id_col3);
    SET @id_domic1=1; SET @id_domic2=2; SET @id_domic3=3; SET @id_domic4=4; SET @id_domic5=5; SET @id_domic6=6; SET @id_domic7=7; SET @id_domic8=8; SET @id_domic9=9; SET @id_domic10=10; SET @id_domic11=11; SET @id_domic12=12;

    INSERT INTO Sucursales (nombre, id_domicilio, telefono) VALUES ('Sucursal Guadalajara', @id_domic1, '33-1234-5678'), ('Sucursal Monterrey', @id_domic2, '81-8765-4321'), ('Sucursal Quer�taro', @id_domic5, '44-2123-2123');
    SET @id_suc1=1; SET @id_suc2=2; SET @id_suc3=3;
    PRINT '12 Domicilios y 3 sucursales insertados.';

    -- 6. Cobertura de Sucursales
    INSERT INTO Sucursal_Colonia (id_sucursal, id_colonia) VALUES (@id_suc1, @id_col1), (@id_suc2, @id_col2), (@id_suc3, @id_col4), (@id_suc1, @id_col3);
    PRINT 'Cobertura de Sucursal_Colonia insertada.';

    -- 7. Suscriptores y Contratos (AMPLIADO)
    INSERT INTO Suscriptores (nombre,apellido_paterno,rfc,email,telefono_contacto,id_domicilio) VALUES 
      ('Luis','P�rez','PEGL850101ABC','luis.perez@mail.com','33-1111-2222',@id_domic1), ('Mar�a','L�pez','LOML900202DEF','maria.lopez@mail.com','81-3333-4444',@id_domic2), ('Carlos','Ram�rez','CARR950303GHI','carlos.ramirez@mail.com','55-5555-6666',@id_domic3), ('Sof�a','Herrera','SOHO970404JKL','sofia.herrera@mail.com','55-7777-8888',@id_domic4), ('Ana','Garc�a','GAAA920505MNO','ana.garcia@mail.com','44-2999-8888',@id_domic5), ('David','Mart�nez','MADM880606PQR','david.martinez@mail.com','99-9111-2222',@id_domic6),
      -- (6 Suscriptores Adicionales)
      ('Laura','S�nchez','SALL910707STU','laura.sanchez@mail.com','55-1212-3434',@id_domic7), ('Jorge','G�mez','GOFJ890808VWX','jorge.gomez@mail.com','81-3434-5656',@id_domic8), ('Patricia','Flores','FLOP930909YZA','patricia.flores@mail.com','33-5656-7878',@id_domic9), ('Miguel','Rojas','ROMM941010BCD','miguel.rojas@mail.com','44-2323-4545',@id_domic10), ('Ver�nica','Cruz','CRUV961111EFG','veronica.cruz@mail.com','99-9595-6767',@id_domic11), ('Ricardo','Mora','MORR871212HIJ','ricardo.mora@mail.com','55-6767-8989',@id_domic12);
    SET @id_sub1=1; SET @id_sub2=2; SET @id_sub3=3; SET @id_sub4=4; SET @id_sub5=5; SET @id_sub6=6; SET @id_sub7=7; SET @id_sub8=8; SET @id_sub9=9; SET @id_sub10=10; SET @id_sub11=11; SET @id_sub12=12;

    INSERT INTO Contratos (id_suscriptor,id_sucursal,fecha_contratacion,plazo_forzoso_meses,estado) VALUES 
      (@id_sub1,@id_suc1,'2025-01-15',12,'Activo'), (@id_sub2,@id_suc2,'2025-03-20',6,'Activo'), (@id_sub3,@id_suc1,'2025-06-05',0,'Activo'), (@id_sub4,@id_suc2,'2025-05-10',3,'Activo'), (@id_sub5,@id_suc3,'2025-06-25',12,'Activo'), (@id_sub6,@id_suc3,'2025-04-01',0,'Suspendido'),
      -- (6 Contratos Adicionales)
      (@id_sub7,@id_suc1,'2025-02-10',0,'Activo'), (@id_sub8,@id_suc2,'2025-03-11',0,'Activo'), (@id_sub9,@id_suc1,'2025-04-12',6,'Cancelado'), (@id_sub10,@id_suc3,'2025-05-13',12,'Activo'), (@id_sub11,@id_suc3,'2025-06-14',0,'Activo'), (@id_sub12,@id_suc1,'2025-07-15',3,'Suspendido');
    SET @id_contrato1=1; SET @id_contrato2=2; SET @id_contrato3=3; SET @id_contrato4=4; SET @id_contrato5=5; SET @id_contrato6=6; SET @id_contrato7=7; SET @id_contrato8=8; SET @id_contrato9=9; SET @id_contrato10=10; SET @id_contrato11=11; SET @id_contrato12=12;
    PRINT '12 Suscriptores y contratos insertados.';

    -- 8. Servicios por contrato
    INSERT INTO Contrato_Servicios (id_contrato, id_servicio, precio_contratado) VALUES 
      (@id_contrato1, @id_serv_internet, 650.00), (@id_contrato2, @id_serv_tv, 300.00), (@id_contrato3, @id_serv_internet, 650.00), (@id_contrato4, @id_serv_internet, 650.00), (@id_contrato5, @id_serv_tv, 300.00), (@id_contrato6, @id_serv_telefonia, 200.00),
      -- (6 Servicios para Contratos Adicionales)
      (@id_contrato7, @id_serv_tv, 300.00), (@id_contrato8, @id_serv_internet, 650.00), (@id_contrato9, @id_serv_telefonia, 200.00), (@id_contrato10, @id_serv_tv, 300.00), (@id_contrato11, @id_serv_internet, 650.00), (@id_contrato12, @id_serv_telefonia, 200.00);
    PRINT 'Contrato_Servicios insertados (1 servicio por contrato).';

    -- 9. Aplicaci�n de promociones a contratos
    INSERT INTO Contrato_Promociones (id_contrato,id_promocion,fecha_aplicacion) VALUES (@id_contrato1,@id_prom_pct,'2025-01-15'), (@id_contrato2,@id_prom_fijo,'2025-03-20'), (@id_contrato3,@id_prom_meses,'2025-06-05'), (@id_contrato4,@id_prom_geo,'2025-05-10'), (@id_contrato5,@id_prom_verano,'2025-06-25');
    PRINT 'Contrato_Promociones insertados (solo para los primeros 5 contratos).';

    -- 10. Alcance geogr�fico
    INSERT INTO Promocion_Alcance (id_promocion,id_colonia) VALUES (@id_prom_geo, @id_col3);
    PRINT 'Promocion_Alcance insertado.';

    -- 11. Movimientos de cuenta
    INSERT INTO Movimientos_Cuenta (id_contrato,concepto,monto_cargo,monto_pago,saldo_resultante) VALUES
        -- Historial Contrato 1 (Luis, Internet $650, Promo 10% -> $585/mes)
        (@id_contrato1, 'Mensualidad Abr 2025', 585.00, 0.00,   585.00),
        (@id_contrato1, 'Pago con tarjeta',       0.00, 585.00,     0.00),
        (@id_contrato1, 'Mensualidad May 2025', 585.00, 0.00,   585.00),
        (@id_contrato1, 'Mensualidad Jun 2025', 585.00, 0.00,  1170.00),
        (@id_contrato1, 'Pago parcial',           0.00, 600.00,   570.00),

        -- Historial Contrato 2 (Mar�a, TV $300, Promo Desc. Instalaci�n)
        (@id_contrato2, 'Cargo por Instalaci�n',  800.00, 0.00,   800.00), 
        (@id_contrato2, 'Abono por Promo Instalaci�n', 0.00, 200.00, 600.00),
        (@id_contrato2, 'Pago de Instalaci�n',      0.00, 600.00,     0.00),
        (@id_contrato2, 'Mensualidad May 2025',   300.00, 0.00,   300.00),
        (@id_contrato2, 'Pago con efectivo',        0.00, 300.00,     0.00),
        (@id_contrato2, 'Mensualidad Jun 2025',   300.00, 0.00,   300.00),
        
        -- Historial Contrato 3 (Carlos, Internet $650, Promo 2 meses gratis)
        (@id_contrato3, 'Mensualidad Jun 2025 (Promo Gratis)', 650.00, 0.00, 650.00), 
        (@id_contrato3, 'Abono por Promo Mes Gratis',            0.00, 650.00,   0.00),
        (@id_contrato3, 'Mensualidad Jul 2025 (Promo Gratis)', 650.00, 0.00, 650.00),
        (@id_contrato3, 'Abono por Promo Mes Gratis',            0.00, 650.00,   0.00),
        (@id_contrato3, 'Mensualidad Ago 2025',          650.00, 0.00, 650.00),
        
        -- Historial Contrato 4 (Sof�a, Internet $650, Promo Geo 15% -> $552.50/mes)
        (@id_contrato4, 'Mensualidad May 2025', 552.50, 0.00, 552.50),
        (@id_contrato4, 'Pago puntual',           0.00, 552.50,   0.00),
        (@id_contrato4, 'Mensualidad Jun 2025', 552.50, 0.00, 552.50),

        -- Historial Contrato 5 (Ana, TV $300, Promo Verano 20% -> $240/mes)
        (@id_contrato5, 'Mensualidad Jul 2025', 240.00, 0.00, 240.00);
    PRINT 'Movimientos_Cuenta insertados con historial ampliado.';

    PRINT '--- FIN: INSERCI�N DE DATOS COMPLETADA ---';
END TRY
BEGIN CATCH
    PRINT 'Ocurri� un error durante la inserci�n de datos.';
    PRINT 'Error N�mero: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Error Mensaje: ' + ERROR_MESSAGE();
END CATCH;
GO