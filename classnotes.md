# Apuntes de Clase

## Uso consola powershell personalizado

1. Iniciar perfil: `New-Item -Path $PROFILE -Type File -Force`
1. Instalar editor: `winget install -e --id Neovim.Neovim`
1. Editar: `nvim $PROFILE`
1. Instalar `winget install JanDeDobbeleer.OhMyPosh --source winget`
1. Instalar fuente: `oh-my-posh font install`
1. Abrir shell : `oh-my-posh get shell` (Consultar web)

## Iniciar git

1. Instalar `winget install "Git.Git" --source winget`
1. Iniciar `git init`
1. Configurar `git config --global user.name "nombreusuario"`
1. Configurar `git config --global user.email "user@mail"`
1. Configurar repo creado en github `git remote add REPONAME REPOURL`
1. Configurar flujo por defecto `git push --set-upstream TestNet master`
1. Añadir codigo `git add . | git commit -m "Tema:Cambio" | git push`

## Inicio proyecto

1. Comprobar .net instalado `dotnet --version`
1. Crear proyecto de consola `dotnet new console --name HelloWorld`
1. Construir `dotnet build`
1. Sacar release `dotnet build -c Release` en .\bin\Release

## BBDD

- SQLServer Dev (Carpeta TestNet)

1. En VisualStudio2022 instalar complemento MSSQLocalDB
1. En explorador de objetos SQL crear base de datos y tabla. Actualizar para ejectuar cambios en t-sql
1. En powershell: `winget install Microsoft.sqlcmd`
1. Consultar estado bbdd pruebas: `sqllocaldb info MSSQLLocalDB`
1. Iniciar bbdd sin VS2022: `sqllocaldb start MSSQLLocalDB` y `sqlcmd -S "np:\\.\pipe\LOCALDB#1234\tsql\query" -Q "SELECT * FROM sys.databases;" -o 'dBases.txt'`
1. Ver cheatsheet comandos sqlcmd
1. En proyecto: `dotnet add package Microsoft.Data.SqlClient`

- Entity Framework (Carpeta TestEF)

1. `dotnet add package Microsoft.EntityFrameworkCore --version x.0.0` - Con x la version de csproj`<TargetFramework>net9.0</TargetFramework>` (No .Core) Antiguo .- Hay que instalar el proveedor de EntityFramework para el tipo de bbdd que se vaya a usar (mssql,postgresql,etc)
1. Las clases que tratan conexiones sql deben implementar IDisposable para gestionar la conexión temporal. using en las variables que guardan SqlConnection implica que llama con.Dispose para eliminar recursos externos.
`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.0`
1. Entidas son clases POCO (Como las POJO) puras sin dependencias que usaremos para el modelo de negocio
1. `public class BlogContext : DbContext` Se debe heredar de DbContext que si depende ya de EntityFramework. Lo usamos con el patrón builder para el constructor.
1. Usamos por ejemplo una base de datos creada en un contenedor docker con docker-compose up.
1. Revisar postgresql esta levantado:`docker logs <nombre-del-contenedor-adminer>` y `docker exec -it testnet-pgsql-1 psql -U demo -d demo`  (Instalar wsl --install y docker desktop con sus reinicios si no estaba instalado) y `docker compose down -v` para borrar el volumen de data
1. Añadimos al blog que el constructor inicialice la lista de post con un tipo que implemente la interfaz IColletion como es el caso de List.
1. Ya podemos usar el Seeder.cs
1. Cuando guardamos una consulta en una variable tenemos que indicar si vamos a querer acceder a sus dependecias con include: `var blogs = ctx.Blogs.Include(b => b.Posts).Where(b => b.Posts.Any(p => p.Id > 5))` en este caso necesario para el Console.Writeline siguiente. No le hace falta para el where.
1. Se pueden usar LINQ para las consultas y/o usar blog.AsEnumerable(). por ejemplo para usar LINQ en memoria. ¡Cuidado con lo que traemos a memoria!
1. Se puede usar IEnumerable encubriendo .AsEnumerable
1. Para no petar la memoria usar IQueryable

## ASP.NET MVC

1. Es muy diferente a webForm. Aplicación web de ASP.NET Core (Modelo-Vista-Controlador)  mvc                                 [C#],F#     Web/MVC
1. Si usamos otros frameworks adicionales para generar html,js y css (babel, etc) el código iría en otra carpeta junto a mvc y wwwroot debería ponerse en .gitignore porque el resultado final tiene que compilarse.
1. Los metodos IActionResult de los controllers deben corresponderse con vistas en la carpeta de vistas con el mismo nombre
1. Añadimos la bd usando EntityFramework, creamos Data para el DbContext y los POCO aunque podríamos tener nuestra propia librería donde tengamos esto ya creado _(MIRAR)_
1. Añadimos el controlador de PostgreSQL
1. Creamos un servicio que hará las peticiones a la base de datos. Para inyectarlo tenemos que _añadirlo a las dependencias del controlador_ . Lo adecuado es definir una interfaz para el servicio, y al _registrar los servicios en Program.cs_ es cuando decidimos que implementación usamos.
1. Al usar el servicio desde el controlador no guardar síncronamente los datos cuando es una llamada async GetPendingTodos, puede provocar deadlocks.
1. En la vista hay que indicar el modelo que usa la vista. Los datos que se pasen estarán ahí en la variable que siempre se llama @Model. (Mirar los ViewModels)
1. Para gestionar el esquema y generar migraciones de BD instalamos las herramientas de EntityFrameworks. [Instalar](https://docs.microsoft.com/es-es/ef/core/cli/dotnet) se instala de forma global como algunas herramientas npm. (Hay que poner la version 9.0.0 también al instalar) Requiere instalar también : `dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0`. Después ya se puede usar `dotnet ef migrations add Initial`que automaticamente crea la carpeta Migrations. Con `dotnet ef database update` aplica directamente las migraciones en la base de datos. Si mas adelante se cambia el POCO de data tenemos que ejectuar `dotnet ef migrations add TodoImportantAdded`. Crea una tabla para llevar el control de las migraciones aplicadas. Metemos datos en la tabla directamente en localhost:8080 para poder verlos (para evitar el seed ahora por tiempo de clase, también se podría hacer con migrations).
1. Podemos ver en la consola el logger que configura ASP para ver las consultas SQL resultantes que hace EntityFramework
1. Duda a _(MIRAR)_: new en  `<a href="@Url.Action("MarkDone", new { id = task.Id })">Mark as done</a>`
1. `dotnet ef migrations script` para el pipeline de despliegue
1. Hot reload con `dotnet watch`
1. Hacer vista de todas las tareas completadas como ejercicio.

## Blazor Web Server

1. Blazor permite ejecutar C# en lado de cliente "sustituyendo" JS hasta que tengamos que interactuar con componentes JS.
1. Hay algunos que tienen wrappers para envolver las llamadas js.
1. `dotnet new search blazorserver` e instalar por nombre de paquete: `dotnet new install Toolbelt.AspNetCore.Blazor.Minimum.Templates`
1. Si no tenemos la ultima version de netx.0 indicamos al crear: `dotnet new blazorservermin --framework net9.0 --name TestWebBlazorServer`
1. Para usar microsoft visual studio necesitamos el fichero sln `dotnet new sln --name TestWebBlazorServer`
1. `dotnet sln .\TestWebBlazorServer.sln add .\TestWebBlazorServer\TestWebBlazorServer.csproj` y ya podemos hacer `start .\TestWebBlazorServer.sln` para abrir el proyecto
1. Blazor tiene un comportamiento parecido a React, siguiendo la logica de componentes jerarquicos que pasan el estado a otros componentes hijos y oyen eventos que publican los hijos para cambiar su estado.
1. Importar en el fichero _Imports.razor la carpeta que creemos para los componentes. Así podemos usar en App.razor o nuestro componente principal sin la ruta completa.
1. Cuando tenemos un parametro declarado en el componente, al usarlo desde otro lo que espera es una expresión en C# `<DemoComponent Value="expresionC#" />`. El padre le pasa al hijo parametros.
1. Los cambios en el hijo no se pasan al padre si no usamos evento con event callback en el hijo y un handler en el padre, esto provocará un refresco del padre, que visualmente se refrescaba sin el event callback y handler porque cambia el  hijo y se renderiza pero no actualizaba la variable en el padre. No se propagan eventos en el arbol de componentes. Hay que explicitarlo.
1. El servidor web que usa es Kestrel de Microsoft.AspNetCore.Server, carga blazor.server.js en cliente, mantiene abierta conexión con websockets al servidor, hace la comparación del Dom Virtual en servidor y manda cambios para Dom real.
1. En la version 2022 habia una página de ejemplo fetch que accede a un servicio, pero podíamos usar EntityFramework para usar nuestra bd.
1. El csproj para instalar paquetes se accede desde el explorador de soluciones, haciendo click en el nombre de la carpeta principal.
1. _(MIRAR)_ El elemento añadido pone id 0

## Blazor WASM

1. `dotnet new blazorwasm --name TestBlazorWasm`
1. `dotnet new sln --name BlazorWasm`
1. `dotnet sln .\BlazorWasm.sln add .\TestBlazorWasm\TestBlazorWasm.csproj`
1. `start .\BlazorWasm.sln`

Diferencias:

1. Todo el codigo se ejecuta en cliente, no en el servidor. Todo se ejecuta en WebAssembly. Se genera un fichero wasm y otro  fichero con mi codigo.
1. No tendremos acceso directo a bd
1. El ejemplo de fetch se hace con un http.getfromjson, asi que necesitamos una API Rest para consumir esos datos.
1. BalzorWASM es lo que más se parece a una SPA.
1. Ventaja de webassenbly: no necesitas conexión permanente, puedes almacenar en local en el cliente y cuando recupere conexión mandar los datos, cosa que en el otro modelo que todo se ejecuta en el backend si perdemos conexión tenemos una pantalla de error y no tenemos esta opción.

## API

1. Usamos `dotnet new webapi --name TestAPI`
1. Antes de .net 5 necesitabamos una entrada de aplicación con main(), con su namespace, etc...
1. Ahora se parte por convención de un Program.cs sin necesidad de escribir todo lo anterior.
1. Antes se explicitaba como levantar el host usando kestrel, que se encarga de abrir los puertos por los que escucha nuestra aplicación.
1. Las peticiones van en un pipeline de middlewares, la respuestas se pueden generar cuando llegue al midleware que sabe como hacerlo, no tiene que recorrer todo.
1. Antes los middleware se agregaban en startup.cs, en el metodo Configure son los app.UseXXXX. Hay un middleware un tanto especial donde se pone todo, UseEndpoints(endpoints => endopoints.MapControllers()) es el que si no encuentra el controlador devuelve 404. Tenemos el middleware UseDeveloperExceptionPage() que nos muestra los errores y la pila.
1. Antes los servicios estaba en otro metodo en Startup: ConfigureServices. Es donde añadiriamos el AddTransient dbcontext...
1. Ahora usamos el builder, toda la parte de levantar el host se resume en una sola linea con el builder.
1. En la ultima versión ya no se usa Swashbuckle.AspNetCore, si no directamente OpenApi.
