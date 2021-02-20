#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ESchool.ApiGateway/ESchool.ApiGateway.csproj", "ESchool.ApiGateway/"]
RUN dotnet restore "ESchool.ApiGateway/ESchool.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ESchool.ApiGateway"
RUN dotnet build "ESchool.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.ApiGateway.dll"]