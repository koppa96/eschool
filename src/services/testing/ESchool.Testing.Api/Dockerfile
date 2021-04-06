#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/services/testing/ESchool.Testing.Api/ESchool.Testing.Api.csproj", "src/services/testing/ESchool.Testing.Api/"]
COPY ["src/libs/ESchool.Libs.AspNetCore/ESchool.Libs.AspNetCore.csproj", "src/libs/ESchool.Libs.AspNetCore/"]
COPY ["src/libs/ESchool.Libs.Outbox/ESchool.Libs.Outbox.csproj", "src/libs/ESchool.Libs.Outbox/"]
COPY ["src/libs/ESchool.Libs.Application/ESchool.Libs.Application.csproj", "src/libs/ESchool.Libs.Application/"]
COPY ["src/libs/ESchool.Libs.Domain/ESchool.Libs.Domain.csproj", "src/libs/ESchool.Libs.Domain/"]
COPY ["src/services/testing/ESchool.Testing.Application/ESchool.Testing.Application.csproj", "src/services/testing/ESchool.Testing.Application/"]
COPY ["src/services/classregister/ESchool.ClassRegister.Interface/ESchool.ClassRegister.Interface.csproj", "src/services/classregister/ESchool.ClassRegister.Interface/"]
COPY ["src/services/testing/ESchool.Testing.Domain/ESchool.Testing.Domain.csproj", "src/services/testing/ESchool.Testing.Domain/"]
RUN dotnet restore "src/services/testing/ESchool.Testing.Api/ESchool.Testing.Api.csproj"
COPY . .
WORKDIR "/src/src/services/testing/ESchool.Testing.Api"
RUN dotnet build "ESchool.Testing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.Testing.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.Testing.Api.dll"]