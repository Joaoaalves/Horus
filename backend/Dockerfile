# ---------- Base runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ENV ASPNETCORE_URLS=http://+:7105
WORKDIR /home/app
EXPOSE 7105

# ---------- Base build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS buildbase
WORKDIR /src

COPY ["Horus.sln", "./"]
COPY ["Horus.Domain/Horus.Domain.csproj", "Horus.Domain/"]
COPY ["Horus.Infrastructure/Horus.Infrastructure.csproj", "Horus.Infrastructure/"]
COPY ["Horus.Application/Horus.Application.csproj", "Horus.Application/"]
COPY ["Horus.Api/Horus.Api.csproj", "Horus.Api/"]
COPY ["tests/Horus.Tests.Unit/Horus.Tests.Unit.csproj", "tests/Horus.Tests.Unit/Horus.Tests.Unit.csproj"]
COPY ["tests/Horus.Tests.Integration/Horus.Tests.Integration.csproj", "tests/Horus.Tests.Integration/Horus.Tests.Integration.csproj"]

RUN dotnet restore Horus.sln

COPY . .

# ---------- App build ----------
FROM buildbase AS build
RUN dotnet publish Horus.Api -c Release -o /app/publish

# ---------- Run migrations ----------
FROM buildbase AS migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet-ef", "database", "update", "--project", "./Horus.Infrastructure", "--startup-project", "./Horus.Api"]

# ---------- Final runtime ----------
FROM base AS final
WORKDIR /home/app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Horus.Api.dll"]
