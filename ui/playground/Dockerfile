# syntax=docker/dockerfile:1.7-labs
ARG ENVIRONMENT production

# ---
# to install npm packages
# separated to have these run only when package.json files are changed
# ---
FROM node:24-alpine as init

COPY --parents ui/package.json .
COPY --parents ui/package-lock.json .
RUN cd ui; npm install

# ---
# to generate nuxt spa project
# ---
FROM init as generate

ARG ENVIRONMENT

COPY --parents ui .

# build module
RUN cd ui && npm run setup

# generate static site with specified environment
RUN cd ui && npm run build:$ENVIRONMENT

# ---
# actual running stage
# ---
FROM nginx AS final

# copy nginx config file
COPY ui/playground/nginx.conf /etc/nginx/nginx.conf

# copy spa output to nginx public folder
COPY --from=generate ui/playground/.output/public /www/public

# run nginx
CMD nginx -g "daemon off;"
