#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EGS.Store/EGS.Store.csproj", "EGS.Store/"]
RUN dotnet restore "EGS.Store/EGS.Store.csproj"
COPY . .
WORKDIR "/src/EGS.Store"
RUN dotnet build "EGS.Store.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EGS.Store.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EGS.Store.dll"]