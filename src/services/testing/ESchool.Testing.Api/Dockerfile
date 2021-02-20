#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ESchool.Testing.Api/ESchool.Testing.Api.csproj", "ESchool.Testing.Api/"]
COPY ["ESchool.Testing.Domain/ESchool.Testing.Domain.csproj", "ESchool.Testing.Domain/"]
RUN dotnet restore "ESchool.Testing.Api/ESchool.Testing.Api.csproj"
COPY . .
WORKDIR "/src/ESchool.Testing.Api"
RUN dotnet build "ESchool.Testing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.Testing.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.Testing.Api.dll"]