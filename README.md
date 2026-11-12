# ProyectoCurso - AssetManager (ASP.NET Core MVC & Clean Architecture)

Este proyecto sirve como plantilla de referencia y material de formacion para el equipo de desarrollo de **AssetManager**. Demuestra como estructurar una solucion empresarial con **Clean Architecture**, manteniendo las reglas de negocio aisladas y ofreciendo una experiencia de usuario fluida mediante **ASP.NET Core MVC con modales y peticiones asincronas (AJAX)** en una sola vista por entidad.

---

## 1. Arquitectura de la Solucion

La solucion esta dividida en capas bajo principios de Clean Architecture:

```
ProyectoCursoAPI.slnx
|-- ProyectoCursoAPI.Domain/             -> Entidades del dominio, reglas invariantes y excepciones de negocio
|-- ProyectoCursoAPI.Application/        -> Casos de uso, interfaces, DTOs y validadores (FluentValidation)
|-- ProyectoCursoAPI.Infrastructure/     -> Entity Framework Core, repositorios, Identity y base de datos
\-- ProyectoCursoAPI.Web/                -> Presentacion ASP.NET Core MVC (Controllers, Vistas, ViewModels, JS)
```

### Reglas de Dependencia
1. **Domain**: No depende de ningun paquete externo ni capa del proyecto.
2. **Application**: Depende unicamente de `Domain` y librerias transversales como `FluentValidation`.
3. **Infrastructure**: Depende de `Application` y `Domain`. Implementa persistencia, repositorios y servicios externos.
4. **Web**: Depende de `Application` e `Infrastructure` para el registro del contenedor de dependencias (`DependencyInjection`).

---

## 2. Decisiones de Diseno y Patrones Utilizados

### A. Modularizacion por Features
En `Application/Features/`, cada entidad de negocio (`Categorias`, `Productos`, `Proveedores`, `Auth`) agrupa de forma cohesionada:
- **DTOs**: Objetos de transferencia de datos de entrada y salida.
- **Validators**: Reglas de validacion con `FluentValidation` en clases independientes.
- **UseCases**: Casos de uso con responsabilidad unica (Single Responsibility Principle).
- **Services**: Servicios fachada que orquestan los casos de uso para consumo de los controladores.

### B. Manejo de Errores con Excepciones de Dominio
En lugar de envoltorios genericos como `ServiceResponse`, el sistema confia en el lanzamiento de excepciones semanticas:
- `ValidationException`: Cuando una regla de entrada falla.
- `NotFoundException`: Cuando un recurso no existe.
- `DomainException`: Cuando se viola una regla interna de la entidad.

Los controladores capturan estas excepciones y las transforman en errores de formulario (`ModelState`) o respuestas estructuradas.

### C. Autenticacion con Cookies e Identity
Se utiliza `SignInManager<IdentityUser>` y el esquema de autenticacion por cookies estandar de ASP.NET Core:
- Sesion persistente mediante cookie cifrada.
- Proteccion contra CSRF en todos los formularios mediante `AutoValidateAntiforgeryTokenAttribute`.
- Decoradores `[Authorize]` y `[AllowAnonymous]` para control de acceso por endpoint.

### D. Patron de Vista Unica por Entidad (Single-Page Experience en MVC)
Toda la administracion de una entidad ocurre en su vista principal `Index.cshtml`:
- **Listado y Paginado**: Se cargan via parcial `_TablePartial.cshtml`. Los cambios de pagina se realizan asincronamente sin recargar el navegador.
- **Crear y Editar**: Comparten un modal dinamico de Bootstrap 5 (`_FormModal.cshtml`).
- **Validacion Asincrona**: Si el servidor detecta errores de validacion, retorna el HTML parcial con los mensajes inyectados en `ModelState`. Si la operacion es exitosa, retorna un JSON para cerrar el modal y refrescar la tabla.
- **Eliminacion Logica**: Modal de confirmacion reutilizable que realiza un POST seguro con token antiforgery.

---

## 3. Guia de JavaScript (`wwwroot/js/crud-helper.js`)

El archivo `crud-helper.js` encapsula las funciones comunes para que cualquier vista pueda implementar el patron en pocas lineas:

| Metodo | Proposito |
|---|---|
| `CrudHelper.openModal(url)` | Realiza un GET a la accion del controlador y carga el formulario dentro del modal generico. |
| `CrudHelper.submitModalForm(form, callback)` | Intercepta el submit, envia los datos con AJAX, valida respuestas y ejecuta el callback de refresco. |
| `CrudHelper.loadTable(selector, url, page)` | Descarga la vista parcial de la tabla y actualiza el contenedor indicado. |
| `CrudHelper.confirmDelete(url, id, token, msg, callback)` | Muestra el modal de confirmacion y ejecuta la eliminacion por POST. |
| `CrudHelper.showAlert(mensaje, tipo)` | Despliega notificaciones flotantes temporales (success, danger, info). |

---

## 4. Configuracion y Puesta en Marcha

1. Asegurar la cadena de conexion en `appsettings.json` o en `UserSecrets`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=AssetManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```
2. Aplicar las migraciones a la base de datos:
   ```bash
   dotnet ef database update --project ProyectoCursoAPI.Infrastructure --startup-project ProyectoCursoAPI.Web
   ```
3. Ejecutar la aplicacion:
   ```bash
   dotnet run --project ProyectoCursoAPI.Web
   ```
4. Navegar a `https://localhost:xxxx` e iniciar sesion o registrar un nuevo usuario para comenzar a administrar el catalogo.
