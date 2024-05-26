#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Item/Item.Presentation/Item.Presentation.csproj", "Services/Item/Item.Presentation/"]
COPY ["Services/Item/Item.BusinessLogic/Item.BusinessLogic.csproj", "Services/Item/Item.BusinessLogic/"]
COPY ["Services/Shared/Shared.csproj", "Services/Shared/"]
COPY ["Services/Item/Item.DataAccess/Item.DataAccess.csproj", "Services/Item/Item.DataAccess/"]
RUN dotnet restore "./Services/Item/Item.Presentation/./Item.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Item/Item.Presentation"
RUN dotnet build "./Item.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Item.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Item.Presentation.dll"]