URLShortener — ASP.NET Core

Описание проекта

Web-приложение URLShortener на платформе ASP.NET Core Razor Pages. Полноценный сервис сокращения ссылок с админ-панелью

Примечания:

Система контроля версий Git

База данных MySQL (Entity Framework Core)

Веб-сервер Kestrel (ASP.NET Core)

Архитектура Razor Pages

ORM Entity Framework Core

Адаптивный дизайн Bootstrap 5

Менеджер зависимостей NuGet

Среда разработки .NET 10

Реализованный функционал:

Сокращение URL — ввод длинной ссылки → короткий код

Статистика кликов — счетчик переходов по каждой ссылке

Перенаправление — /r/{shortcode} → оригинальный URL

Копирование — one-click в буфер обмена

Админ-панель (CRUD):

CREATE — создание новых коротких ссылок

READ — таблица последних 10 ссылок

UPDATE — inline редактирование URL (prompt + AJAX)

DELETE — удаление с подтверждением

AJAX без перезагрузки — мгновенное редактирование

AntiForgeryToken — защита от CSRF

База данных

MySQL / MariaDB

Технологический стек 

Backend: ASP.NET Core Razor Pages (.NET 10)

ORM: Entity Framework Core 10 (MySQL)

База данных: MySQL/MariaDB

Frontend: Bootstrap 5 + Font Awesome 6 + JavaScript

AJAX: Fetch API + AntiForgeryToken

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