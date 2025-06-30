# 🎯 API Promo Configurator

API RESTful para la gestión de promociones, contratos, suscriptores y servicios de una empresa de telecomunicaciones. Construida con ASP.NET Core 8 y metodología Database First.

---

## Modelo relacional de la base de datos
![Esquema_Relacional](https://github.com/user-attachments/assets/949a22e3-8616-47c6-8015-47584473bc12)

## 📦 Estructura del Proyecto

- **Controllers**: Define los endpoints HTTP.
- **Models**: Clases generadas desde base de datos (Database First).
- **Dtos**: Objetos de transferencia de datos usados en la API.
- **Repository / IRepository**: Capa de abstracción y lógica de acceso a datos.
- **Mapping**: Archivos AutoMapper para transformar entidades ↔ DTOs.
- **Data**: DbContext (`ApplicationDbContext`) que gestiona la conexión a la base de datos.

---

## 🔌 Endpoints principales

### 📝 Contratos

- `GET /api/contratos`  
  Retorna todos los contratos y sus detalles.

- `POST /api/contratos`  
  Crea un nuevo contrato con sus servicios asociados.

### 🛍️ Promociones

- `GET /api/promociones`  
  Lista todas las promociones, incluyendo los servicios ligados.

- `POST /api/promociones/crear-completa`  
  Crea una promoción con servicios y alcances (por zona geográfica).

### 🔗 Contrato-Promociones

- `GET /api/contratoPromociones`  
  Muestra todas las promociones asignadas a contratos.

- `GET /api/contratoPromociones/contrato/{idContrato}`  
  Muestra las promociones asignadas a un contrato específico.

- `POST /api/contratoPromociones`  
  Asigna una promoción a un contrato. Solo se requiere el `idContrato` y `idPromocion`, la fecha se establece automáticamente desde el backend.

---

## 🗃️ Estructura de la Base de Datos (Resumen de relaciones)

- **Contrato ↔ Servicio**: Relación N:M (`Contrato_Servicios`)
- **Contrato ↔ Promoción**: Relación N:M (`Contrato_Promociones`)
- **Promoción ↔ Servicio**: Relación N:M (`Promocion_Servicio`)
- **Promoción ↔ Alcance**: Relación 1:N para delimitar zonas geográficas (`Promocion_Alcances`)
- **Contrato ↔ Suscriptor**: Relación 1:N

---

## 🔄 Flujo para asignar promociones

1. El usuario selecciona un contrato desde el panel izquierdo.
2. El frontend filtra y muestra las promociones disponibles según los servicios del contrato.
3. Se selecciona una promoción.
4. Se ejecuta el `POST /api/contratoPromociones` enviando `{ idContrato, idPromocion }`.
5. El backend asigna la promoción al contrato y guarda la fecha actual como `fechaAplicacion`.

---

## 🧪 Pruebas

Puedes probar los endpoints desde:
- Postman (colección incluida si aplica).
- Swagger (`http://localhost:{puerto}/swagger`).

---

## 🛠️ Tecnologías

- .NET 8
- Entity Framework Core (Database First)
- AutoMapper
- Swagger para documentación interactiva
- SQL Server

---

## 📁 Ejemplo de Request (Asignar promoción a contrato)

```http
POST /api/contratoPromociones
Content-Type: application/json

{
  "idContrato": 1,
  "idPromocion": 3
}
