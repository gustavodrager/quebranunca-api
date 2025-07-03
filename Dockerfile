FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY src/QNF.Plataforma.API/*.csproj ./src/QNF.Plataforma.API/
COPY src/QNF.Plataforma.Application/*.csproj ./src/QNF.Plataforma.Application/
COPY src/QNF.Plataforma.Core/*.csproj ./src/QNF.Plataforma.Core/
COPY src/QNF.Plataforma.Infrastructure/*.csproj ./src/QNF.Plataforma.Infrastructure/
COPY tests/QNF.Plataforma.UnitTests/*.csproj ./tests/QNF.Plataforma.UnitTests/
COPY tests/QNF.Plataforma.IntegrationTests/*.csproj ./tests/QNF.Plataforma.IntegrationTests/

RUN dotnet restore

COPY . .

RUN dotnet publish src/QNF.Plataforma.API -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "QNF.Plataforma.API.dll"]