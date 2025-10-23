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
COPY --parents core/src/Baked.Abstractions/Baked.Abstractions.csproj .
COPY --parents core/src/Baked/Baked.csproj .
COPY --parents core/test/Directory.Build.props .
COPY --parents core/test/Baked.Test/Baked.Test.csproj .
COPY --parents core/test/Baked.Test.Application/Baked.Test.Application.csproj .
RUN dotnet restore core/test/Baked.Test.Application

COPY . .
RUN dotnet publish ./core/test/Baked.Test.Application -c Release -o /app/publish /p:GenerateArgs="--no-ui"

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet Baked.Test.Application.dll
