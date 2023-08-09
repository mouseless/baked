FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base

ARG ENVIRONMENT=Production

EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS publish

COPY . .
RUN dotnet publish ./test/blueprints/Do.Test.Blueprints.Service.Application -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet Do.Test.Blueprints.Service.Application.dll
