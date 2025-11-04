# PersonAPI - API REST con ASP.NET Core MVC

## 📋 Descripción del Proyecto

**PersonAPI** es una API REST desarrollada con ASP.NET Core 6.0 que implementa un sistema completo de gestión de personas, profesiones, estudios y teléfonos. Utiliza el patrón de arquitectura MVC con DAO (Data Access Object) implementados como Repositorios.

### Stack Tecnológico
- **Framework:** ASP.NET Core 6.0 (MVC)
- **Base de Datos:** SQL Server 2019 Express
- **ORM:** Entity Framework Core 6.0
- **API Documentation:** Swagger 3.0
- **Patrones:** MVC + DAO (Repositories)
- **Inyección de Dependencias:** Integrada en ASP.NET Core

## 🗄️ Modelo de Datos

### Entidades:
- **Profesion:** Almacena profesiones con descripción
- **Persona:** Datos de personas (cédula, nombre, apellido, género, edad)
- **Estudios:** Relación muchos-a-muchos entre Profesion y Persona
- **Telefono:** Números telefónicos asociados a Personas

## ⚙️ Configuración del Ambiente

### 1. Clonar el Repositorio

```
git clone https://github.com/osoriofy/personapi-dotnet.git
cd personapi-dotnet/personapi-dotnet
```

### 2. Crear la Base de Datos

#### Opción A: Con SQL Server Management Studio (GUI)
1. Abre **SQL Server Management Studio**
2. Conecta al servidor: `localhost\SQLEXPRESS`
3. Abre una nueva consulta (Ctrl + N)
4. Pegar el script DDL_BD que esta en la carpeta de la entrega
5. Presiona F5 para ejecutar
6. (Opcional) Pegar el script DDL_inserciones para probar las tablas

### 3. Restaurar Paquetes NuGet

```
dotnet restore
```

O en Visual Studio:
- Herramientas → Administrador de paquetes NuGet → Restaurar paquetes
---

## 🔧 Compilación

### Desde Visual Studio Community:
1. Abre el archivo `personapi-dotnet.sln`
2. Presiona `Ctrl + Shift + B` para compilar

### Desde Línea de Comandos:
```
dotnet build
```

## 🚀 Despliegue y Ejecución

### Opción 1: Desde Visual Studio Community
1. Presiona **F5** para ejecutar con depuración
2. O presiona **Ctrl + F5** para ejecutar sin depuración
3. La aplicación se abrirá en el navegador

### Opción 2: Desde Línea de Comandos
```
dotnet run
```

La aplicación estará disponible en:
- **Swagger UI:** http://localhost:5204/swagger
---

## 📚 Uso de la API

### Acceder a la Documentación Interactiva
Ve a: **http://localhost:5204/swagger**

Aquí podrás ver y probar todos los endpoints disponibles.

### Endpoints Principales

#### Profesiones
```
GET    /api/profesiones              → Obtener todas
GET    /api/profesiones/{id}         → Obtener por ID
GET    /api/profesiones/buscar/{nom} → Buscar por nombre
POST   /api/profesiones              → Crear
PUT    /api/profesiones/{id}         → Actualizar
DELETE /api/profesiones/{id}         → Eliminar
```

#### Personas
```
GET    /api/personas                 → Obtener todas
GET    /api/personas/{cc}            → Obtener por cédula
GET    /api/personas/genero/{gen}    → Obtener por género
GET    /api/personas/edad/{edad}     → Obtener por edad
POST   /api/personas                 → Crear
PUT    /api/personas/{cc}            → Actualizar
DELETE /api/personas/{cc}            → Eliminar
```

#### Teléfonos
```
GET    /api/telefonos                → Obtener todos
GET    /api/telefonos/{numero}       → Obtener por número
GET    /api/telefonos/persona/{id}   → Obtener por dueño
POST   /api/telefonos                → Crear
PUT    /api/telefonos/{numero}       → Actualizar
DELETE /api/telefonos/{numero}       → Eliminar
```

#### Estudios
```
GET    /api/estudios                 → Obtener todos
GET    /api/estudios/persona/{cc}    → Obtener por persona
GET    /api/estudios/profesion/{id}  → Obtener por profesión
POST   /api/estudios                 → Crear
PUT    /api/estudios                 → Actualizar
DELETE /api/estudios/{id}/{cc}       → Eliminar
```

## 🔐 Configuración de Conexión

El archivo `appsettings.json` contiene la cadena de conexión:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true;"
  }
}
```
## 🧪 Pruebas

### Probar con Swagger UI
1. Ejecuta la aplicación
2. Ve a: http://localhost:5204/swagger
3. Haz clic en cualquier endpoint
4. Haz clic en "Try it out"
5. Introduce datos y presiona "Execute"
