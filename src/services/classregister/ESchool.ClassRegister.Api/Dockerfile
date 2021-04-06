#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/services/classregister/ESchool.ClassRegister.Api/ESchool.ClassRegister.Api.csproj", "src/services/classregister/ESchool.ClassRegister.Api/"]
COPY ["src/libs/ESchool.Libs.Outbox.AspNetCore/ESchool.Libs.Outbox.AspNetCore.csproj", "src/libs/ESchool.Libs.Outbox.AspNetCore/"]
COPY ["src/libs/ESchool.Libs.Outbox/ESchool.Libs.Outbox.csproj", "src/libs/ESchool.Libs.Outbox/"]
COPY ["src/libs/ESchool.Libs.AspNetCore/ESchool.Libs.AspNetCore.csproj", "src/libs/ESchool.Libs.AspNetCore/"]
COPY ["src/libs/ESchool.Libs.Application/ESchool.Libs.Application.csproj", "src/libs/ESchool.Libs.Application/"]
COPY ["src/libs/ESchool.Libs.Domain/ESchool.Libs.Domain.csproj", "src/libs/ESchool.Libs.Domain/"]
COPY ["src/services/classregister/ESchool.ClassRegister.Application/ESchool.ClassRegister.Application.csproj", "src/services/classregister/ESchool.ClassRegister.Application/"]
COPY ["src/services/homeassignments/ESchool.HomeAssignments.Interface/ESchool.HomeAssignments.Interface.csproj", "src/services/homeassignments/ESchool.HomeAssignments.Interface/"]
COPY ["src/services/classregister/ESchool.ClassRegister.Interface/ESchool.ClassRegister.Interface.csproj", "src/services/classregister/ESchool.ClassRegister.Interface/"]
COPY ["src/services/identityprovider/ESchool.IdentityProvider.Interface/ESchool.IdentityProvider.Interface.csproj", "src/services/identityprovider/ESchool.IdentityProvider.Interface/"]
COPY ["src/services/classregister/ESchool.ClassRegister.Domain/ESchool.ClassRegister.Domain.csproj", "src/services/classregister/ESchool.ClassRegister.Domain/"]
COPY ["src/libs/ESchool.Libs.Outbox.EntityFrameworkCore/ESchool.Libs.Outbox.EntityFrameworkCore.csproj", "src/libs/ESchool.Libs.Outbox.EntityFrameworkCore/"]
RUN dotnet restore "src/services/classregister/ESchool.ClassRegister.Api/ESchool.ClassRegister.Api.csproj"
COPY . .
WORKDIR "/src/src/services/classregister/ESchool.ClassRegister.Api"
RUN dotnet build "ESchool.ClassRegister.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.ClassRegister.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.ClassRegister.Api.dll"]