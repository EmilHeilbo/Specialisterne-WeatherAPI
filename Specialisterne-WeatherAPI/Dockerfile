FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Specialisterne-WeatherAPI.csproj", "Specialisterne-WeatherAPI/"]
WORKDIR "/src/Specialisterne-WeatherAPI"
RUN dotnet restore "./Specialisterne-WeatherAPI.csproj"
COPY . .
RUN dotnet build "./Specialisterne-WeatherAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Specialisterne-WeatherAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Specialisterne-WeatherAPI.dll"]
