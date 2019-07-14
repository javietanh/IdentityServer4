FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["IdentityServices/Identity/Identity.csproj", "IdentityServices/Identity/"]
RUN dotnet restore "IdentityServices/Identity/Identity.csproj"
COPY . .
WORKDIR "/src/IdentityServices/Identity"
RUN dotnet build "Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Services.Identity.dll"]
