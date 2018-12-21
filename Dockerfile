FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/*.csproj ./car-catalog/
WORKDIR /app/car-catalog
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY src/. ./car-catalog/
WORKDIR /app/car-catalog
RUN dotnet publish -c Release -o out

# test application -- see: dotnet-docker-unit-testing.md
#FROM build AS testrunner
#WORKDIR /app/tests
#COPY tests/. .
#ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM microsoft/dotnet:2.2-runtime AS runtime
WORKDIR /app
COPY --from=build /app/car-catalog/out ./
ENTRYPOINT ["dotnet", "CarCatalog.dll"]
