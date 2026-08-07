<div align="center">

# 📈 TrendSense

Backend-сервис на ASP.NET Core для отслеживания котировок акций Московской биржи (MOEX).
Синхронизирует данные с **MOEX ISS API**, хранит их в базе и отдаёт через REST API с JWT-аутентификацией и персональными списками наблюдения.

<br>

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-ORM-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-orange?style=for-the-badge)
![AutoMapper](https://img.shields.io/badge/AutoMapper-Mapping-blue?style=for-the-badge)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-API_Docs-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)

</div>

## 🛠 Стек технологий

<div align="center">

![C#](https://skillicons.dev/icons?i=cs)
![.NET](https://skillicons.dev/icons?i=dotnet)
![SQLite](https://skillicons.dev/icons?i=sqlite)
![Visual Studio](https://skillicons.dev/icons?i=visualstudio)
![Git](https://skillicons.dev/icons?i=git)
![GitHub](https://skillicons.dev/icons?i=github)

</div>

<br>

## 🏗 Архитектура

Проект построен на принципах **Clean Architecture** с разделением на 4 слоя:

- **Domain** — сущности и бизнес-правила без внешних зависимостей
- **Application** — команды и запросы (CQRS через MediatR), интерфейсы, DTO
- **Infrastructure / Persistence** — клиент MOEX ISS, JWT, EF Core, работа с БД
- **WebApi** — контроллеры, middleware, Swagger

Каждый слой зависит только от более внутренних, что позволяет менять источник данных или хранилище без переписывания бизнес-логики.

<br>

## 🚀 Возможности

- 🔄 Синхронизация котировок акций с MOEX ISS API
- 📊 Публичный просмотр списка акций и данных по конкретному тикеру
- 🔐 Регистрация и вход через JWT-аутентификацию (ASP.NET Core Identity)
- ⭐ Персональные списки наблюдения (watchlists) — создание, удаление, добавление/удаление акций
- 🧩 Версионирование API
- 📘 Интерактивная документация через Swagger UI

<br>

<br>

## 🔧 Быстрый старт

```bash
git clone https://github.com/<ваш-username>/TrendSense.git
cd TrendSense
```

Настройте строку подключения и JWT-секрет в `appsettings.Development.json`, затем примените миграции и запустите проект:

```bash
dotnet ef database update --project src/TrendSense.Persistence --startup-project src/TrendSense.WebApi
dotnet run --project src/TrendSense.WebApi
```

После запуска Swagger UI будет доступен по адресу `https://localhost:<port>/`.

<br>

## 📄 О проекте

Pet-проект, созданный в процессе изучения ASP.NET Core, для отработки Clean Architecture, CQRS и работы с внешними API на практике.
