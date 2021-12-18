#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tournament/Tournament.API.csproj", "Tournament/"]
COPY ["Tournament.Logic/Tournament.Logic.csproj", "Tournament.Logic/"]
COPY ["Interfaces/Tournament.Interfaces.csproj", "Interfaces/"]
COPY ["Tournament.Common/Tournament.Common.Objects.csproj", "Tournament.Common/"]
COPY ["Repository.Mongo/Tournamnent.Repository.Mongo.csproj", "Repository.Mongo/"]
RUN dotnet restore "Tournament/Tournament.API.csproj"
COPY . .
WORKDIR "/src/Tournament"
RUN dotnet build "Tournament.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tournament.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tournament.API.dll"]