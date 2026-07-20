# Step 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Project file ကို သီးသန့် copy ကူးပြီး restore လုပ်ခြင်း
COPY ["Alumni/Alumni.csproj", "Alumni/"]
RUN dotnet restore "Alumni/Alumni.csproj"

# ကျန်တဲ့ file အားလုံးကို copy ကူးပြီး publish လုပ်ခြင်း
COPY . .
WORKDIR "/src/Alumni"
RUN dotnet publish "Alumni.csproj" -c Release -o /app/out

# Step 2: Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Alumni.dll"]