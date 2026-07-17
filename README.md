# DeveloperLanding API

Backend API для обработки сообщений с контактной формы сайта разработчика.

Сервис принимает обращения пользователей, выполняет валидацию данных, сохраняет заявки, отправляет email-уведомления и выполняет AI-анализ текста сообщения.

Проект реализован на ASP.NET Core Web API с использованием принципов Clean Architecture.

## Используемый стек

- C#
- .NET 10
- ASP.NET Core Web API
- Clean Architecture
- FluentValidation
- Serilog
- Swagger/OpenAPI
- SMTP
- OpenRouter API
- Dependency Injection

## Возможности

### Контактная форма

Реализован REST API endpoint:

```
POST /api/contact
```

Функциональность:

- прием пользовательских обращений;
- валидация входных данных;
- обработка ошибок;
- сохранение заявок;
- отправка email уведомлений;
- логирование запросов.

Пример запроса:

```json
{
  "name": "Ivan Ivanov",
  "phone": "+79999999999",
  "email": "ivan@example.com",
  "comment": "Need help with backend development"
}
```

## AI-анализ сообщений

Для анализа текста используется OpenRouter API.

После получения сообщения AI определяет:

- тональность обращения;
- категорию;
- приоритет.

Пример результата:

```json
{
  "sentiment": "neutral",
  "category": "support",
  "priority": "medium"
}
```

Категории обращений:

- job — предложение работы;
- commercial — коммерческое обращение;
- support — технический вопрос;
- other — другое.

При недоступности внешнего AI-сервиса используется fallback-механизм, позволяющий сохранить работоспособность приложения.

## Email уведомления

После обработки заявки сервис:

- отправляет уведомление владельцу;
- отправляет подтверждение пользователю.

Для отправки используется SMTP.

## Архитектура проекта

Проект разделен на следующие слои:

```
DeveloperLanding
│
├── DeveloperLanding.API
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── DeveloperLanding.Application
│   ├── Interfaces
│   ├── DTOs
│   ├── Services
│   └── Validators
│
└── DeveloperLanding.Infrastructure
    ├── AI
    ├── Email
    ├── Persistence
    └── Options
```

### API Layer

Отвечает за:

- HTTP endpoints;
- middleware;
- взаимодействие с клиентом.

### Application Layer

Содержит:

- бизнес-логику;
- интерфейсы;
- DTO-модели;
- правила валидации.

### Infrastructure Layer

Отвечает за:

- внешние сервисы;
- AI-интеграцию;
- отправку email;
- хранение данных.

## Логирование

Для логирования используется Serilog.

Система записывает:

- HTTP-запросы;
- ошибки;
- обращения к внешним сервисам.

Файлы логов:

```
Logs/api-{date}.log
```

## Rate Limiting

Добавлено ограничение количества запросов к API.

Используется для защиты контактной формы от большого количества повторных обращений.

## Обработка ошибок

Реализован глобальный middleware обработки исключений.

Поддерживаются основные HTTP-статусы:

- 400 Bad Request — ошибка валидации;
- 429 Too Many Requests — превышение лимита запросов;
- 500 Internal Server Error — внутренняя ошибка сервера.

## Запуск проекта

Требования:

- .NET SDK 10

Клонирование проекта:

```bash
git clone https://github.com/<username>/DeveloperLanding.git
```

Запуск:

```bash
dotnet run
```

Swagger документация:

```
https://localhost:7031/swagger
```

## Настройка конфигурации

Для хранения секретных данных используется User Secrets.

Добавление ключа OpenRouter:

```bash
dotnet user-secrets set "OpenRouter:ApiKey" "YOUR_API_KEY"
```

Используемые настройки:

- OpenRouter API Key;
- SMTP параметры.

## Хранение данных

В текущей версии используется JSON-хранилище.

Репозиторий реализован через интерфейс:

```
IContactRepository
```

что позволяет заменить способ хранения данных без изменения бизнес-логики.

## Возможные улучшения

- подключение PostgreSQL;
- добавление Entity Framework Core;
- использование Redis для распределенного Rate Limiting;
- добавление очереди для отправки email;
- создание административной панели.

## Автор

Романов Клим

Стек:
- C#
- ASP.NET Core
- REST API
- Clean Architecture
- AI Integration
