#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ESchool.ClassRegister.Api/ESchool.ClassRegister.Api.csproj", "ESchool.ClassRegister.Api/"]
COPY ["ESchool.ClassRegister.Domain/ESchool.ClassRegister.Domain.csproj", "ESchool.ClassRegister.Domain/"]
COPY ["ESchool.Libs.Domain/ESchool.Libs.Domain.csproj", "ESchool.Libs.Domain/"]
RUN dotnet restore "ESchool.ClassRegister.Api/ESchool.ClassRegister.Api.csproj"
COPY . .
WORKDIR "/src/ESchool.ClassRegister.Api"
RUN dotnet build "ESchool.ClassRegister.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.ClassRegister.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.ClassRegister.Api.dll"]