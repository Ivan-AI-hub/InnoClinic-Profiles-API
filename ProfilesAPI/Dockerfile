#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfilesAPI.Web/ProfilesAPI.Web.csproj", "ProfilesAPI.Web/"]
COPY ["ProfilesAPI.Domain/ProfilesAPI.Domain.csproj", "ProfilesAPI.Domain/"]
COPY ["ProfilesAPI.Persistence/ProfilesAPI.Persistence.csproj", "ProfilesAPI.Persistence/"]
COPY ["ProfilesAPI.Presentation/ProfilesAPI.Presentation.csproj", "ProfilesAPI.Presentation/"]
COPY ["ProfilesAPI.Application.Abstraction/ProfilesAPI.Application.Abstraction.csproj", "ProfilesAPI.Application.Abstraction/"]
COPY ["ProfilesAPI.Application/ProfilesAPI.Application.csproj", "ProfilesAPI.Application/"]
RUN dotnet restore "ProfilesAPI.Web/ProfilesAPI.Web.csproj"
COPY . .
WORKDIR "/src/ProfilesAPI.Web"
RUN dotnet build "ProfilesAPI.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfilesAPI.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfilesAPI.Web.dll"]