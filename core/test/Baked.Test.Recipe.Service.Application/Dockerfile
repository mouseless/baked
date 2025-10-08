# syntax=docker/dockerfile:1.7-labs

ARG ENVIRONMENT Production

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

ARG ENVIRONMENT
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish

ARG ENVIRONMENT

ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT

COPY --parents core/Directory.Packages.props .
COPY --parents core/Directory.Build.props .
COPY --parents core/src/Directory.Build.props .
COPY --parents core/src/Baked.Architecture/Baked.Architecture.csproj .
COPY --parents core/src/Baked.Recipe.Service/Baked.Recipe.Service.csproj .
COPY --parents core/src/Baked.Recipe.Service.Application/Baked.Recipe.Service.Application.csproj .
COPY --parents core/test/Directory.Build.props .
COPY --parents core/test/Baked.Test.Recipe.Service/Baked.Test.Recipe.Service.csproj .
COPY --parents core/test/Baked.Test.Recipe.Service.Application/Baked.Test.Recipe.Service.Application.csproj .
RUN dotnet restore test/Baked.Test.Recipe.Service.Application

COPY . .
RUN dotnet publish ./core/test/Baked.Test.Recipe.Service.Application -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet Baked.Test.Recipe.Service.Application.dll
