﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Workshop.API/Workshop.API.csproj", "Workshop.API/"]
RUN dotnet restore "Workshop.API/Workshop.API.csproj"
COPY . .
WORKDIR "/src/Workshop.API"
RUN dotnet build "Workshop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workshop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshop.API.dll"]
