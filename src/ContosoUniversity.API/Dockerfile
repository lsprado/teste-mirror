FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /api

# Copy everything else and build
COPY . .

# Run restore packages
RUN dotnet restore ContosoUniversity.API.csproj

RUN dotnet publish ContosoUniversity.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /api
COPY --from=build /api/out .

ENTRYPOINT ["dotnet", "ContosoUniversity.API.dll"]
