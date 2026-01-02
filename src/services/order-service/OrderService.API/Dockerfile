# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/order-service/OrderService.API/OrderService.API.csproj", "src/services/order-service/OrderService.API/"]
COPY ["src/services/customer-service/CustomerService.Applilcation/CustomerService.Applilcation.csproj", "src/services/customer-service/CustomerService.Applilcation/"]
COPY ["src/services/customer-service/CustomerService.Domain/CustomerService.Domain.csproj", "src/services/customer-service/CustomerService.Domain/"]
COPY ["src/building-blocks/Domain.SeedWork/Domain.SeedWork.csproj", "src/building-blocks/Domain.SeedWork/"]
COPY ["src/services/customer-service/CustomerService.Infrastructure/CustomerService.Infrastructure.csproj", "src/services/customer-service/CustomerService.Infrastructure/"]
RUN dotnet restore "./src/services/order-service/OrderService.API/OrderService.API.csproj"
COPY . .
WORKDIR "/src/src/services/order-service/OrderService.API"
RUN dotnet build "./OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.API.dll"]