FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 8080

COPY ["UserService.sln", "UserService.sln"]
COPY ["UserService.API/UserService.API.csproj", "UserService.API/"]
COPY ["UserService.Application/UserService.Application.csproj", "UserService.Application/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]

RUN dotnet restore "UserService.sln"

COPY . .

WORKDIR "/src/UserService.API"
RUN dotnet publish "UserService.API.csproj" -c Release -o /app/publish





FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /src/UserService.API/app/publish .


ENTRYPOINT ["dotnet", "UserService.API.dll"]