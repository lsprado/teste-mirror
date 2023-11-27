FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy everything else and build
COPY . .

# Run restore packages
RUN dotnet restore ContosoUniversity.WebApplication.csproj

RUN dotnet publish ContosoUniversity.WebApplication.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ContosoUniversity.WebApplication.dll"]
