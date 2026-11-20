# Sistema web de control de personal

**Grupo 1 · Tema 5: Departamento – Empleado**

Desarrollo Web para la Integración de Tecnologías · Universidad de las Fuerzas Armadas ESPE · Departamento de Ciencias de la Computación

---

## 1. Tema y problema asignado

Una empresa necesita controlar su personal. Hoy registra la información en hojas de cálculo que cualquier persona puede abrir y modificar: se han borrado registros por error, nadie sabe quién cambió qué y los datos de una tabla no coinciden con los de la otra.

La aplicación web con base de datos resuelve el problema así:

- Los datos se **validan** antes de guardarse (mensajes en español).
- Cada empleado **pertenece a un departamento**, elegido por su nombre desde una lista desplegable.
- Solo las **personas registradas** pueden modificar información.
- El rol de **gestión (Gerente)** crea, edita y elimina; el rol de **consulta (Colaborador)** solo ve.

## 2. Tecnologías

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core con SQL Server (SQL Server Express)
- ASP.NET Core Identity (registro, inicio y cierre de sesión, roles)
- GitHub para el trabajo colaborativo

## 3. Entidades

| Entidad | Rol | Columnas |
|---|---|---|
| **Departamento** | Principal | `Id` (int), `Nombre`, `Ubicacion?` |
| **Empleado** | Dependiente | `Id` (int), `Nombre`, `Cargo`, `Salario` (decimal), `DepartamentoId` (int) |

- Relación **uno a muchos**: un Departamento tiene muchos Empleados.
- La clave foránea `DepartamentoId` usa `ON DELETE RESTRICT`: no se puede eliminar un departamento que tenga empleados asignados.
- Las columnas de texto son obligatorias, salvo `Ubicacion?`, que es opcional.
- Además, la base de datos incluye las tablas de Identity (`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, entre otras).

## 4. Roles y permisos

| Acción | Visitante sin sesión | Colaborador (consulta) | Gerente (gestión) |
|---|---|---|---|
| Ver lista y detalle (Departamentos y Empleados) | Sí | Sí | Sí |
| Crear, editar y eliminar | No (redirige al inicio de sesión) | No (Acceso denegado) | Sí |
| Enlaces Crear, Editar y Eliminar | Ocultos | Ocultos | Visibles |

- Un usuario registrado desde la página queda **sin rol** y tampoco puede modificar información.
- El saludo del menú muestra el correo junto al rol del usuario.

## 5. Integrantes, turnos y aporte

El orden de los turnos es el orden de la tabla. Solo un integrante a la vez genera una migración, y el siguiente hace **Git > Extraer** antes de crear la suya.

| Turno | Integrante | Fase | Aporte |
|---|---|---|---|
| 1 | Shaffik Samir Samandé Mujica | Fase 1 y cierre | Proyecto y repositorio, entidad Empleado con CRUD, cadena de conexión, migración `InitialCreate`; enlaces ocultos según el rol y rol en el saludo del menú |
| 2 | John Steven Guanotoa Diaz | Fase 2 | Validaciones con DataAnnotations en español en Empleado y migración `ValidacionesEmpleado` |
| 3 | Dylan Joel Lopez Morales | Fase 3 | Entidad Departamento con CRUD, relación uno a muchos, lista desplegable y nombre del departamento en la lista y el detalle; migraciones `AgregarDepartamento` y `RelacionDepartamentoEmpleado` |
| 4 | David Ivan Granada Pachacama | Fase 4 | `ApplicationUser`, Identity con scaffolding, migración `AgregarIdentity`, `_LoginPartial` en el menú, `[Authorize]` y `[AllowAnonymous]` en EmpleadosController |
| 5 | Ronny Adrian Sanchez Garcia | Fase 5 | Roles Gerente y Colaborador, usuarios de prueba, restricción por rol en ambos controladores y credenciales en el README |

## 6. Usuarios de prueba

Se crean automáticamente al iniciar la aplicación (clase `IdentitySeeder`).

| Rol | Correo | Contraseña |
|---|---|---|
| Gerente (gestión) | gerente@empresa.com | Gerente123* |
| Colaborador (consulta) | colaborador@empresa.com | Colaborador123* |

## 7. Cómo ejecutar el proyecto

**Requisitos:** Visual Studio con la carga de trabajo de ASP.NET, .NET 10 SDK, SQL Server Express (instancia `localhost\SQLEXPRESS`) y SSMS.

1. **Clonar** el repositorio: en Visual Studio, *Git > Clonar repositorio* y pegar la URL.
2. **Ajustar la cadena de conexión** en `appsettings.json` si su instancia no es `localhost\SQLEXPRESS`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EmpresaPersonalDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   ```
3. **Aplicar las migraciones:** en *Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes*, ejecutar:
   ```powershell
   Update-Database
   ```
4. **Ejecutar** el proyecto (F5). Al iniciar se crean los roles y los dos usuarios de prueba.
5. Iniciar sesión con alguno de los usuarios de la sección 6.

**Problemas frecuentes**

| Problema | Solución |
|---|---|
| No conecta a la base de datos | Revisar que el servicio *SQL Server (SQLEXPRESS)* esté en ejecución y la cadena de conexión |
| `Invalid object name 'AspNetUsers'` | Falta ejecutar `Update-Database` |
| La migración de la relación falla | Borrar empleados antiguos de la base local: `DELETE FROM Empleados;` y repetir `Update-Database` |
| El rol no tiene efecto | Cerrar sesión y volver a iniciarla |
| Las páginas de login no muestran el menú | Revisar `Areas/Identity/Pages/_ViewStart.cshtml` y `app.MapRazorPages()` |

## 8. Migraciones

La carpeta `Migrations` contiene, en este orden:

| N.º | Migración | Contenido |
|---|---|---|
| 1 | `InitialCreate` | Tabla `Empleados` inicial |
| 2 | `ValidacionesEmpleado` | Longitudes máximas de Nombre y Cargo, precisión de Salario |
| 3 | `AgregarDepartamento` | Tabla `Departamentos` |
| 4 | `RelacionDepartamentoEmpleado` | Columna `DepartamentoId`, índice y clave foránea |
| 5 | `AgregarIdentity` | Tablas de Identity (`AspNet...`) |

## 9. Estructura del proyecto

```
GestionPersonal/
├── Areas/Identity/Pages/...      Páginas de Identity (registro, login, logout, acceso denegado)
├── Controllers/
│   ├── DepartamentosController.cs
│   ├── EmpleadosController.cs
│   └── HomeController.cs
├── Data/
│   ├── ApplicationDbContext.cs   DbContext (hereda de IdentityDbContext<ApplicationUser>)
│   ├── ApplicationUser.cs
│   ├── IdentitySeeder.cs         Crea roles y usuarios de prueba al iniciar
│   └── RolesApp.cs               Constantes de los roles
├── Migrations/
├── Models/
│   ├── Departamento.cs
│   └── Empleado.cs
├── Views/
│   ├── Departamentos/
│   ├── Empleados/
│   └── Shared/_LoginPartial.cshtml
├── appsettings.json
└── Program.cs
```

## 10. Forma de trabajo en GitHub

Ciclo de cada sesión de trabajo:

1. Extraer los cambios del repositorio (*Git > Extraer*) antes de modificar cualquier archivo.
2. Si llegaron migraciones nuevas, ejecutar `Update-Database`.
3. Hacer el cambio y compilar la solución sin errores.
4. Probar el cambio en el navegador.
5. Hacer *commit* con un mensaje descriptivo y **Confirmar todo e insertar**.

Condiciones acordadas:

- Cada *commit* contiene un cambio que compila.
- Los mensajes empiezan con un verbo y describen qué se hizo (por ejemplo: `Agregar validaciones Required y StringLength a Empleado`).
- Cada integrante registra al menos cuatro *commits* propios, en fechas distintas.
- No se suben las carpetas `bin` ni `obj`, ni el proyecto comprimido.

## 11. Enlaces

- **Repositorio de GitHub:** [https://github.com/samirsamande/GestionPersonal]
- **Video de demostración:** [https://youtu.be/nWxJhUGwsJM]
