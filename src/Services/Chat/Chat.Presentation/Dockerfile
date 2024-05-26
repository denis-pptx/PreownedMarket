#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Chat/Chat.Presentation/Chat.Presentation.csproj", "Services/Chat/Chat.Presentation/"]
COPY ["Services/Chat/Chat.Infrastructure/Chat.Infrastructure.csproj", "Services/Chat/Chat.Infrastructure/"]
COPY ["Services/Chat/Chat.Application/Chat.Application.csproj", "Services/Chat/Chat.Application/"]
COPY ["Services/Shared/Shared.csproj", "Services/Shared/"]
COPY ["Services/Chat/Chat.Domain/Chat.Domain.csproj", "Services/Chat/Chat.Domain/"]
RUN dotnet restore "./Services/Chat/Chat.Presentation/./Chat.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Chat/Chat.Presentation"
RUN dotnet build "./Chat.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Chat.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.Presentation.dll"]