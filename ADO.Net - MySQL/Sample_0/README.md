# Authors CRUD — WinForms + MySQL

Простое учебное приложение на C# (WinForms) для работы с базой данных MySQL. Реализует базовые операции CRUD над таблицей Authors (авторы).

---

## Что делает приложение

- Подключается к базе данных MySQL
- Загружает всех авторов в таблицу DataGridView
- Добавляет нового автора
- Обновляет данные выбранного автора
- Удаляет выбранного автора
- Корректно закрывает соединение при выходе

---

## Требования

- Windows
- Visual Studio 2019 / 2022 (любая редакция, включая Community)
- .NET Framework 4.7.2+ или .NET 6/7/8 (Windows)
- Установленный MySQL Server (локально или удалённо)
- Доступ к серверу MySQL (логин и пароль)

---

## Шаг 1. Установка MySQL Server

1. Скачай установщик с официального сайта MySQL (MySQL Installer for Windows).
2. При установке выбери тип Setup Type: Developer Default или Custom.
3. Обязательно запомни:
   - порт (по умолчанию 3306),
   - логин администратора (обычно root),
   - пароль администратора.
4. После установки убедись, что служба MySQL запущена (можно проверить в services.msc или через MySQL Workbench).

---

## Шаг 2. Создание базы данных и таблицы

Открой MySQL Workbench (или командную строку MySQL) и выполни скрипт:

- Создай базу данных LibraryDB
- Создай таблицу Authors со столбцами:
  - id — целое, автоинкремент, первичный ключ
  - firstName — строка, не NULL
  - lastName — строка, не NULL

Скрипт создания таблицы приведён в разделе «SQL-скрипт» ниже.

---

## Шаг 3. Создание проекта в Visual Studio

1. Открой Visual Studio.
2. File → New → Project.
3. Выбери шаблон Windows Forms App (.NET Framework) или Windows Forms App (.NET).
4. Задай имя проекта, например SimpleMySQLApp.
5. Нажми Create.

---

## Шаг 4. Установка NuGet-пакета MySQL

1. В Solution Explorer щёлкни правой кнопкой по проекту.
2. Выбери Manage NuGet Packages.
3. Перейди на вкладку Browse.
4. Введи в поиске MySql.Data (или MySqlConnector).
5. Установи последнюю стабильную версию.
6. Прими лицензионное соглашение.

Проверка: после установки в разделе Dependencies → Packages должен появиться MySql.Data.

---

## Шаг 5. Разметка формы

Перетащи на форму Form1 следующие элементы из Toolbox:

- DataGridView — имя dgvAuthors
- TextBox (для имени) — имя txtFirstName
- TextBox (для фамилии) — имя txtLastName
- Button (Загрузить) — имя btnLoad
- Button (Добавить) — имя btnInsert
- Button (Обновить) — имя btnUpdate
- Button (Удалить) — имя btnDelete

Совет: имена задаются в окне Properties в поле (Name).

---

## Шаг 6. Настройка строки подключения

В коде используется строка подключения вида:

Server=localhost;Database=LibraryDB;Uid=root;Pwd=ВАШ_ПАРОЛЬ;

Параметры:
- Server — адрес сервера (localhost, если БД на этой же машине)
- Database — имя базы данных (LibraryDB)
- Uid — имя пользователя MySQL
- Pwd — пароль пользователя MySQL
- Port — указывать, только если порт не 3306 (по умолчанию не нужен)

Важно: для учебного примера строка подключения хранится прямо в коде. В реальных проектах её хранят в App.config / appsettings.json.

---

## Шаг 7. Запуск приложения

1. Убедись, что MySQL Server запущен.
2. Нажми F5 или кнопку Start в Visual Studio.
3. В открывшемся окне:
   - нажми «Загрузить» — в таблице появятся авторы;
   - заполни поля и нажми «Добавить» — появится новая запись;
   - выдели строку, измени данные, нажми «Обновить»;
   - выдели строку и нажми «Удалить».

---

## SQL-скрипт создания БД и таблицы

CREATE DATABASE LibraryDB;

USE LibraryDB;

CREATE TABLE Authors (
    id        INT AUTO_INCREMENT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName  VARCHAR(50) NOT NULL
);

---

## Частые ошибки и их решения

Ошибка: Authentication method 'caching_sha2_password' is not supported.

Решение: обнови NuGet-пакет MySql.Data до актуальной версии. Если не помогло — в MySQL выполни смену метода аутентификации пользователя на mysql_native_password.

Ошибка: Unable to connect to any of the specified MySQL hosts.

Решение: проверь, что служба MySQL запущена, порт 3306 открыт, и в строке подключения указан верный сервер.

Ошибка: Access denied for user 'root'@'localhost'.

Решение: проверь логин и пароль в строке подключения. Убедись, что у пользователя есть права на базу LibraryDB.

Ошибка: Unknown database 'LibraryDB'.

Решение: создай базу данных скриптом из раздела выше.

Ошибка: Table 'LibraryDB.Authors' doesn't exist.

Решение: выполни скрипт создания таблицы Authors.

Ошибка: Не найден тип или пространство имён MySql.Data.

Решение: убедись, что NuGet-пакет MySql.Data установлен и в файле кода подключён using MySql.Data.MySqlClient.

---

## Рекомендации по безопасности (для студентов)

1. Никогда не склеивай SQL-запросы строками. Используй параметры (@id, @firstName, @lastName) — это защита от SQL-инъекций.
2. Не храни пароль от БД в открытом виде в реальных проектах.
3. Всегда оборачивай работу с БД в try / catch / finally.
4. Закрывай соединение после завершения работы с БД.
5. Пользуйся конструкцией using для Command, Connection, DataAdapter.

---

## Структура проекта

SimpleMySQLApp/
- Form1.cs — код формы: подключение, CRUD, обработчики кнопок
- Form1.Designer.cs — сгенерированная разметка формы
- Program.cs — точка входа
- App.config — конфигурация приложения (опционально)
- packages.config или *.csproj — зависимости NuGet

---

## Полезные ссылки

- Документация MySql.Data: https://dev.mysql.com/doc/connector-net/en/
- MySQL Workbench: https://dev.mysql.com/downloads/workbench/
- Руководство по WinForms: https://learn.microsoft.com/ru-ru/dotnet/desktop/winforms/

---

## Лицензия

Учебный пример. Свободно использовать в образовательных целях.