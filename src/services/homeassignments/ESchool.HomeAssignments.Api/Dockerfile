#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ESchool.HomeAssignments.Api/ESchool.HomeAssignments.Api.csproj", "ESchool.HomeAssignments.Api/"]
COPY ["ESchool.HomeAssignments.Domain/ESchool.HomeAssignments.Domain.csproj", "ESchool.HomeAssignments.Domain/"]
RUN dotnet restore "ESchool.HomeAssignments.Api/ESchool.HomeAssignments.Api.csproj"
COPY . .
WORKDIR "/src/ESchool.HomeAssignments.Api"
RUN dotnet build "ESchool.HomeAssignments.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.HomeAssignments.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.HomeAssignments.Api.dll"]