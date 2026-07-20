# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Alumni/Alumni.csproj", "Alumni/"]
RUN dotnet restore "./Alumni/Alumni.csproj"
COPY . .
WORKDIR "/src/Alumni"
RUN dotnet build "./Alumni.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Alumni.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode
FROM base AS final
WORKDIR /app

# 1. Root User အဖြစ် ခေတ္တ ပြောင်းပါ
USER root

# 2. Published code များကို အရင် copy ကူးပါ
COPY --from=publish /app/publish .

# 3. wwwroot/uploads directory ဖန်တီးပြီး App User ($APP_UID) ဆီ Permission နှင့် Ownership အပြည့်ပေးပါ
RUN mkdir -p /app/wwwroot/uploads \
    && chmod -R 777 /app/wwwroot \
    && chown -R $APP_UID:$APP_UID /app/wwwroot

# 4. App User ဆီ ပြန်ပြောင်းပါ
USER $APP_UID

ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Alumni.dll"]