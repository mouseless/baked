# syntax=docker/dockerfile:1.7-labs

FROM node:22-bullseye as init

COPY test/recipe/service/stub-api-dependency/package.json .
COPY test/recipe/service/stub-api-dependency/package-lock.json .
RUN npm install

COPY test/recipe/service/stub-api-dependency .

CMD ["node", "index.js"]

