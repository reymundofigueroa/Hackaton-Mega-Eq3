# 🎯 API Promo Configurator

API RESTful para la gestión de promociones, contratos, suscriptores y servicios de una empresa de telecomunicaciones. Desarrollada en ASP.NET Core 8 con enfoque Database First y arquitectura por capas (DTOs, repositorios, mapeo con AutoMapper).

## Modelo relacional de la base de datos
![Esquema_Relacional](https://github.com/user-attachments/assets/949a22e3-8616-47c6-8015-47584473bc12)

---

## 📦 Estructura del Proyecto

- `Controllers/` — Endpoints públicos de la API.
- `Models/` — Entidades generadas desde la base de datos (DB First).
- `Dtos/` — Objetos de transferencia de datos entre cliente ↔ API.
- `Repository/` y `IRepository/` — Capa de acceso y lógica de datos.
- `Mapping/` — Configuración de AutoMapper para transformación de datos.
- `Data/ApplicationDbContext.cs` — Contexto EF Core que gestiona las entidades y relaciones.

---

## 🔗 Relaciones Clave en la Base de Datos

### 📘 Relación de entidades principales

| Entidad | Relación | Descripción |
|--------|----------|-------------|
| `Contratos` ↔ `Suscriptores` | 1:N | Un suscriptor puede tener varios contratos. |
| `Contratos` ↔ `Servicios` | N:M | Mediante tabla intermedia `Contrato_Servicios`. |
| `Contratos` ↔ `Promociones` | N:M | Mediante `Contrato_Promociones`. Se registra `fecha_aplicacion`. |
| `Promociones` ↔ `Servicios` | N:M | Tabla intermedia `Promocion_Servicio`. Define qué servicios incluye cada promoción. |
| `Promociones` ↔ `Zonas geográficas` | 1:N | Se define mediante `Promocion_Alcance`. Incluye estado, municipio, ciudad, colonia y sucursal. |
| `Suscriptores` ↔ `Domicilios` | 1:1 | El suscriptor está ligado a un domicilio. |
| `Sucursales` ↔ `Colonias` | N:M | Relación manejada mediante `Sucursal_Colonia`. |
| `MovimientosCuenta` ↔ `Contratos` | 1:N | Cada contrato puede tener múltiples movimientos contables. |

---

## 🧪 Endpoints principales

### 📄 Contratos

- `GET /api/contratos`  
  Listado de contratos con su suscriptor y servicios.

- `POST /api/contratos`  
  Crea un nuevo contrato con servicios contratados.

### 🛍️ Promociones

- `GET /api/promociones`  
  Lista todas las promociones disponibles, junto con los servicios asociados.

- `POST /api/promociones/crear-completa`  
  Crea una promoción nueva incluyendo sus servicios y zonas de alcance.

### 🔄 Contrato Promociones

- `GET /api/contratoPromociones`  
  Retorna todas las promociones aplicadas a contratos.

- `GET /api/contratoPromociones/contrato/{id}`  
  Promociones activas para un contrato específico.

- `POST /api/contratoPromociones`  
  Asigna una promoción a un contrato existente.

#### Ejemplo de `POST /api/contratoPromociones`

```json
{
  "idContrato": 1,
  "idPromocion": 2
}

