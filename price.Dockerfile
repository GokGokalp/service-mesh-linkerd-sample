#Build Stage
FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /workdir

COPY ./src/Price.API ./src/Price.API/

RUN dotnet restore ./src/Price.API/Price.API.csproj
RUN dotnet publish ./src/Price.API/Price.API.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /publish /publish
WORKDIR /publish
EXPOSE 5000
ENTRYPOINT ["dotnet", "Price.API.dll"]