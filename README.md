# 📱 DNS Shop Mobile Autotests

[![.NET Build](https://github.com/lolipusechka/appium-csharp-dns-shop/actions/workflows/build.yml/badge.svg)](https://github.com/lolipusechka/appium-csharp-dns-shop/actions/workflows/build.yml)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)

Автоматизированный фреймворк для UI-тестирования мобильного приложения [DNS Shop](https://www.dns-shop.ru/) (Android). Проект демонстрирует современные практики разработки на **C#** с использованием **Appium**, **NUnit** и **Page Object Model**.

---

## 🎯 О проекте

Этот репозиторий содержит production-ready фреймворк для автоматизации тестирования мобильного e-commerce приложения. Проект построен на принципах чистой архитектуры, что позволяет легко масштабировать тестовое покрытие и поддерживать код в команде.

### Ключевые особенности:
- ✅ **Модульная архитектура** — разделение на слои (Framework, Page Objects, Tests)
- ✅ **Централизованная конфигурация** — управление через `appsettings.json`
- ✅ **CI/CD готовность** — автоматическая сборка через GitHub Actions
- ✅ **Параметризованные тесты** — data-driven подход с использованием NUnit
- ✅ **Расширяемость** — легко добавить новые экраны и тестовые сценарии

---

## 🛠 Технологический стек

| Компонент | Технология | Назначение |
|-----------|-----------|-----------|
| **Язык** | C# 12 / .NET 8.0 | Основной язык разработки |
| **Автоматизация** | Appium + UiAutomator2 | Управление Android-устройством |
| **Тестовый раннер** | NUnit 3.14 | Запуск и параметризация тестов |
| **Логирование** | NLog | Структурированные логи выполнения |
| **Конфигурация** | Microsoft.Extensions.Configuration | Чтение JSON-конфигов |
| **CI/CD** | GitHub Actions | Автоматическая сборка проекта |

---

## 🏗 Архитектура проекта

```
appium-csharp-dns-shop/
├── 📦 AppiumFramework/         # Ядро фреймворка (переиспользуемое)
│   ├── Core/
│   │   ├── Base/               # Базовые классы (BaseTest, BaseScreen)
│   │   ├── Config/             # Модели конфигурации
│   │   ├── Driver/             # Управление AppiumDriver
│   │   ├── Elements/           # Обертки над UI-элементами
│   │   └── Helpers/            # Утилиты (скриншоты, алерты)
│   ├── appsettings.json        # Конфигурация приложения
│   └── NLog.config             # Настройки логирования
│
├── 📱 DnsGui/                   # Слой Page Objects
│   ├── Screens/                # Экраны приложения
│   │   ├── CatalogScreens/     # Каталог товаров
│   │   ├── CartScreen.cs       # Корзина
│   │   ├── ProductScreen.cs    # Карточка товара
│   │   └── ...                 # Другие экраны
│   └── Helpers/                # Специфичные хелперы для DNS
│
├── 🧪 AutotestDnsGui/           # Слой тестов
│   ├── Autotests/              # E2E тестовые сценарии
│   ├── TestDataHelpers/        # Генераторы тестовых данных
│   └── appsettings.json        # Переопределение конфига для тестов
│
└── ⚙️ .github/workflows/        # CI/CD пайплайны
    └── build.yml               # Автоматическая сборка
```

### Принципы архитектуры:

1. **Dependency Inversion** — тесты зависят от абстракций (Page Objects), а не от конкретной реализации
2. **Single Responsibility** — каждый класс отвечает только за свой экран/элемент
3. **DRY (Don't Repeat Yourself)** — общая логика вынесена в `AppiumFramework`
4. **Configuration over Code** — параметры вынесены в JSON, а не захардкожены

---

## 🚀 Быстрый старт

### Требования:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Appium Server](https://appium.io/) (запущен на `localhost:4723`)
- Android-эмулятор или реальное устройство
- Установленная переменная окружения `ANDROID_HOME`

### Установка и запуск:

```bash
# 1. Клонируйте репозиторий
git clone https://github.com/lolipusechka/appium-csharp-dns-shop.git
cd appium-csharp-dns-shop

# 2. Восстановите зависимости
dotnet restore

# 3. Соберите проект
dotnet build

# 4. Запустите тесты
dotnet test AutotestDnsGui
```

### Конфигурация:

Основные параметры настраиваются в `AppiumFramework/appsettings.json`:

```json
{
  "AppiumFramework": {
    "URL": "http://localhost:4723/",
    "AutoGrantPermissions": true,
    "TimeOut": 60000
  },
  "DnsGui": {
    "Package": "ru.dns.shop.android"
  },
  "AutotestDnsGui": {
    "Cities": ["Санкт-Петербург", "Красноярск", "Минусинск"],
    "MemoryCardCapacity": [128, 512]
  }
}
```

---

## 🧪 Реализованные тестовые сценарии

### 1. Проверка выбора города

**Файл:** `ChooseCityTest.cs`

Тестирует функциональность выбора города при первом запуске приложения:

1. Запуск приложения и отображение экрана выбора города
2. Выбор города из списка (параметризация через appsettings.json)
3. Проверка, что город корректно сохранился в профиле
4. Проверка отображения контента для выбранного города (цены, наличие)

**Пример параметризации:**

```csharp
[Test]
public void RunTest([ValueSource(typeof(TestDataProvider), 
    nameof(TestDataProvider.GetCities))] string city)
{
    // Тест запускается для каждого города из конфигурации
    // 3 города = 3 тестовых прогона
}
```

**Тестовые данные из appsettings.json:**

```json
"Cities": ["Санкт-Петербург", "Красноярск", "Минусинск"]
```

---

### 2. E2E: Добавление товара в корзину

**Файл:** `TryAddMemoryCardToCartAndDeleteFrom.cs`

Тестирует полный пользовательский путь покупки карты памяти:

1. Выбор города и пропуск авторизации
2. Выдача разрешений ОС
3. Навигация: Каталог → Аксессуары → Карты памяти
4. Применение фильтров (емкость 128/512 ГБ)
5. Добавление товара в корзину
6. Проверка итоговой стоимости
7. Удаление товара с проверкой уведомления (SnackBar)

**Пример параметризации:**

```csharp
[Test]
public void RunTest([ValueSource(typeof(TestDataProvider), 
    nameof(TestDataProvider.GetMemoryCardCapacity))] int memoryCardCapacity)
{
    // Тест запускается для каждой емкости карты памяти
    // 2 емкости (128, 512) = 2 тестовых прогона
}
```

**Тестовые данные из appsettings.json:**

```json
"MemoryCardCapacity": [128, 512]
```

---

### 3. Network Testing: Режим полёта

**Файл:** `AirPlaneTest.cs`

Проверяет корректную обработку отсутствия интернета:

1. Включение режима полёта на устройстве
2. Запуск приложения
3. Проверка экрана ошибки "Нет подключения к интернету"
4. Отключение режима полёта (восстановление исходного состояния)

---

## 💡 Ключевые паттерны и подходы

### Page Object Model (POM)

Каждый экран представлен отдельным классом с инкапсулированной логикой:

```csharp
public class CartScreen : BaseDnsScreen
{
    protected override BaseElement UniqueElement => new TextView("Корзина", By.XPath("//*[@text=\"Корзина\"]"));

    public BottomMenu BottomMenu => _bottomMenu;

    private string _productNameLocator = "//*[@resource-id=\"ru.dns.shop.android:id/product_title_text\" and contains(@text, \"{0}\")]";

    private Button _productBtn = new("Продукт", By.XPath("//*[@resource-id=\"ru.dns.shop.android:id/product_title_text\"]"));
        
    ...

    public void AssertIsCartEmpty()
    {
        AssertHelper.AssertIsTrue(!(_productBtn.IsElementDisplayed()), "Корзина пуста");
    }

    public void AssertIsEmptyContentActionBtnIsDisplayed()
    {
        AssertHelper.AssertIsTrue(_emptyContentActionBtn.IsElementDisplayed(), "Кнопка 'Перейти в каталог' отображается");
    }

    ...
}
```

### Data-Driven Testing

Тестовые данные вынесены в конфигурацию и могут быть переопределены без изменения кода:

```csharp
public static IEnumerable<string> GetCities()
{
    var config = ConfigManager.GetSection<AutotestDnsGuiTestData>("AutotestDnsGui");
    return config.Cities;
}
```

---

## 🔄 CI/CD Pipeline

Проект настроен на автоматическую сборку через **GitHub Actions**:

```yaml
name: .NET Build

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
```

**Статус сборки:** [![.NET Build](https://github.com/lolipusechka/appium-csharp-dns-shop/actions/workflows/build.yml/badge.svg)](https://github.com/lolipusechka/appium-csharp-dns-shop/actions/workflows/build.yml)

---

## 📈 Возможности расширения

### Добавить новый экран:
1. Создать класс в `DnsGui/Screens/`
2. Наследовать от `BaseDnsScreen`
3. Описать элементы и методы взаимодействия

### Добавить новый тест:
1. Создать класс в `AutotestDnsGui/Autotests/`
2. Наследовать от `BaseTest`
3. Использовать Page Objects для сценария

### Добавить тестовые данные:
1. Расширить `appsettings.json`
2. Создать модель в `AppiumFramework/Core/Config/`
3. Использовать `ConfigManager.GetSection<T>()`

---

## 👤 Автор

**lolipusechka**  
QA Automation Engineer / SDET  
📫 GitHub: [@lolipusechka](https://github.com/lolipusechka)