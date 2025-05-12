# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos csproj e restaura as dependências
COPY *.sln .
COPY src/QNF.Plataforma.API/*.csproj ./src/QNF.Plataforma.API/
COPY src/QNF.Plataforma.Application/*.csproj ./src/QNF.Plataforma.Application/
COPY src/QNF.Plataforma.Core/*.csproj ./src/QNF.Plataforma.Core/
COPY src/QNF.Plataforma.Infrastructure/*.csproj ./src/QNF.Plataforma.Infrastructure/
COPY tests/QNF.Plataforma.UnitTests/*.csproj ./tests/QNF.Plataforma.UnitTests/
COPY tests/QNF.Plataforma.IntegrationTests/*.csproj ./tests/QNF.Plataforma.IntegrationTests/

RUN dotnet restore

# Copia todo o restante do código
COPY . .

# Publica a aplicação
RUN dotnet publish src/QNF.Plataforma.API -c Release -o /app/publish

# Etapa 2: Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expõe a porta padrão do ASP.NET
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "QNF.Plataforma.API.dll"]