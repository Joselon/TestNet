# Apuntes de Clase
## Uso consola powershell personalizado:

1. Iniciar perfil: `New-Item -Path $PROFILE -Type File -Force`
1. Instalar editor: `winget install -e --id Neovim.Neovim`
1. Editar: `nvim $PROFILE`
1. Instalar `winget install JanDeDobbeleer.OhMyPosh --source winget`
1. Instalar fuente: `oh-my-posh font install`
1. Abrir shell : `oh-my-posh get shell` (Consultar web)

## Inicio proyecto

1. Comprobar .net instalado `dotnet --version`
1. Crear proyecto de consola `dotnet new console --name HelloWorld`
1. Construir `dotnet build`
1. Sacar release `dotnet build -c Release` en .\bin\Release

## BBDD

- SQLServer Dev

1. En VisualStudio2022 instalar complemento MSSQLocalDB
1. En explorador de objetos SQL crear base de datos y tabla. Actualizar para ejectuar cambios en t-sql
1. En powershell: `winget install Microsoft.sqlcmd`
1. Consultar estado bbdd pruebas: `sqllocaldb info MSSQLLocalDB`
1. Iniciar bbdd sin VS2022: `sqllocaldb start MSSQLLocalDB` y `sqlcmd -S "np:\\.\pipe\LOCALDB#1234\tsql\query" -Q "SELECT * FROM sys.databases;" -o 'dBases.txt'`
1. Ver cheatsheet comandos sqlcmd
1. En proyecto: `dotnet add package Microsoft.Data.SqlClient`

- Entity Framework

1. `dotnet add package Microsoft.EntityFrameworkCore --version x.0.0` - Con x la version de csproj`<TargetFramework>net9.0</TargetFramework>` (No .Core) Antiguo .- Hay que instalar el proveedor de EntityFramework para el tipo de bbdd que se vaya a usar (mssql,postgresql,etc)
1. Las clases que tratan conexiones sql deben implementar IDisposable para gestionar la conexión temporal. using en las variables que guardan SqlConnection implica que llama con.Dispose para eliminar recursos externos.
`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.0`
1. Entidas son clases POCO (Como las POJO) puras sin dependencias que usaremos para el modelo de negocio
1. `public class BlogContext : DbContext` Se debe heredar de DbContext que si depende ya de EntityFramework. Lo usamos con el patrón builder para el constructor.
1. Usamos por ejemplo una base de datos creada en un contenedor docker con docker-compose up.
1. Revisar postgresql esta levantado:
`docker logs <nombre-del-contenedor-adminer>` y 
`docker exec -it testnet-pgsql-1 psql -U demo -d demo`
 (Instalar wsl --install y docker desktop con sus reinicios)
 y `docker compose down -v` para borrar el volumen de data
 1. Añadimos al blog que el constructor inicialice la lista de post con un tipo que implemente la interfaz IColletion como es el caso de List.
 1. Ya podemos usar el Seeder.cs
 1. Cuando guardamos una consulta en una variable tenemos que indicar si vamos a querer acceder a sus dependecias con include: `var blogs = ctx.Blogs.Include(b => b.Posts).Where(b => b.Posts.Any(p => p.Id > 5))` en este caso necesario para el Console.Writeline siguiente. No le hace falta para el where.
 1. Se pueden usar LINQ para las consultas y/o usar blog.AsEnumerable(). por ejemplo para usar LINQ en memoria. ¡Cuidado con lo que traemos a memoria!
 1. Se puede usar IEnumerable encubriendo .AsEnumerable 
 1. Para no petar la memoria usar IQueryable

## Iniciar git

1. Instalar `winget install "Git.Git" --source winget`
1. Iniciar `git init`
1. Configurar `git config --global user.name "nombreusuario"`
1. Configurar `git config --global user.email "user@mail"`
1. Configurar repo creado en github `git remote add REPONAME REPOURL`
1. Configurar flujo por defecto `git push --set-upstream TestNet master`
1. Añadir codigo `git add . | git commit -m "Tema:Cambio" | git push`
