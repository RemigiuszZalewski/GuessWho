### STAGE 1: Build ###
FROM node:14-alpine AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
COPY . .
RUN npm install
RUN npm run build --prod
### STAGE 2: Run ###
FROM nginx:alpine
COPY --from=build /usr/src/app/dist/client-app /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80

