#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Hotel/Hotel.csproj", "Hotel/"]
COPY ["Services/Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "./Hotel/Hotel.csproj"

COPY Services/Hotel/. ./Hotel/
COPY Services/Messages/. ./Messages/
RUN dotnet build "./Hotel/Hotel.csproj" -c Release -o /app/build -nowarn:CS8618

FROM build AS publish
RUN dotnet publish "./Hotel/Hotel.csproj" -c Release -o /app/publish --self-contained false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotel.dll"]