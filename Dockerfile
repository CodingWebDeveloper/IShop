FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReactQuery-Server.csproj", "./"]
RUN dotnet restore "./ReactQuery-Server.csproj"
COPY . .
RUN dotnet build "ReactQuery-Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ReactQuery-Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ReactQuery-Server.dll"]