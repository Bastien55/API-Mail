#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API-Mail.csproj", "API-Mail/"]
WORKDIR "/src/API-Mail"
RUN dotnet restore "API-Mail.csproj"
COPY . .
RUN dotnet build "API-Mail.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API-Mail.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API-Mail.dll"]