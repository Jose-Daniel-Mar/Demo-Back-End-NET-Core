# Description: Dockerfile for the MarCorp.DemoBack.Services.WebApi project.
# Author: Daniel Mar

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