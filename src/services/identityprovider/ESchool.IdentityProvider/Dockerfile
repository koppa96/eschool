#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/services/identityprovider/ESchool.IdentityProvider/ESchool.IdentityProvider.csproj", "src/services/identityprovider/ESchool.IdentityProvider/"]
COPY ["src/libs/ESchool.Libs.Outbox.AspNetCore/ESchool.Libs.Outbox.AspNetCore.csproj", "src/libs/ESchool.Libs.Outbox.AspNetCore/"]
COPY ["src/libs/ESchool.Libs.Outbox/ESchool.Libs.Outbox.csproj", "src/libs/ESchool.Libs.Outbox/"]
COPY ["src/libs/ESchool.Libs.AspNetCore/ESchool.Libs.AspNetCore.csproj", "src/libs/ESchool.Libs.AspNetCore/"]
COPY ["src/libs/ESchool.Libs.Application/ESchool.Libs.Application.csproj", "src/libs/ESchool.Libs.Application/"]
COPY ["src/libs/ESchool.Libs.Domain/ESchool.Libs.Domain.csproj", "src/libs/ESchool.Libs.Domain/"]
COPY ["src/services/identityprovider/ESchool.IdentityProvider.Application/ESchool.IdentityProvider.Application.csproj", "src/services/identityprovider/ESchool.IdentityProvider.Application/"]
COPY ["src/services/identityprovider/ESchool.IdentityProvider.Domain/ESchool.IdentityProvider.Domain.csproj", "src/services/identityprovider/ESchool.IdentityProvider.Domain/"]
COPY ["src/libs/ESchool.Libs.Outbox.EntityFrameworkCore/ESchool.Libs.Outbox.EntityFrameworkCore.csproj", "src/libs/ESchool.Libs.Outbox.EntityFrameworkCore/"]
COPY ["src/services/identityprovider/ESchool.IdentityProvider.Interface/ESchool.IdentityProvider.Interface.csproj", "src/services/identityprovider/ESchool.IdentityProvider.Interface/"]
RUN dotnet restore "src/services/identityprovider/ESchool.IdentityProvider/ESchool.IdentityProvider.csproj"
COPY . .
WORKDIR "/src/src/services/identityprovider/ESchool.IdentityProvider"
RUN dotnet build "ESchool.IdentityProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.IdentityProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.IdentityProvider.dll"]