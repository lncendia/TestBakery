# Указываем docker-image для сборки
FROM node:21-alpine as BUILD_IMAGE

# Указываем рабочую область
WORKDIR /app

# Копируем настройки сборки приложения
COPY package.json .

# Собираем проект
RUN npm install

# Копируем проект в текущую директорию
COPY . .

# Запускаем vite сборку проекта
RUN npm run build

# Указываем docker-image для работы с собранной сборкой
FROM node:21-alpine as PRODUCTION_IMAGE

# Указываем рабочую область
WORKDIR /app

# Копируем собранные материалы vite
COPY --from=BUILD_IMAGE /app/dist /app/dist

# Указываем порт нашего приложения
EXPOSE 4200

# Устанавливаем angular
RUN npm install -g @angular/cli

# Запускаем предварительный просмотр приложения
CMD ["npm", "run", "start"]
