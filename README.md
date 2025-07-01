
# 🧩 Monorepo - Cálculo de deuda - Equipo 3

Este repositorio contiene el sistema completo dividido en tres módulos principales:

- 🔌 [Backend (API)](./apps/API-promo-configurator/API.md)
- 🎨 [Frontend](./apps/promo-configurator/frontend.md)

Puedes revisar como levantar el proyecto con docker desde:

## Creado por

Con amor por:

- Jesus Isaac Estrada Ramirez
- Eduardo Antonio Sandoval Adame
- Luis Humberto Rivera Chang
- Reymundo Fernando Figueroa Romo

---

## 🚀 Cómo iniciar

### Requisitos previos

Asegúrate de tener instalados en tu entorno:

- [Node.js](https://nodejs.org/) (v18 recomendado)
- [Angular CLI](https://angular.io/cli)
- [.NET SDK](https://dotnet.microsoft.com/) (v9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

### 1. Clonar el repositorio

```bash
git clone https://github.com/reymundofigueroa/Hackaton-Mega-Eq3
cd Hackaton-Mega-Eq3
```

---

### 2. Ejecutar el comando 'npm install' dentro de la siguiente ruta del repositorio

1.

```bash
cd apps/promo-configurator


```

---

### 4. Para ejecutar los proyectos ejecuta los siguientes comandos en las rutas especificadas

```bash
cd apps/API-promo-configurator
dotnet run

```

```bash
cd apps/promo-configurator
ng serve

```
---

### 5. Para preparar la base de datos

1. Asegúrate de tener SQL Server corriendo.

2. Ejecuta los scripts de las carpeta /T-SQL-promo-configurator primero el archivo: create-ConfiguradorPromociones.sql y después: insertPromoData.sql

---

### Ultima actualización el 30/06/2025
