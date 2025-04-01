# Use the ARM64 .NET 6 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-arm64 AS build
WORKDIR /app
# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore
# Copy the remaining source code and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the ARM64 .NET 6 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-arm64
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "WeatherService.dll"]
