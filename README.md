# 🚀 PruebaEmi 

Proyecto realizado como prueba técnica para el ingreso a a EMI.

[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)

---

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías](#️-tecnologías)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#️-configuración)
- [Ejecución](#-ejecución)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [API Endpoints](#-api-endpoints)
- [Autenticación](#-autenticación)
- [Arquitectura](#️-arquitectura)
- [Patrones de Diseño](#-patrones-de-diseño)
- [Testing](#-testing)
- [Troubleshooting](#-troubleshooting)

---

## ✨ Características

- ✅ **CRUD completo** de empleados con validaciones
- ✅ **Autenticación JWT** con tokens Bearer
- ✅ **Autorización basada en roles** (Admin, User)
- ✅ **Historial de posiciones** de un empleado
- ✅ **Cálculo automático de bonos** según posición (10% Regular, 20% Manager)
- ✅ **Relaciones many-to-many** entre empleados y proyectos
- ✅ **Middleware personalizado** para logging de requests
- ✅ **Swagger UI** integrado con autenticación JWT
- ✅ **Entity Framework Core** con Code-First migrations
- ✅ **Clean Architecture** con separación de capas
- ✅ **SOLID Principles** aplicados en toda la solución


---

## 🛠️ Tecnologías

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM
- **SQL Server LocalDB** - Base de datos
- **JWT Bearer Authentication** - Seguridad
- **BCrypt.Net-Next** - Hashing de contraseñas
- **Swashbuckle (Swagger)** - Documentación de API

### Arquitectura
- **Clean Architecture** (Domain, Application, Infrastructure, Presentation)
- **Repository Pattern**
- **Dependency Injection**
- **Template Method Pattern**
- **Strategy Pattern**
- **Middleware Pattern**

---

## 📦 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- ✅ [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (versión 8.0 o superior)
- ✅ [Visual Studio 2022/2026](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)
- ✅ [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (incluido con Visual Studio)
- ✅ [Git](https://git-scm.com/) para clonar el repositorio

### Verificar Instalación

```bash
# Verificar .NET SDK
dotnet --version

# Verificar SQL Server LocalDB
sqllocaldb info

# Debería mostrar: MSSQLLocalDB
```

---

## 📥 Instalación

### 1. Clonar el Repositorio

```bash
git clone https://github.com/andresftm/pruebaemi.git
cd pruebaemi
```

### 2. Restaurar Dependencias

```bash
dotnet restore
```

### 3. Configurar Connection String

Edita `PruebaEmi\appsettings.json` y verifica/ajusta la conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PruebaEmiDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 4. Aplicar Migraciones

```bash
# Navegar al proyecto principal
cd PruebaEmi

# Aplicar migraciones
dotnet ef database update

# Si ocurre un error, ejecutar desde la raíz:
dotnet ef database update --project PruebaEmi.Infrastructure --startup-project PruebaEmi
```

---

## ⚙️ Configuración

### Configuración de JWT

El archivo `appsettings.json` incluye la configuración JWT:

```json
{
  "JwtSettings": {
    "SecretKey": "MySecretKey12345MySecretKey12345MySecretKey12345",
    "Issuer": "https://localhost:7257",
    "Audience": "https://localhost:7257",
    "ExpirationInHours": 24
  }
}
```

> ⚠️ **Importante**: Si fuesemos a llevar el proyecto a producción deberíamos de mover el `SecretKey` a **Azure Key Vault** o variables de entorno, lo deje así por ser una prueba técnica.

---

## 🚀 Ejecución

1. Abre la solución `PruebaEmi.sln` en Visual Studio
2. Selecciona el proyecto **PruebaEmi** como startup project
3. Presiona **F5** o haz clic en **Run**
4. Navega a: `https://localhost:7257/swagger`


## 📁 Estructura del Proyecto

```
PruebaEmi/
│
├── 📁 PruebaEmi/                          # Capa de Presentación (API)
│   ├── Controllers/
│   │   ├── EmployeeController.cs          # CRUD de empleados
│   │   └── AuthController.cs              # Login/Register
│   ├── Middleware/
│   │   └── RequestLoggingMiddleware.cs    # Logging de requests
│   ├── Program.cs                         # Configuración y DI
│   └── appsettings.json                   # Configuración
│
├── 📁 PruebaEmi.Domain/                   # Capa de Dominio (Core)
│   ├── Entities/
│   │   ├── employee.cs                    # Entidad Employee
│   │   ├── departments.cs                 # Entidad Department
│   │   ├── projects.cs                    # Entidad Project
│   │   ├── PositionHistory.cs             # Tracking de posiciones
│   │   ├── EmployeeProject.cs             # Tabla junction
│   │   └── User.cs                        # Usuario para autenticación
│   ├── Interfaces/
│   │   ├── IRepository.cs                 # Interfaz genérica
│   │   ├── IEmployeeRepository.cs         # Interfaz específica
│   │   ├── IService.cs                    # Interfaz de servicio
│   │   ├── IEmployeeService.cs            # Servicio de empleados
│   │   └── IAuthService.cs                # Servicio de autenticación
│   └── DTOs/
│       ├── LoginRequest.cs
│       ├── RegisterRequest.cs
│       └── AuthResponse.cs
│
├── 📁 PruebaEmi.Services/                 # Capa de Aplicación
│   ├── Service.cs                         # Servicio genérico (base)
│   ├── EmployeeService.cs                 # Lógica de negocio
│   └── AuthService.cs                     # Autenticación JWT
│
├── 📁 PruebaEmi.Infrastructure/           # Capa de Infraestructura
│   ├── Data/
│   │   ├── ApplicationDbContext.cs        # DbContext
│   │   └── Configurations/                # Fluent API
│   │       ├── EmployeeConfiguration.cs
│   │       ├── DepartmentConfiguration.cs
│   │       ├── ProjectConfiguration.cs
│   │       ├── PositionHistoryConfiguration.cs
│   │       ├── EmployeeProjectConfiguration.cs
│   │       └── UserConfiguration.cs
│   ├── Repositories/
│   │   ├── Repository.cs                  # Repositorio genérico
│   │   └── EmployeeRepository.cs          # Repositorio específico
│   └── Migrations/                        # Migraciones EF Core
│
└── README.md                              # Este archivo
```

---

## 🌐 API Endpoints

### Autenticación

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| POST | `/api/Auth/register` | Registrar nuevo usuario | No |
| POST | `/api/Auth/login` | Iniciar sesión y obtener JWT token | No |

**Ejemplo - Register:**
```json
POST /api/Auth/register
{
  "username": "admin",
  "email": "andrestabares@gmail.com",
  "password": "Admin123!",
  "role": "Admin"
}
```

**Ejemplo - Login:**
```json
POST /api/Auth/login
{
  "username": "admin",
  "password": "Admin123!"
}

// Respuesta:
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "admin",
  "email": "admin@example.com",
  "role": "Admin",
  "expiresAt": "2024-03-21T10:30:00Z"
}
```

---

### Empleados

| Método | Endpoint | Descripción | Roles |
|--------|----------|-------------|-------|
| GET | `/api/Employee` | Listar todos los empleados | Admin, User |
| GET | `/api/Employee/{id}` | Obtener empleado por ID | Admin, User |
| GET | `/api/Employee/{id}/bonus` | Calcular bono anual | Admin, User |
| GET | `/api/Employee/department/{id}/with-projects` | Empleados de un departamento con proyectos | Admin, User |
| POST | `/api/Employee` | Crear nuevo empleado | Admin |
| PUT | `/api/Employee/{id}` | Actualizar empleado | Admin |
| DELETE | `/api/Employee/{id}` | Eliminar empleado | Admin |

**Se deben de ejecutar este script que llana información en tablas para poder probar los endpoints de empleados:**
```sql

INSERT INTO dbo.Departments (id, name) VALUES (1, 'contabilidad');
INSERT INTO dbo.Departments (id, name) VALUES (2, 'ti');
INSERT INTO dbo.Departments (id, name) VALUES (3, 'cobranza');

INSERT INTO PositionHistories (EmployeeId, position, startdate, enddate)
VALUES (6, 'manager', '2026-02-01', NULL);

INSERT INTO PositionHistories (EmployeeId, position, startdate, enddate)
VALUES (7, 'employee', '2026-02-01', NULL);

UPDATE dbo.PositionHistories
SET EndDate = '2026-03-02'
WHERE id = 5;

INSERT INTO PositionHistories (EmployeeId, position, startdate, enddate)
VALUES (6, 'managers', '2026-03-02', NULL);

INSERT INTO dbo.Projects (id, name) VALUES (1, 'Educativo');
INSERT INTO dbo.Projects (id, name) VALUES (2, 'Recursivo');
INSERT INTO dbo.Projects (id, name) VALUES (3, 'Novato');

```

** Agregar empleados a proyectos

**Ejemplo - Crear Empleado:**
```json
POST /api/Employee
Authorization: Bearer {token}

{
  "name": "Juan Pérez",
  "currentPosition": null,
  "salary": 50000,
  "departmentId": 1,
}
```


---

## 🔐 Autenticación

### Flujo de Autenticación

```mermaid
sequenceDiagram
    participant Client
    participant API
    participant AuthService
    participant Database

    Client->>API: POST /api/Auth/login
    API->>AuthService: LoginAsync(credentials)
    AuthService->>Database: Buscar usuario
    Database-->>AuthService: Usuario encontrado
    AuthService->>AuthService: Verificar BCrypt
    AuthService->>AuthService: Generar JWT Token
    AuthService-->>API: AuthResponse + Token
    API-->>Client: 200 OK + Token
    
    Client->>API: GET /api/Employee (Bearer Token)
    API->>API: Validar JWT
    API->>API: Verificar Roles
    API-->>Client: 200 OK + Datos
```

### Uso del Token en Swagger

1. Haz clic en el botón **"Authorize"** 🔓 en Swagger UI
2. Ingresa el token en el formato: `Bearer {tu-token-jwt}`
3. Haz clic en **"Authorize"**
4. Ahora puedes probar todos los endpoints protegidos

### Uso del Token con cURL

```bash
curl -X GET "https://localhost:7257/api/Employee" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIs..."
```

---

## 🏗️ Arquitectura

### Clean Architecture Implementada

```
┌─────────────────────────────────────────────┐
│          Presentation Layer (API)           │
│  Controllers, Middleware, Program.cs        │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│       Application Layer (Services)          │
│   Business Logic, Validations, DTOs         │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│          Domain Layer (Core)                │
│   Entities, Interfaces, Domain Logic        │
└─────────────────▲───────────────────────────┘
                  │
┌─────────────────┴───────────────────────────┐
│      Infrastructure Layer                   │
│   EF Core, Repositories, External Services  │
└─────────────────────────────────────────────┘
```

### Principios SOLID Aplicados

| Principio | Implementación |
|-----------|----------------|
| **S**ingle Responsibility | Cada clase tiene una única responsabilidad |
| **O**pen/Closed | Uso de `virtual`/`override` para extensión |
| **L**iskov Substitution | Interfaces correctamente implementadas |
| **I**nterface Segregation | Interfaces específicas por responsabilidad |
| **D**ependency Inversion | Inyección de dependencias en toda la app |

---

## 🎨 Patrones de Diseño

### 1. Repository Pattern
```csharp
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
}
```

### 2. Dependency Injection
```csharp
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
```

### 3. Template Method Pattern
```csharp
public class Service<T> : IService<T>
{
    public virtual async Task<T> AddAsync(T entity) { ... }
}

public class EmployeeService : Service<employee>
{
    public override async Task<employee> AddAsync(employee entity)
    {
        // Validaciones específicas
        return await base.AddAsync(entity);
    }
}
```

### 4. Strategy Pattern
```csharp
public decimal CalculateYearlyBonus()
{
    if (position.Contains("manager"))
        return Salary * 0.20m; // Estrategia Manager
    else
        return Salary * 0.10m; // Estrategia Regular
}
```

### 5. Middleware/Decorator Pattern
```csharp
public class RequestLoggingMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Log request
        await _next(context); // Delegar al siguiente middleware
        // Log response
    }
}
```

---

### Error: "401 Unauthorized" en Swagger

**Solución:**
1. Registra un usuario usando `/api/Auth/register`
2. Haz login con `/api/Auth/login` para obtener el token
3. Copia el token de la respuesta
4. Haz clic en **"Authorize"** en Swagger
5. Ingresa: `Bearer {tu-token}` (con espacio después de "Bearer")
6. Haz clic en **"Authorize"**

---

## 📚 Recursos Adicionales

- [Documentación de .NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Security](https://learn.microsoft.com/en-us/aspnet/core/security/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://www.digitalocean.com/community/conceptual-articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)

---

## 👤 Autor

**Andrés Tabares**  
GitHub: [@andresftm](https://github.com/andresftm)

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver archivo `LICENSE` para más detalles.

---

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

</div>
