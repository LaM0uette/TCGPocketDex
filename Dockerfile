# Étape build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY TCGPocketDex.sln ./
COPY TCGPocketDex.Api/TCGPocketDex.Api.csproj TCGPocketDex.Api/
COPY TCGPocketDex.Contracts/TCGPocketDex.Contracts.csproj TCGPocketDex.Contracts/
RUN dotnet restore

COPY . .
WORKDIR /src/TCGPocketDex.Api
RUN dotnet publish -c Release -o /app/publish

# Étape runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Variables injectées au runtime (venant de docker-compose ou GitHub Actions)
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "TCGPocketDex.Api.dll"]
