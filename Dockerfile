FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TrendSense.WebApi/TrendSense.WebApi.csproj", "TrendSense.WebApi/"]
COPY ["TrendSense.Application/TrendSense.Application.csproj", "TrendSense.Application/"]
COPY ["TrendSense.Domain/TrendSense.Domain.csproj", "TrendSense.Domain/"]
COPY ["TrendSense.Infrastructure/TrendSense.Infrastructure.csproj", "TrendSense.Infrastructure/"]
COPY ["TrendSense.Persistence/TrendSense.Persistence.csproj", "TrendSense.Persistence/"]

RUN dotnet restore "TrendSense.WebApi/TrendSense.WebApi.csproj"

COPY . .

WORKDIR "/src/TrendSense.WebApi"

RUN dotnet publish "TrendSense.WebApi.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TrendSense.WebApi.dll"]