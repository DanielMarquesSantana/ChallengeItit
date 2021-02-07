FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORkDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

COPY ["src/ProjectChallenge.ItiDigital.Validation/ProjectChallenge.ItiDigital.Validation.csproj", "src/ProjectChallenge.ItiDigital.Validation/"]

RUN dotnet restore "src/ProjectChallenge.ItiDigital.Validation/ProjectChallenge.ItiDigital.Validation.csproj"
COPY . .

RUN dotnet build "src/ProjectChallenge.ItiDigital.Validation/ProjectChallenge.ItiDigital.Validation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet","ProjectChallenge.ItiDigital.Validation.dll"]