# Description: Dockerfile for the MarCorp.DemoBack.Presentation.WebApi project.
# Author: Daniel Mar

# (build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src
COPY . .
WORKDIR "/src/MarCorp.DemoBack.Presentation.WebApi"
RUN dotnet restore
RUN dotnet publish -c Release -o out

# (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build-env /src/MarCorp.DemoBack.Presentation.WebApi/out .
ENTRYPOINT ["dotnet", "MarCorp.DemoBack.Presentation.WebApi.dll"]