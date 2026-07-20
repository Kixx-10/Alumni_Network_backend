# Step 1: Build Stage using .NET 10.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Project file ကို copy ကူးပြီး restore လုပ်ခြင်း
COPY ["Alumni/Alumni.csproj", "Alumni/"]
RUN dotnet restore "Alumni/Alumni.csproj"

# ကျန်သည့် ဖိုင်အားလုံးကို copy ကူးပြီး publish လုပ်ခြင်း
COPY . .
WORKDIR "/src/Alumni"
RUN dotnet publish "Alumni.csproj" -c Release -o /app/out

# Step 2: Runtime Stage using .NET 10.0 ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Alumni.dll"]