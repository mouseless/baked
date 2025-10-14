# syntax=docker/dockerfile:1.7-labs

FROM node:24-alpine as init

COPY core/test/Baked.Test.StubApi/package.json .
COPY core/test/Baked.Test.StubApi/package-lock.json .
RUN npm install

COPY core/test/Baked.Test.StubApi .

CMD ["node", "index.js"]
