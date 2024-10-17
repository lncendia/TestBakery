# Указываем docker-image для сборки
FROM node:21-alpine AS build

# Указываем рабочую область
WORKDIR /app

# Копируем настройки сборки приложения
COPY Bakery.Client/package.json .

# Устанавливаем angular
RUN npm install -g @angular/cli

# Устанавливаем зависимости
RUN npm install

# Копируем исходный код
COPY Bakery.Client .

# Запускаем сборку приложения
RUN ng build

FROM nginx:latest

COPY nginx etc/nginx/conf.d

COPY --from=build app/dist/bakery-client/browser usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Expose port 443
EXPOSE 443
