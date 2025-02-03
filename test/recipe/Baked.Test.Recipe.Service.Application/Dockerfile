# syntax=docker/dockerfile:1.7-labs
ARG ENVIRONMENT Production

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

ARG ENVIRONMENT

EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish

ARG ENVIRONMENT

ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT

COPY --parents Directory.Packages.props .
COPY --parents Directory.Build.props .
COPY --parents src/Directory.Build.props .
COPY --parents src/core/Baked.Architecture/Baked.Architecture.csproj .
COPY --parents src/recipe/Baked.Recipe.Service/Baked.Recipe.Service.csproj .
COPY --parents src/recipe/Baked.Recipe.Service.Application/Baked.Recipe.Service.Application.csproj .
COPY --parents test/Directory.Build.props .
COPY --parents test/recipe/Baked.Test.Recipe.Service/Baked.Test.Recipe.Service.csproj .
COPY --parents test/recipe/Baked.Test.Recipe.Service.Application/Baked.Test.Recipe.Service.Application.csproj .
RUN dotnet restore test/recipe/Baked.Test.Recipe.Service.Application

COPY . .
RUN dotnet publish ./test/recipe/Baked.Test.Recipe.Service.Application -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet Baked.Test.Recipe.Service.Application.dll
