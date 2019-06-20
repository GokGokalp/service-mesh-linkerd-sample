#Build Stage
FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /workdir

COPY ./src/ProductGateway.API ./src/ProductGateway.API/

RUN dotnet restore ./src/ProductGateway.API/ProductGateway.API.csproj
RUN dotnet publish ./src/ProductGateway.API/ProductGateway.API.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /publish /publish
WORKDIR /publish
EXPOSE 5000
ENTRYPOINT ["dotnet", "ProductGateway.API.dll"]