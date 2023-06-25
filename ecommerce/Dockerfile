FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

ENV ASPNETCORE_CONTENT_ROOT=/src/wwwroot

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /src
COPY --from=build /src/out ./

EXPOSE 5232

ENTRYPOINT ["dotnet", "ecommerce.dll"]