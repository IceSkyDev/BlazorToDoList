FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BlazorServer.csproj", ""]
RUN dotnet restore "BlazorServer.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BlazorServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "BlazorServer.dll", "--server.urls", "http://*:5000"]
