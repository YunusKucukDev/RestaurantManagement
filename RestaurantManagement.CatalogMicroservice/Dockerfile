FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# GitHub ana dizinindeki (Root) klasör isimlerine göre güncelledik
COPY ["RestaurantManagement.CatalogMicroservice/RestaurantManagement.CatalogMicroservice.csproj", "RestaurantManagement.CatalogMicroservice/"]
COPY ["RestaaurantManagement.DtoLayer/RestaaurantManagement.DtoLayer.csproj", "RestaaurantManagement.DtoLayer/"]

RUN dotnet restore "RestaurantManagement.CatalogMicroservice/RestaurantManagement.CatalogMicroservice.csproj"

COPY . .
WORKDIR "/src/RestaurantManagement.CatalogMicroservice"
RUN dotnet build "RestaurantManagement.CatalogMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestaurantManagement.CatalogMicroservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestaurantManagement.CatalogMicroservice.dll"]