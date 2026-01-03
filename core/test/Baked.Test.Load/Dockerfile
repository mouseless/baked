# syntax=docker/dockerfile:1.7-labs

FROM node:24-alpine as init

COPY core/test/Baked.Test.Load/package.json .
COPY core/test/Baked.Test.Load/package-lock.json .
RUN npm install

COPY core/test/Baked.Test.Load .

CMD ["node", "index.js"]
