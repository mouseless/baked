# syntax=docker/dockerfile:1.7-labs
ARG ENVIRONMENT Production

# ---
# to install npm packages
# separated to have these run only when package.json files are changed
# ---
FROM node:24-alpine as init

COPY --parents ui/package.json .
COPY --parents ui/package-lock.json .
COPY --parents ui/playground/package.json .
COPY --parents ui/playground/package-lock.json .
RUN cd ui; npm install
RUN cd ui/playground; npm install

# ---
# to generate nuxt spa project
# ---
FROM init as generate

ARG ENVIRONMENT

# copy source files
COPY --parents ui .
COPY --parents ui/playground .

# creates a .tgz package file at the root
RUN cd ui ; npm pack --pack-destination ..

# finds package file name including version and installs it on top of existing.
# this is to have the local npm package isolated from its source to simulate
# production usage
RUN cd ui/playground ; npm i $(find ../.. -name 'baked-recipe-admin-*.tgz' -print -quit)

# builds test project
RUN cd ui/playground ; npm run build:$ENVIRONMENT

# ---
# actual running stage
# ---
FROM nginx AS final

# copy nginx config file
COPY ui/playground/nginx.conf /etc/nginx/nginx.conf

# copy spa output to nginx public folder
COPY --from=generate ui/specs/.output/public /www/public

# run nginx
CMD nginx -g "daemon off;"
