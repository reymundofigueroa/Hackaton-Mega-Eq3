-- 1) Listado de todas las promociones (al menos 3) con sus tipos de descuento
SELECT
  id_promocion,
  nombre,
  tipo_descuento,
  valor_descuento,
  aplica_a,
  duracion_meses,
  fecha_inicio,
  fecha_fin
FROM dbo.Promociones;

-- 2) Suscriptores con sus servicios contratados y la promoción aplicada (si la hubiera)
SELECT
  s.id_suscriptor,
  CONCAT(s.nombre, ' ', s.apellido_paterno, ISNULL(' ' + s.apellido_materno, '')) AS Suscriptor,
  sv.nombre AS Servicio,
  cs.precio_contratado,
  p.nombre AS Promocion,
  p.tipo_descuento,
  cp.fecha_aplicacion
FROM dbo.Suscriptores s
  INNER JOIN dbo.Contratos c
    ON s.id_suscriptor = c.id_suscriptor
  INNER JOIN dbo.Contrato_Servicios cs
    ON c.id_contrato = cs.id_contrato
  INNER JOIN dbo.Servicios sv
    ON cs.id_servicio = sv.id_servicio
  LEFT JOIN dbo.Contrato_Promociones cp
    ON c.id_contrato = cp.id_contrato
  LEFT JOIN dbo.Promociones p
    ON cp.id_promocion = p.id_promocion
ORDER BY s.id_suscriptor, sv.nombre;

-- 3) Cálculo de deuda actual de un suscriptor (cargo total – pago total)
--    Reemplaza @idSub con el id_suscriptor que quieras consultar
DECLARE @idSub INT = 1;

SELECT
  s.id_suscriptor,
  CONCAT(s.nombre, ' ', s.apellido_paterno) AS Suscriptor,
  SUM(mc.monto_cargo)    AS Total_Cargos,
  SUM(mc.monto_pago)     AS Total_Pagos,
  SUM(mc.monto_cargo) - SUM(mc.monto_pago) AS Saldo_Pendiente
FROM dbo.Suscriptores s
  INNER JOIN dbo.Contratos c
    ON s.id_suscriptor = c.id_suscriptor
  INNER JOIN dbo.Movimientos_Cuenta mc
    ON c.id_contrato = mc.id_contrato
WHERE s.id_suscriptor = @idSub
GROUP BY s.id_suscriptor, CONCAT(s.nombre, ' ', s.apellido_paterno);

-- 4) Suscriptor(s) con promoción geográfica aplicada en su zona
--    (caso de la “Promo Zona Coyoacán”)
SELECT
  s.id_suscriptor,
  CONCAT(s.nombre, ' ', s.apellido_paterno)   AS Suscriptor,
  d.calle + ' ' + d.numero_exterior           AS Domicilio,
  col.nombre         AS Colonia,
  pz.nombre          AS Promocion_Geo,
  pa.id_colonia      AS Colonia_Aplicacion
FROM dbo.Suscriptores s
  INNER JOIN dbo.Domicilios d
    ON s.id_domicilio = d.id_domicilio
  INNER JOIN dbo.Colonias col
    ON d.id_colonia = col.id_colonia
  INNER JOIN dbo.Contrato_Promociones cp
    ON s.id_suscriptor = cp.id_contrato /* Nota: igualamos por contrato */
  INNER JOIN dbo.Promociones pz
    ON cp.id_promocion = pz.id_promocion
  INNER JOIN dbo.Promocion_Alcance pa
    ON pz.id_promocion = pa.id_promocion
WHERE pz.nombre = 'Promo Zona Coyoacán'
  AND pa.id_colonia = d.id_colonia;










