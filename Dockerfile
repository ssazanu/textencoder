FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TextEncoder/TextEncoder.csproj", "TextEncoder/"]
RUN dotnet restore "TextEncoder/TextEncoder.csproj"
COPY . .
WORKDIR "/src/TextEncoder"
RUN dotnet build "TextEncoder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TextEncoder.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextEncoder.dll"]
