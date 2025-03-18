# Use .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only project files first
COPY Backend/*.csproj ./
RUN dotnet restore

# Copy remaining files
COPY . ./

# Build the application
RUN dotnet publish Backend.sln -c Release -o /out

# Use lightweight runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

EXPOSE 5071
ENTRYPOINT ["dotnet", "Backend.dll"]

