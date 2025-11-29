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

## Iniciar BBDD

- SQLServer Dev

1. En VisualStudio2022 instalar complemento MSSQLocalDB
1. En explorador de objetos SQL crear base de datos y tabla. Actualizar para ejectuar cambios en t-sql
1. En powershell: `winget install Microsoft.sqlcmd`
1. Consultar estado bbdd pruebas: `sqllocaldb info MSSQLLocalDB`
1. Iniciar bbdd sin VS2022: `sqllocaldb start MSSQLLocalDB` y `sqlcmd -S "np:\\.\pipe\LOCALDB#1234\tsql\query" -Q "SELECT * FROM sys.databases;" -o 'dBases.txt'`
1. Ver cheatsheet comandos sqlcmd
1. En proyecto: `dotnet add package Microsoft.Data.SqlClient`

## Iniciar git

1. Instalar `winget install "Git.Git" --source winget`
1. Iniciar `git init`
1. Configurar `git config --global user.name "nombreusuario"`
1. Configurar `git config --global user.email "user@mail"`
1. Configurar repo creado en github `git remote add REPONAME REPOURL`
1. Configurar flujo por defecto `git push --set-upstream TestNet master`
1. Añadir codigo `git add . | git commit -m "Tema:Cambio" | git push`