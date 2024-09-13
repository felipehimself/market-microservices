FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Market.Inventory.sln .
COPY src/Market.Inventory.Api/*.csproj ./src/Market.Inventory.Api/
COPY src/Market.Inventory.Application/*.csproj ./src/Market.Inventory.Application/
COPY src/Market.Inventory.CrossCutting/*.csproj ./src/Market.Inventory.CrossCutting/
COPY src/Market.Inventory.Domain/*.csproj ./src/Market.Inventory.Domain/
COPY src/Market.Inventory.Infrastructure/*.csproj ./src/Market.Inventory.Infrastructure/
COPY src/Market.Inventory.IoC/*.csproj ./src/Market.Inventory.IoC/

RUN dotnet restore

COPY . . 

WORKDIR /app/src/Market.Inventory.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Market.Inventory.Api.dll"]
