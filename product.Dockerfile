#Build Stage
FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /workdir

COPY ./src/Product.API ./src/Product.API/

RUN dotnet restore ./src/Product.API/Product.API.csproj
RUN dotnet publish ./src/Product.API/Product.API.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /publish /publish
WORKDIR /publish
EXPOSE 5000
ENTRYPOINT ["dotnet", "Product.API.dll"]