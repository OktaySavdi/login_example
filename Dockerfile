FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

EXPOSE 5000

ARG UID=1000
ARG GID=1000

RUN groupadd --gid ${GID} buildadmin \
    && useradd --uid ${UID} --gid buildadmin --shell /bin/bash --create-home buildadmin

WORKDIR /app
COPY --from=build-env /app/out .

RUN chown -R buildadmin:buildadmin /app/* 

USER buildadmin
ENTRYPOINT ["dotnet", "SecureApp.dll"]
