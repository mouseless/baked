# syntax=docker/dockerfile:1.7-labs
ARG ENVIRONMENT Production

FROM mcr.microsoft.com/dotnet/sdk:9.0 as build

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
RUN dotnet build ./test/recipe/Baked.Test.Recipe.Service.Application --no-restore -c Release

FROM node:22-bullseye as final

ARG ENVIRONMENT

COPY --from=build  ./test/recipe/admin ./test/recipe/admin

COPY --parents test/recipe/admin/package.json .
COPY --parents test/recipe/admin/package-lock.json .
RUN cd test/recipe/admin; npm install

COPY --parents test/recipe/admin .
RUN cd test/recipe/admin ; npm run postinstall ; npm run build:$ENVIRONMENT

EXPOSE 3000
CMD ["npx", "http-server","./test/recipe/admin/.output/public","-p 3000"]

