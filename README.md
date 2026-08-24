<div align="center">

# 📈 TrendSense

Backend-сервис на ASP.NET Core для отслеживания котировок акций Московской биржи (MOEX).
Синхронизирует данные с **MOEX ISS API**, хранит их в базе и отдаёт через REST API с JWT-аутентификацией и персональными списками наблюдения.

<br>

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-ORM-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-orange?style=for-the-badge)
![AutoMapper](https://img.shields.io/badge/AutoMapper-Mapping-blue?style=for-the-badge)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-API_Docs-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![SQLite](https://img.shields.io/badge/SQLite-Database-003B57?style=for-the-badge&logo=sqlite&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?style=for-the-badge&logo=docker&logoColor=white)

</div>

## Стек технологий

<div align="center">

![C#](https://skillicons.dev/icons?i=cs)
![.NET](https://skillicons.dev/icons?i=dotnet)
![SQLite](https://skillicons.dev/icons?i=sqlite)
![Docker](https://skillicons.dev/icons?i=docker)
![Visual Studio](https://skillicons.dev/icons?i=visualstudio)
![Git](https://skillicons.dev/icons?i=git)
![GitHub](https://skillicons.dev/icons?i=github)

</div>

<br>

## Архитектура

Проект построен на принципах Clean Architecture с разделением ответственности между слоями:

- **Domain** — сущности и бизнес-правила без внешних зависимостей
- **Application** — команды и запросы (CQRS через MediatR), интерфейсы, DTO
- **Infrastructure / Persistence** — клиент MOEX ISS, JWT, EF Core, работа с БД
- **WebApi** — контроллеры, middleware, Swagger

Каждый слой зависит только от более внутренних, что позволяет менять источник данных или хранилище без переписывания бизнес-логики.

<br>

## Возможности
📊 Работа с акциями
- получение списка доступных акций MOEX;
- получение информации по конкретному тикеру;
- синхронизация акций с MOEX;
- обновление текущих котировок;
- хранение истории цен.
  
⭐ WatchList
- создание пользовательских списков;
- получение списков пользователя;
- удаление списка;
- добавление акций;
- удаление акций;
- получение акций внутри WatchList.
  
🔐 Аутентификация
- регистрация пользователя;
- авторизация;
- ASP.NET Core Identity;
- JWT access tokens;
  
⚙️ Фоновое обновление

Приложение содержит BackgroundService, который периодически обновляет актуальные котировки акций.

## Технические особенности

- Clean Architecture;
- CQRS + MediatR;
- FluentValidation;
- AutoMapper;
- ASP.NET Core Identity + JWT;
- глобальная обработка исключений;
- API versioning;
- Swagger/OpenAPI;
- unit-тесты;
- Docker.

## Быстрый старт

Для самого простого запуска проекта рекомендуется использовать Docker.

### Требования

Установите:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

Проверьте:

```bash
docker --version
docker compose version
```

---

### 1. Клонирование

```bash
git clone https://github.com/Reyme87/TrendSense.git
cd TrendSense
```

---

### 2. Настройка JWT

JWT signing key **не хранится в GitHub**.

Создайте файл:

```text
.env
```

в корне проекта.

Пример:

```env
JWT_KEY=your-base64-encoded-secret
```

Файл `.env.example` содержит пример переменной.

---

### 3. Запуск

Из корня проекта выполните:

```bash
docker compose up --build
```

Docker Compose запустит приложение в контейнере.

После запуска API будет доступен по адресу:

```text
http://localhost:8080
```

Swagger:

```text
http://localhost:8080/
```

Остановить приложение:

```bash
docker compose down
```

---

## Локальный запуск без Docker

Docker не является обязательным для разработки.

### Требования

- .NET 10 SDK
- Visual Studio 2026 или другой IDE с поддержкой .NET 10

Проверить версию:

```bash
dotnet --version
```

---

### 1. Клонирование

```bash
git clone https://github.com/Reyme87/TrendSense.git
cd TrendSense
```

---

### 2. Настройка JWT через User Secrets

JWT secret не хранится в `appsettings.json` или `appsettings.Development.json`.

Для локальной разработки используется **ASP.NET Core User Secrets**.

Перейдите в проект Web API:

```bash
cd TrendSense.WebApi
```

Если User Secrets ещё не настроены:

```bash
dotnet user-secrets init
```

Добавьте JWT key:

```bash
dotnet user-secrets set "Jwt:Key" "your-base64-encoded-secret"
```

Проверить:

```bash
dotnet user-secrets list
```

После этого вернитесь в корень решения:

```bash
cd ..
```

---

### 3. Запуск

```bash
dotnet run --project TrendSense.WebApi
```

После запуска Swagger будет доступен по адресу, указанному ASP.NET Core в консоли.

Обычно это:

```text
https://localhost:xxxx/
```

---

## API

После запуска приложения Swagger предоставляет интерактивную документацию API.

Основные группы endpoints:

```text
/api/v1/Auth
/api/v1/Stocks
/api/v1/WatchLists
```

Для защищённых endpoints необходимо сначала получить JWT token через authentication endpoints и передать его в Swagger через кнопку **Authorize**.

<br>

## 📄 О проекте

Проект создан как pet-project для практики разработки backend-приложений на ASP.NET Core и работы с внешними API.

Проект находится в активной разработке.
