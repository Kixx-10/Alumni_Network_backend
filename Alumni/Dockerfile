# Step 1: .NET 10 SDK သုံးပြီး App ကို Build/Publish လုပ်ခြင်း
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Project files များကို copy ကူးပြီး restore လုပ်ခြင်း
COPY . .
RUN dotnet restore

# Release mode ဖြင့် Publish လုပ်ခြင်း
RUN dotnet publish -c Release -o /app/out

# Step 2: .NET 10 Runtime သုံးပြီး App ကို Run ခြင်း
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

# Render ပေါ်တွင် Port 8080 ဖြင့် ပွင့်စေရန်
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Alumni.dll"]