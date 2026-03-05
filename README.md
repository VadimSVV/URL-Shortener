URLShortener — ASP.NET Core

Описание проекта

Web-приложение URLShortener на платформе ASP.NET Core Razor Pages. Полноценный сервис сокращения ссылок с админ-панелью (Backend)

Примечания:

Система контроля версий Git

База данных MySQL (Entity Framework Core)

Веб-сервер Kestrel (ASP.NET Core)

Архитектура Razor Pages

ORM Entity Framework Core

Адаптивный дизайн Bootstrap 5

Менеджер зависимостей NuGet

Среда разработки .NET 

Реализованный функционал:

Сокращение URL — ввод длинной ссылки → короткий код

Статистика кликов — счетчик переходов по каждой ссылке

Перенаправление — /r/{shortcode} → оригинальный URL

Копирование — перенос в буфер обмена

Админ-панель (CRUD):

CREATE

READ

UPDATE

DELETE

Технологический стек 

Backend: ASP.NET Core Razor Pages

ORM: Entity Framework Core (MySQL)

База данных: MySQL/MariaDB

Frontend: Bootstrap 5 + JavaScript

Менеджер зависимостей: NuGet

Контроль версий: Git

Запуск проекта

1. Восстановить пакеты
dotnet restore

2. Миграции БД (создать/обновить таблицы)
dotnet ef database update

3. Запуск
dotnet run

Доступ: http://localhost:5042
