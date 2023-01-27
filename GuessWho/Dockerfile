FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GuessWho.WebUI/GuessWho.WebUI.csproj", "GuessWho.WebUI/"]
RUN dotnet restore "GuessWho.WebUI/GuessWho.WebUI.csproj"
COPY . .
WORKDIR "/src/GuessWho.WebUI"
RUN dotnet build "GuessWho.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GuessWho.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "GuessWho.WebUI.dll"]