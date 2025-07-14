## Proyecto
En este proyecto se implementa una API RESTFUL para la gestion de especialidades medicas, usando una arquitectura Clean con .net y entity framework core. En principio es una solucion a las operaciones CRUD de especialidades.

## Configuracion para levantar API localmente:
1. Clonar repositorio
2. Restaurar depencencias con "dotnet restore"
3. Configuracion de base de datos: modificar appsettings.json (Se utilizo una instancia local de BD ya que tuve problemas de permisos para poder crear una bd en el servidor proporcionado)
4. Migraciones con "Add-Migration InitialCreate"
5. Ejecutar con "dotnet run"
6. Acceder a swagger UI

## Arquitectura y decisiones
Se utiliza Clean architecture para la separacion de responsabilidades y para un mejor mantenimiento.
Se dividen en las siguientes capas:
- Core: Incluye entidades de dominio y reglas de negocio. Capa mas interna
- Application: Incluye la logica de negocio, interfaces de servicio, DTOs y la utilizacion de AutoMapper.
- Infrastructure: Incluye persistencia de datos.
- Web: Incluye controladores API y la inyeccion de dependencias.

## Ejemplos de request (Utilizando postman)
- Se guardo un archivo .json en el directorio Postman/ de la solucion, el cual fue exportado de postman con las rutas a los endpoints del CRUD.
