# syntax=docker/dockerfile:1.7-labs
ARG ENVIRONMENT Production

# ---
# to install npm packages
# separated to have these run only when package.json files are changed
# ---
FROM node:22-bullseye as init

COPY --parents src/recipe/admin/package.json .
COPY --parents src/recipe/admin/package-lock.json .
COPY --parents test/recipe/admin/package.json .
COPY --parents test/recipe/admin/package-lock.json .
RUN cd src/recipe/admin; npm install
RUN cd test/recipe/admin; npm install

# ---
# to generate nuxt spa project
# ---
FROM init as generate

ARG ENVIRONMENT

# copy source files
COPY --parents src/recipe/admin .
COPY --parents test/recipe/admin .

# creates a .tgz package file at the root
RUN cd src/recipe/admin ; npm pack --pack-destination ../../..

# finds package file name including version and installs it on top of existing.
# this is to have the local npm package isolated from its source to simulate
# production usage
RUN cd test/recipe/admin ; npm i $(find ../../.. -name 'baked-recipe-admin-*.tgz' -print -quit)

# builds test project
RUN cd test/recipe/admin ; npm run build:$ENVIRONMENT

# ---
# actual running stage
# ---
FROM nginx AS final

# copy nginx config file
COPY test/recipe/admin/nginx.conf /etc/nginx/nginx.conf

# copy spa output to nginx public folder
COPY --from=generate test/recipe/admin/.output/public /www/public

# run nginx
CMD nginx -g "daemon off;"
