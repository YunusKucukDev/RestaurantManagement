FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Dosyalarý GitHub'daki tam yollarýyla kopyalýyoruz
COPY ["Services/Catalog/RestaurantManagement.CatalogMicroservice/RestaurantManagement.CatalogMicroservice.csproj", "Services/Catalog/RestaurantManagement.CatalogMicroservice/"]
COPY ["FrontEnd/RestaaurantManagement.DtoLayer/RestaaurantManagement.DtoLayer.csproj", "FrontEnd/RestaaurantManagement.DtoLayer/"]

# Restore komutu (Yolun doðruluðundan emin ol)
RUN dotnet restore "Services/Catalog/RestaurantManagement.CatalogMicroservice/RestaurantManagement.CatalogMicroservice.csproj"

COPY . .
WORKDIR "/src/Services/Catalog/RestaurantManagement.CatalogMicroservice"
RUN dotnet build "RestaurantManagement.CatalogMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestaurantManagement.CatalogMicroservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestaurantManagement.CatalogMicroservice.dll"]