#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /src
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EGS.Api/EGS.Api.csproj", "EGS.Api/"]
RUN dotnet restore "EGS.Api/EGS.Api.csproj"
COPY . .
WORKDIR "/src/EGS.Api"
RUN dotnet build "EGS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EGS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EGS.Api.dll"]