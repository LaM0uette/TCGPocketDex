# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier la solution et les csproj
COPY TCGPocketDex.sln .
COPY TCGPocketDex.Api/TCGPocketDex.Api.csproj TCGPocketDex.Api/
COPY TCGPocketDex.Contracts/TCGPocketDex.Contracts.csproj TCGPocketDex.Contracts/

# Restore des dépendances
RUN dotnet restore TCGPocketDex.sln

# Copier le reste du code
COPY . .

# Publish l’API
RUN dotnet publish TCGPocketDex.Api/TCGPocketDex.Api.csproj -c Release -o /out

# Étape 2 : runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TCGPocketDex.Api.dll"]
