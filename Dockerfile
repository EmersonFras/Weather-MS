# Use the ARM64 .NET 6 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
# Copy everything in the Weather-MS folder into the container
COPY . .
RUN rm -f weather.db
# Restore dependencies for the WeatherDataService project
RUN dotnet restore WeatherDataService.csproj
# Publish the project into the /app/out folder
RUN dotnet publish WeatherDataService.csproj -c Release -o /app/out

# Use the ARM64 .NET 6 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim
WORKDIR /app
# Copy published output from the build stage
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "WeatherDataService.dll"]
