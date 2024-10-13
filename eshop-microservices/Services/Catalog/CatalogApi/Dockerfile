# See https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the Linux-based .NET SDK image for building and running the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Services/Catalog/CatalogApi/CatalogApi.csproj", "Services/Catalog/CatalogApi/"]
RUN dotnet restore "./Services/Catalog/CatalogApi/CatalogApi.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/Services/Catalog/CatalogApi"
RUN dotnet build "./CatalogApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./CatalogApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use a runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the ports
EXPOSE 8080
EXPOSE 8081

# Set the entry point for the application
ENTRYPOINT ["dotnet", "CatalogApi.dll"]
