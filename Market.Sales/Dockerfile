FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

COPY Market.Sales.sln .
COPY src/Market.Sales.Api/*.csproj ./src/Market.Sales.Api/
COPY src/Market.Sales.Application/*.csproj ./src/Market.Sales.Application/
COPY src/Market.Sales.CrossCutting/*.csproj ./src/Market.Sales.CrossCutting/
COPY src/Market.Sales.Domain/*.csproj ./src/Market.Sales.Domain/
COPY src/Market.Sales.Infrastructure/*.csproj ./src/Market.Sales.Infrastructure/
COPY src/Market.Sales.IoC/*.csproj ./src/Market.Sales.IoC/

RUN dotnet restore

COPY . . 

WORKDIR /app/src/Market.Sales.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Market.Sales.Api.dll"]
