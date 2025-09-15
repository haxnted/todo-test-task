# Тестовое задание для МТС. Система управления задачами (аналог Jira)

## Основные требования

### Задачи
- Поддержка CRUD-операций  
- Поля задачи:  
  - Автор  
  - Исполнитель  
  - Статус: `New`, `InProgress`, `Done`  
  - Приоритет: `Low`, `Medium`, `High`  
- Вложенности:  
  - Подзадачи  
  - Связи между задачами (`related to`)  

### Пользователи
- Авторизация: простой вариант на основе **JWT**  
- Регистрация и хранение пользователей **не требуется**  

### База данных
- Использование **EF Core**  
- СУБД: **MSSQL** или **PostgreSQL** (на выбор)  
- Миграции через **EF Core**  

## Технические требования
- **.NET 8**, **ASP.NET Core**  
- **Dependency Injection**  
- Поддержка **CQRS** (разделение команд и запросов)  
- **Swagger** для документации API  
- Логирование (можно стандартное)  
- Опционально: **docker-compose** для запуска  

## Будет плюсом
- Unit-тесты бизнес-логики  
- Архитектура в стиле **Clean Architecture** или хотя бы **Onion Architecture**  

Итог после тз:
.NET 8, Clean Architecture, Domain-Driven Design (DDD)
Dependency Injection (DI), CQRS, Specifications
Entity Framework (EF), Migrations, CRUD операции, Unit Tests (покрытие 96%)
Docker & Docker Compose, Swagger / OpenAPI
Логирование: консоль, Elasticsearch, Kibana
Wolverine
