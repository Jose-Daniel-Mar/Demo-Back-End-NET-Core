## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["MarCorp.DemoBack.Services.WebApi/MarCorp.DemoBack.Services.WebApi.csproj", "MarCorp.DemoBack.Services.WebApi/"]
#COPY ["MarCorp.DemoBack.Aplication.DTO/MarCorp.DemoBack.Application.DTO.csproj", "MarCorp.DemoBack.Aplication.DTO/"]
#COPY ["MarCorp.DemoBack.Aplication.Interface/MarCorp.DemoBack.Application.Interface.csproj", "MarCorp.DemoBack.Aplication.Interface/"]
#COPY ["MarCorp.DemoBack.Support.Common/MarCorp.DemoBack.Support.Common.csproj", "MarCorp.DemoBack.Support.Common/"]
#COPY ["MarCorp.DemoBack.Aplication.Main/MarCorp.DemoBack.Application.Main.csproj", "MarCorp.DemoBack.Aplication.Main/"]
#COPY ["MarCorp.DemoBack.Domain.Interface/MarCorp.DemoBack.Domain.Interface.csproj", "MarCorp.DemoBack.Domain.Interface/"]
#COPY ["MarCorp.DemoBack.Domain.Models/MarCorp.DemoBack.Domain.Models.csproj", "MarCorp.DemoBack.Domain.Models/"]
#COPY ["MarCorp.DemoBack.Data.Connections/MarCorp.DemoBack.Data.Connections.csproj", "MarCorp.DemoBack.Data.Connections/"]
#COPY ["MarCorp.DemoBack.Data.Interface/MarCorp.DemoBack.Data.Interface.csproj", "MarCorp.DemoBack.Data.Interface/"]
#COPY ["MarCorp.DemoBack.Data.Repository/MarCorp.DemoBack.Data.Repository.csproj", "MarCorp.DemoBack.Data.Repository/"]
#COPY ["MarCorp.DemoBack.Domain.Core/MarCorp.DemoBack.Domain.Core.csproj", "MarCorp.DemoBack.Domain.Core/"]
#COPY ["MarCorp.DemoBack.Support.Logging/MarCorp.DemoBack.Support.Logging.csproj", "MarCorp.DemoBack.Support.Logging/"]
#COPY ["MarCorp.DemoBack.Support.Mapper/MarCorp.DemoBack.Support.Mapper.csproj", "MarCorp.DemoBack.Support.Mapper/"]
#RUN dotnet restore "./MarCorp.DemoBack.Services.WebApi/MarCorp.DemoBack.Services.WebApi.csproj"
#COPY . .
#WORKDIR "/src/MarCorp.DemoBack.Services.WebApi"
#RUN dotnet build "./MarCorp.DemoBack.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./MarCorp.DemoBack.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "MarCorp.DemoBack.Services.WebApi.dll"]

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src
COPY . .
WORKDIR "/src/MarCorp.DemoBack.Services.WebApi"
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build-env /src/MarCorp.DemoBack.Services.WebApi/out .
ENTRYPOINT ["dotnet", "MarCorp.DemoBack.Services.WebApi.dll"]