# ADO.NET — учебная шпаргалка

Учебный проект демонстрирует работу с базами данных из приложения **C# WinForms** с использованием **ADO.NET** и **SQL Server**.

Материал разделён на шесть форм:

| Форма | Тема | Основные понятия |
|---|---|---|
| 1 | Фабрика подключения | `DbProviderFactory`, `DbConnection`, `DbDataAdapter`, `App.config` |
| 2 | Управление данными в `DataSet` | `DataSet`, `DataTable`, `DataViewManager`, `RowFilter`, `Sort`, `SqlCommandBuilder` |
| 3 | Транзакции | `SqlTransaction`, `BeginTransaction()`, `Commit()`, `Rollback()` |
| 4 | Асинхронный запрос и callback | `BeginExecuteReader()`, `EndExecuteReader()`, `AsyncCallback`, `Invoke()` |
| 5 | Асинхронный запрос и ожидание | `IAsyncResult`, `WaitHandle`, `WaitOne()`, `IsCompleted` |
| 6 | Асинхронный код через `async` / `await` | `Task`, `OpenAsync()`, `ExecuteReaderAsync()`, `ReadAsync()` |

> **Важно о версии API.** Формы 4 и 5 используют устаревший асинхронный шаблон Begin/End API. В современном коде обычно предпочитают `async`/`await` и асинхронные методы провайдера. Наличие и поддержка отдельных методов зависят от версии .NET и конкретного провайдера.

---

## Общие основы ADO.NET

ADO.NET — набор классов .NET для подключения к источникам данных, выполнения SQL-команд и чтения или изменения данных.

### Основные объекты

- **Connection** — соединение с базой данных.
- **Command** — SQL-команда или вызов хранимой процедуры.
- **DataReader** — последовательное чтение результатов запроса; обычно работает в режиме чтения вперёд.
- **DataAdapter** — посредник между БД и отключёнными объектами данных (`DataSet` / `DataTable`).
- **DataTable** — таблица в памяти.
- **DataSet** — набор таблиц в памяти, потенциально со связями и ограничениями.
- **Transaction** — группа операций, которые должны завершиться согласованно.
- **Provider** — реализация ADO.NET для конкретной СУБД.

### Типовой порядок работы

1. Получить строку подключения.
2. Создать соединение.
3. Открыть соединение.
4. Создать команду и задать SQL.
5. Выполнить команду подходящим методом.
6. Обработать результат.
7. Закрыть или освободить ресурсы.

По возможности используйте `using` для объектов, реализующих `IDisposable`, и параметризованные SQL-команды для пользовательского ввода.

### `ExecuteReader`, `ExecuteNonQuery`, `ExecuteScalar`

- `ExecuteReader()` — возвращает строки результата запроса.
- `ExecuteNonQuery()` — выполняет команды без чтения таблицы результатов; обычно применяется для `INSERT`, `UPDATE`, `DELETE` и DDL. Возвращаемое число затронутых строк зависит от команды и провайдера.
- `ExecuteScalar()` — возвращает первое значение первой строки результата, например `COUNT(*)`.

```csharp
// Получение строк
using (SqlDataReader reader = command.ExecuteReader())
{
    while (reader.Read())
        Console.WriteLine(reader["Title"]);
}

// Изменение данных
int affected = command.ExecuteNonQuery();

// Одно значение
object value = command.ExecuteScalar();
```

---

# Форма № 1. Фабрика подключения

## Цель

Показать, как работать с поставщиками ADO.NET через общие базовые классы, не привязывая весь код к одному конкретному провайдеру.

## Теория

### `DbProviderFactory`

Фабрика создаёт объекты, необходимые для работы с конкретным провайдером:

- `CreateConnection()` — соединение;
- `CreateCommand()` — команда;
- `CreateDataAdapter()` — адаптер.

### `DbConnection`

Базовый класс для соединения. Конкретный провайдер реализует его для своей СУБД.

### `DbProviderFactories`

- `GetFactoryClasses()` — возвращает таблицу зарегистрированных провайдеров.
- `GetFactory(invariantName)` — получает фабрику по имени провайдера.

Имя провайдера — это его invariant name, а не произвольное название СУБД. Регистрация и доступность провайдеров зависят от используемого .NET и способа установки пакета.

### `App.config` и строка подключения

Строки подключения можно хранить в конфигурации приложения, чтобы не встраивать их в код.

Пример для SQL Server:

```xml
<configuration>
  <connectionStrings>
    <add name="BooksDb"
         providerName="System.Data.SqlClient"
         connectionString="Data Source=.;Initial Catalog=BooksDb;Integrated Security=True;TrustServerCertificate=True" />
  </connectionStrings>
</configuration>
```

Код проекта ищет строку по `ProviderName`. Если у приложения несколько строк с одним `providerName`, текущий метод возвращает первую найденную. Надёжнее искать по имени записи (`name`), если приложение использует несколько подключений к одной СУБД.

`ConfigurationManager` в зависимости от типа проекта и версии .NET может потребовать соответствующую сборку или пакет конфигурации.

### Заполнение `DataGridView`

```csharp
DbDataAdapter adapter = factory.CreateDataAdapter();
adapter.SelectCommand = connection.CreateCommand();
adapter.SelectCommand.CommandText = "SELECT * FROM Books";

DataTable table = new DataTable();
adapter.Fill(table);

dataGridView.DataSource = table;
```

`Fill()` выполняет запрос и загружает результат в таблицу в памяти. Для `SELECT` соединение может управляться адаптером автоматически, если оно изначально закрыто; явное управление соединением тоже допустимо.

## Что демонстрирует код формы

1. Получение списка доступных провайдеров.
2. Выбор фабрики по `InvariantName`.
3. Создание универсального соединения.
4. Получение строки подключения из конфигурации.
5. Выполнение `SELECT` через `DbDataAdapter`.
6. Отображение результата в `DataGridView`.

## Короткий пример для MySQL

Установите совместимый пакет, например `MySql.Data`, и используйте провайдер MySQL. Для актуальных проектов часто применяется `MySqlConnector`; API и регистрация фабрики зависят от выбранного пакета.

```csharp
using MySql.Data.MySqlClient;

string connectionString =
    "Server=localhost;Database=books_db;User ID=root;Password=your_password;";

using var connection = new MySqlConnection(connectionString);
using var command = new MySqlCommand("SELECT * FROM Books", connection);
using var adapter = new MySqlDataAdapter(command);

var table = new DataTable();
adapter.Fill(table);
dataGridView.DataSource = table;
```

Указанный пример использует конкретный MySQL-провайдер, а не универсальную фабрику. Пароль и другие секреты не следует хранить в публичном репозитории.

---

# Форма № 2. Управление данными в `DataSet`

## Цель

Загрузить данные из SQL Server в память, а затем фильтровать и сортировать их без повторного SQL-запроса.

## Теория

### `DataSet`

`DataSet` — отключённое представление данных в памяти. Он может содержать несколько `DataTable`, связи и ограничения. После загрузки данные можно обрабатывать без постоянного открытого соединения.

### `DataTable`

Представляет одну таблицу в памяти. В примере результат загружается в таблицу с именем `Books`:

```csharp
DataSet dataSet = new DataSet();
adapter.Fill(dataSet, "Books");
```

### `DataViewManager`, `DataView`, `RowFilter`, `Sort`

- `DataView` предоставляет представление строк таблицы с фильтрацией и сортировкой.
- `DataViewManager` позволяет настраивать представления для таблиц в `DataSet`.
- `RowFilter` задаёт условие отбора строк в синтаксисе выражений `DataView`, а не полноценный SQL-запрос.
- `Sort` задаёт порядок сортировки.

```csharp
DataViewManager manager = new DataViewManager(dataSet);
manager.DataViewSettings["Books"].RowFilter = "id < 100";
manager.DataViewSettings["Books"].Sort = "Title ASC";

DataView view = manager.CreateDataView(dataSet.Tables["Books"]);
dataGridView.DataSource = view;
```

`ASC` — по возрастанию, `DESC` — по убыванию.

### `SqlDataAdapter` и `SqlCommandBuilder`

- `SqlDataAdapter` загружает данные из SQL Server и может применять изменения из памяти к БД.
- `SqlCommandBuilder` автоматически генерирует команды `INSERT`, `UPDATE` и `DELETE` для простых запросов, если провайдер может однозначно определить таблицу и ключи.

**Важное уточнение:** в текущем коде `SqlCommandBuilder` создаётся, но операции изменения данных (`Update`) не вызываются. Поэтому форма демонстрирует загрузку, фильтрацию и сортировку, а не полный цикл сохранения изменений в БД.

## Важные отличия

- Фильтрация через `RowFilter` выполняется в памяти.
- Фильтрация в SQL через `WHERE` выполняется сервером.
- Для большого набора данных часто выгоднее отбирать нужные строки на стороне БД.
- Изменение `DataView` не означает автоматического сохранения изменений в БД.

## Короткий пример для MySQL

Вместо `SqlDataAdapter` используйте адаптер выбранного MySQL-провайдера, например `MySqlDataAdapter` из `MySql.Data`.

```csharp
using MySql.Data.MySqlClient;

var dataSet = new DataSet();

using var adapter = new MySqlDataAdapter(
    "SELECT id, Title FROM Books", connectionString);

adapter.Fill(dataSet, "Books");

var view = new DataView(dataSet.Tables["Books"]);
view.RowFilter = "id < 100";
view.Sort = "Title ASC";

dataGridView.DataSource = view;
```

Синтаксис `RowFilter` относится к .NET, поэтому для такого простого фильтра он не меняется при переходе с SQL Server на MySQL.

---

# Форма № 3. Транзакции

## Цель

Показать, как объединять несколько SQL-операций в одну логическую операцию: либо фиксируются все изменения, либо выполняется откат.

## Теория

### Что такое транзакция

Транзакция — группа операций, для которой требуется согласованный результат.

Основные свойства ACID:

- **Atomicity (атомарность)** — все операции выполняются как единое целое.
- **Consistency (согласованность)** — данные сохраняют ограничения и правила целостности.
- **Isolation (изолированность)** — параллельные транзакции не должны недопустимо мешать друг другу; конкретный уровень изоляции влияет на поведение.
- **Durability (долговечность)** — подтверждённые изменения сохраняются согласно гарантиям СУБД.

### Основные методы

- `BeginTransaction()` — начинает транзакцию.
- `Commit()` — фиксирует изменения.
- `Rollback()` — отменяет изменения транзакции.

Команды должны выполняться в рамках той же транзакции, например через `command.Transaction = transaction`.

## Логика формы

1. Открыть соединение.
2. Начать транзакцию.
3. Создать таблицу.
4. Выполнить несколько `INSERT`.
5. При успехе вызвать `Commit()`.
6. При ошибке вызвать `Rollback()`.

Если одна из команд завершится ошибкой до `Commit()`, транзакция откатит поддерживаемые ею изменения. DDL-операции, такие как создание таблицы, поддерживаются транзакциями в SQL Server, но детали зависят от СУБД.

## Что стоит исправить в учебном коде

- В `catch` вызов `tran.Rollback()` может сам вызвать исключение, если транзакция не была создана или уже завершилась. Проверяйте `tran != null` и обрабатывайте ошибку отката.
- Во втором `INSERT` комментарий сообщает о предполагаемой ошибке, но сам текст SQL похож на корректный. Чтобы показать откат, нужно действительно вызвать ошибку.
- Имя таблицы подставляется в SQL через интерполяцию. Имена таблиц нельзя защитить обычными SQL-параметрами, поэтому используйте фиксированное имя или проверяйте его по разрешённому списку.
- Соединение и транзакцию лучше освобождать через `using`, если архитектура проекта это позволяет.

## Короткий пример для MySQL

В MySQL используйте транзакцию соединения MySQL. Таблицы должны использовать транзакционный движок, обычно InnoDB.

```csharp
using var connection = new MySqlConnection(connectionString);
connection.Open();

using var transaction = connection.BeginTransaction();

try
{
    using var command = connection.CreateCommand();
    command.Transaction = transaction;
    command.CommandText =
        "INSERT INTO Books (Title) VALUES (@title)";
    command.Parameters.AddWithValue("@title", "Example");
    command.ExecuteNonQuery();

    transaction.Commit();
}
catch
{
    transaction.Rollback();
    throw;
}
```

Синтаксис SQL, типы данных и поддержка DDL в транзакциях могут отличаться от SQL Server.

---

# Форма № 4. Асинхронная работа через Begin/End и callback

## Цель

Показать старый асинхронный шаблон ADO.NET: запуск операции, а затем получение результата в callback-методе.

## Теория

### `BeginExecuteReader()` и `EndExecuteReader()`

- `BeginExecuteReader()` запускает асинхронное выполнение запроса и возвращает `IAsyncResult`.
- `EndExecuteReader()` получает итоговый результат операции и возвращает `SqlDataReader`.
- `AsyncCallback` — делегат метода, который вызывается при завершении операции.
- `IAsyncResult.AsyncState` позволяет передать объект между запуском и callback.

Важное правило: завершать операцию через `EndExecuteReader()` нужно у того же объекта `SqlCommand`, который запускал `BeginExecuteReader()`.

### Зачем в строке подключения указан асинхронный режим

В примере используется параметр:

```text
Asynchronous Processing=true
```

Он относится к старому провайдеру `System.Data.SqlClient` и старому асинхронному API. Для современных провайдеров и версий .NET настройки и требования могут отличаться. Не добавляйте этот параметр автоматически без проверки документации используемого провайдера.

### `WAITFOR DELAY`

```sql
WAITFOR DELAY '00:00:05';
SELECT * FROM Books;
```

`WAITFOR DELAY` — команда SQL Server, которая задерживает выполнение. В учебном примере она нужна для имитации долгого запроса. В MySQL этот синтаксис не работает.

## Как работает форма

1. Открывает соединение.
2. Запускает `BeginExecuteReader()`.
3. Передаёт `GetDataCallback` в качестве callback.
4. Callback вызывает `EndExecuteReader()` и переносит результаты в `DataTable`.
5. Результат отображается в `DataGridView`.

### Почему нужен `Invoke()`

Элементы WinForms привязаны к UI-потоку. Нельзя безопасно изменять `DataGridView` из произвольного рабочего потока.

```csharp
private void DgvAction()
{
    if (dataGridView.InvokeRequired)
    {
        dataGridView.Invoke(new Action(DgvAction));
        return;
    }

    dataGridView.DataSource = _dataTable;
}
```

- `InvokeRequired` проверяет, нужно ли переключиться на поток, которому принадлежит элемент.
- `Invoke()` выполняет делегат в UI-потоке синхронно.
- `BeginInvoke()` планирует вызов в UI-потоке асинхронно.

## На что обратить внимание

- Закрывайте `SqlDataReader` и соединение даже при исключениях.
- В callback нужно корректно обработать ошибки `EndExecuteReader()`.
- Поле `_dataTable` может быть небезопасным при одновременных запросах из нескольких запусков.
- Не вызывайте блокирующие UI-операции из callback без необходимости.
- `MessageBox.Show()` из фонового callback не является хорошим способом построения полноценной обработки ошибок в большом приложении.

## Аналог для MySQL

Методы `BeginExecuteReader()` / `EndExecuteReader()` не следует считать универсальными для всех MySQL-провайдеров. Современный подход — использовать асинхронные методы провайдера:

```csharp
using var connection = new MySqlConnection(connectionString);
await connection.OpenAsync();

using var command = new MySqlCommand(
    "SELECT id, Title FROM Books", connection);

using var reader = await command.ExecuteReaderAsync();
```

Такой пример использует `async`/`await`, который разбирается в форме № 6.

---

# Форма № 5. Асинхронная работа через `WaitHandle` и опрос

## Цель

Показать, как проверить завершение асинхронной операции через объект ожидания или периодически опрашивать состояние операции.

## Теория

### `IAsyncResult`

Объект результата асинхронной операции предоставляет информацию о её состоянии, включая:

- `IsCompleted` — завершилась ли операция;
- `AsyncWaitHandle` — объект синхронизации, сигнализирующий о завершении операции;
- `AsyncState` — переданное состояние, если оно использовалось.

### `WaitHandle.WaitOne()`

```csharp
IAsyncResult result = command.BeginExecuteReader();
WaitHandle handle = result.AsyncWaitHandle;

if (handle.WaitOne(10000))
{
    // Операция завершилась за время ожидания.
    using var reader = command.EndExecuteReader(result);
}
else
{
    // Истекло время ожидания.
}
```

`WaitOne(10000)` блокирует вызывающий поток максимум на 10 секунд. Если это UI-поток WinForms, интерфейс на это время перестанет отвечать. Поэтому этот подход полезен для изучения синхронизации, но обычно не подходит для отзывчивого интерфейса.

Тайм-аут ожидания не обязательно отменяет сам SQL-запрос. Для управления выполнением запроса изучите `CommandTimeout` и механизмы отмены, поддерживаемые провайдером.

### `WaitAll()` и `WaitAny()`

- `WaitAll()` ждёт завершения всех объектов ожидания.
- `WaitAny()` ждёт завершения любого из объектов ожидания.

Доступность и ограничения зависят от типа объектов ожидания и среды выполнения.

### Опрос состояния

В примере:

```csharp
while (!result.IsCompleted)
{
    Console.WriteLine("Waiting...");
}
```

Это активный опрос (busy waiting): цикл постоянно проверяет состояние и может расходовать процессорное время. В WinForms вывод в консоль обычно не нужен. Для учебного сравнения можно проверять состояние с паузой или использовать ожидание/асинхронные API.

## Короткий пример для MySQL

Если выбранный MySQL-провайдер не предоставляет Begin/End API, не пытайтесь механически перенести код формы. Используйте асинхронные методы, например `ExecuteReaderAsync()`, либо отдельный механизм ожидания, подходящий выбранной библиотеке.

```csharp
using var connection = new MySqlConnection(connectionString);
await connection.OpenAsync();

using var command = new MySqlCommand(
    "SELECT id, Title FROM Books", connection);

using var reader = await command.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    Console.WriteLine(reader["Title"]);
}
```

---

# Форма № 6. Асинхронная работа через `Task`, `async` и `await`

## Цель

Показать современный и обычно предпочтительный способ асинхронного взаимодействия с БД в приложении WinForms.

## Теория

### `Task` и `Task<T>`

- `Task` представляет асинхронную операцию без возвращаемого значения.
- `Task<T>` представляет асинхронную операцию, возвращающую результат типа `T`.

### `async` и `await`

- `async` разрешает использовать `await` внутри метода.
- `await` ожидает завершения асинхронной операции, не блокируя текущий поток на всё время ожидания.
- В UI-коде WinForms продолжение обычно возвращается в UI-контекст, поэтому после `await` можно обновить элементы формы, если контекст не был намеренно изменён.

`async` само по себе не запускает метод в отдельном потоке. Асинхронные операции ввода-вывода позволяют не блокировать поток, пока ждём ответ БД.

### Асинхронные методы ADO.NET

- `OpenAsync()` — асинхронное открытие соединения.
- `ExecuteReaderAsync()` — асинхронный запуск запроса с чтением строк.
- `ReadAsync()` — асинхронное чтение следующей строки.
- `GetFieldValueAsync<T>()` — асинхронное получение значения поля, если поддерживается провайдером.

## Логика формы

1. При загрузке формы создаётся фабрика SQL Server и получается строка подключения.
2. Обработчик кнопки вызывает `await LoadDataAsync()`.
3. Метод открывает соединение асинхронно.
4. Запрос выполняется через `ExecuteReaderAsync()`.
5. Результат читается по строкам и записывается в `DataTable`.
6. Таблица связывается с `DataGridView`.
7. Соединение закрывается в `finally`.

### Что стоит улучшить

- Использовать `using` / `await using` там, где это поддерживает тип и целевая версия .NET.
- Проверять наличие строки подключения до запуска запроса.
- Не объединять пользовательский SQL с `WAITFOR DELAY` в рабочем приложении.
- Пользовательские значения передавать через SQL-параметры.
- Если запрос должен поддерживать отмену, рассмотреть `CancellationToken` и методы провайдера с поддержкой отмены.
- При работе с несколькими запросами не переиспользовать одновременно одно соединение без учёта ограничений провайдера.

## Короткий пример для MySQL

```csharp
using MySql.Data.MySqlClient;

private async Task LoadBooksAsync()
{
    const string sql = "SELECT id, Title FROM Books";

    await using var connection =
        new MySqlConnection(connectionString);

    await connection.OpenAsync();

    await using var command =
        new MySqlCommand(sql, connection);

    var table = new DataTable();

    await using var reader = await command.ExecuteReaderAsync();
    table.Load(reader);

    dataGridView.DataSource = table;
}

private async void buttonLoad_Click(object sender, EventArgs e)
{
    try
    {
        await LoadBooksAsync();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}
```

Пример предполагает современную версию .NET и MySQL-провайдер, поддерживающий указанные асинхронные методы. Для старых целевых версий может потребоваться заменить `await using` на `using`.

---

# SQL Server и MySQL: краткие различия

| Задача | SQL Server | MySQL |
|---|---|---|
| Провайдер | `Microsoft.Data.SqlClient` или `System.Data.SqlClient` | `MySql.Data` или `MySqlConnector` |
| Тип соединения | `SqlConnection` | `MySqlConnection` |
| Команда | `SqlCommand` | `MySqlCommand` |
| Адаптер | `SqlDataAdapter` | `MySqlDataAdapter` в `MySql.Data` |
| Параметр в запросе | Обычно `@name` | Обычно `@name` |
| Задержка для демонстрации | `WAITFOR DELAY '00:00:05'` | `SELECT SLEEP(5)` |
| Автоинкремент | `IDENTITY(1,1)` | `AUTO_INCREMENT` |
| Ограничение строк | `TOP (10)` | `LIMIT 10` |
| Типичная транзакционная таблица | Зависит от механизма БД | InnoDB |

Пример параметризованного запроса, применимый по смыслу к обоим провайдерам:

```csharp
command.CommandText =
    "SELECT * FROM Books WHERE id = @id";
command.Parameters.AddWithValue("@id", bookId);
```

Типизированные параметры (`Add` с явным типом) обычно предпочтительнее `AddWithValue`, особенно когда важны тип данных, точность или производительность.

---

# Рекомендации по качеству кода

1. **Не склеивайте пользовательский ввод с SQL.** Используйте параметры.
2. **Освобождайте ресурсы.** Применяйте `using` для соединений, команд, транзакций, адаптеров и reader там, где это подходит.
3. **Обрабатывайте ошибки на подходящем уровне.** Не скрывайте исходную причину и не допускайте, чтобы ошибка `Rollback()` маскировала основную ошибку.
4. **Не блокируйте UI-поток надолго.** Для асинхронного ввода-вывода обычно используйте `async`/`await`.
5. **Разделяйте ответственность.** Код доступа к данным лучше постепенно вынести из обработчиков кнопок в отдельный класс или слой.
6. **Не храните секреты в репозитории.** Не публикуйте реальные пароли, токены и чувствительные строки подключения.
7. **Не путайте фильтрацию в памяти и фильтрацию в БД.** `DataView.RowFilter` не отправляет новый SQL-запрос.
8. **Учитывайте различия СУБД.** SQL Server и MySQL имеют различия в DDL, функциях, типах данных, транзакциях и API провайдера.

---

# Итоговая шпаргалка

- **Форма 1:** фабрика позволяет создавать объекты ADO.NET через общий API; конфигурация хранит строки подключения.
- **Форма 2:** `DataSet` хранит данные в памяти; `DataView` и `DataViewManager` фильтруют и сортируют их локально.
- **Форма 3:** транзакция группирует операции; `Commit()` фиксирует, `Rollback()` откатывает.
- **Форма 4:** Begin/End запускает асинхронную операцию и завершает её в callback; WinForms-контролы нужно обновлять из UI-потока.
- **Форма 5:** `WaitHandle` и `IsCompleted` помогают ожидать или проверять завершение, но блокирующее ожидание может заморозить интерфейс.
- **Форма 6:** `Task`, `async` и `await` — предпочтительный современный шаблон асинхронного доступа к данным.
