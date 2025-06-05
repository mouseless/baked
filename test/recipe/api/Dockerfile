# syntax=docker/dockerfile:1.7-labs

FROM node:22-bullseye as init

COPY test/recipe/api/package.json .
COPY test/recipe/api/package-lock.json .
RUN npm install

COPY test/recipe/api .

CMD ["node", "index.js"]

