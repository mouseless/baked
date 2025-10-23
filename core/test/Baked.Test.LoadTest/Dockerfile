# syntax=docker/dockerfile:1.7-labs

FROM node:24-alpine as init

COPY core/test/Baked.Test.LoadTest/package.json .
COPY core/test/Baked.Test.LoadTest/package-lock.json .
RUN npm install

COPY core/test/Baked.Test.LoadTest .

CMD ["node", "index.js"]
