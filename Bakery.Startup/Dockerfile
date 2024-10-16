FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bakery.Startup/Bakery.Startup.csproj", "Bakery.Startup/"]
COPY ["Bakery.Infrastructure.Identity/Bakery.Infrastructure.Identity.csproj", "Bakery.Infrastructure.Identity/"]
COPY ["Bakery.Application/Bakery.Application.csproj", "Bakery.Application/"]
COPY ["Bakery.Domain/Bakery.Domain.csproj", "Bakery.Domain/"]
COPY ["Bakery.Infrastructure.Web/Bakery.Infrastructure.Web.csproj", "Bakery.Infrastructure.Web/"]
COPY ["Bakery.Infrastructure.Storage/Bakery.Infrastructure.Storage.csproj", "Bakery.Infrastructure.Storage/"]
RUN dotnet restore "Bakery.Startup/Bakery.Startup.csproj"
COPY . .
WORKDIR "/src/Bakery.Startup"
RUN dotnet build "Bakery.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bakery.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bakery.Startup.dll"]
