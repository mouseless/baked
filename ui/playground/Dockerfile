# syntax=docker/dockerfile:1.7-labs

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

COPY --parents ui .

# build module
RUN cd ui ; npm run postinstall ; npm run build

# creates a .tgz package file at the root
RUN cd ui ; npm pack --pack-destination ./playground

# prepare playground project
## baked version
RUN BAKED_VER=$(grep '"version":' ui/package.json | head -1 | awk -F'"' '{print $4}') && \
    sed -i "s/\"\$@mouseless\/baked\"/\"$BAKED_VER\"/" ui/playground/package.json
## other packages
RUN node ui/playground/package-sync.mjs ../package.json package.json
## module name
RUN sed -i 's#"\.\./src/module"#"@mouseless/baked"#g' ui/playground/nuxt.config.ts

# finds package file name including version and installs it on top of existing.
# this is to have the local npm package isolated from its source to simulate
# production usage
RUN cd ui/playground ; npm i $(find . -name 'mouseless-baked-*.tgz' -print -quit)

# generate static site with specified environment
RUN cd ui/playground ; npm run generate

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
