# Sistema web de control de personal — Grupo 1 (Tema 5)

**Asignatura:** Desarrollo Web para la Integración de Tecnologías · ESPE

## Problema asignado
Una empresa necesita controlar su personal. Hoy registra la información en hojas de cálculo que cualquiera puede abrir y modificar: se borran registros por error, nadie sabe quién cambió qué y los datos de una tabla no coinciden con los de la otra. La aplicación valida los datos antes de guardarlos, asocia cada empleado a un departamento elegido por nombre, permite modificar información solo a personas registradas y separa un rol de gestión (Gerente) de uno de consulta (Colaborador).

## Entidades
- **Departamento** (principal): Id, Nombre, Ubicacion?
- **Empleado** (dependiente): Id, Nombre, Cargo, Salario (decimal), DepartamentoId

## Integrantes, turnos y fases
| Turno | Integrante | Fase |
|---|---|---|
| 1 | Shaffik Samir Samandé Mujica | Fase 1: proyecto y entidad dependiente · cierre del componente autónomo |
| 2 | John Steven Guanotoa Diaz | Fase 2: validaciones |
| 3 | Dylan Joel Lopez Morales | Fase 3: relación uno a muchos |
| 4 | David Ivan Granada Pachacama | Fase 4: Identity |
| 5 | Ronny Adrian Sanchez Garcia | Fase 5: roles y restricción por rol |

## Usuarios de prueba
(Se completará en la Fase 5.)

## Cómo ejecutar el proyecto
1. Clonar el repositorio.
2. Ajustar la cadena de conexión `DefaultConnection` en `appsettings.json`.
3. Ejecutar `Update-Database` en la Consola del Administrador de paquetes.
