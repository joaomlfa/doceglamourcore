FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
COPY . /app
WORKDIR /app
ENTRYPOINT ["dotnet", "DoceGlamourCore.dll"]
