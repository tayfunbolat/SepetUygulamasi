#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CicekSepeti.Api/CicekSepeti.Api.csproj", "CicekSepeti.Api/"]
COPY ["CicekSepeti.Service/CicekSepeti.Service.csproj", "CicekSepeti.Service/"]
COPY ["CicekSepeti.Repository/CicekSepeti.Repository.csproj", "CicekSepeti.Repository/"]
COPY ["Common.Api/Common.Api.csproj", "Common.Api/"]
COPY ["CicekSepeti.Domain/CicekSepeti.Domain.csproj", "CicekSepeti.Domain/"]
RUN dotnet restore "CicekSepeti.Api/CicekSepeti.Api.csproj"
COPY . .
WORKDIR "/src/CicekSepeti.Api"
RUN dotnet build "CicekSepeti.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CicekSepeti.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CicekSepeti.Api.dll"]