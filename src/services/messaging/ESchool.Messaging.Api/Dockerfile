FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/services/messaging/ESchool.Messaging.Api/ESchool.Messaging.Api.csproj", "ESchool.Messaging.Api/"]
RUN dotnet restore "src/services/messaging/ESchool.Messaging.Api/ESchool.Messaging.Api.csproj"
COPY . .
WORKDIR "/src/ESchool.Messaging.Api"
RUN dotnet build "ESchool.Messaging.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESchool.Messaging.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESchool.Messaging.Api.dll"]
