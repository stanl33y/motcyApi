# Build 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y netcat-openbsd

COPY --from=build /app/out .
COPY --from=build /app/wait-for-it.sh .

RUN chmod +x wait-for-it.sh

ENTRYPOINT ["bash", "-c", "/app/wait-for-it.sh postgresdb 5432 -- dotnet motcyApi.dll"]
