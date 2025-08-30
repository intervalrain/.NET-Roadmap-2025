# Chapter 6.1: LINQ

## 前言

歡迎來到第六章！在前面的章節中，我們學習了如何設計資料庫以及如何使用 SQL 來查詢資料。然而，在 C# 程式碼中手動拼接 SQL 字串不僅繁瑣、容易出錯，而且無法享受到編譯時的型別檢查。為了解決這個問題，.NET 提供了一個強大而優雅的工具：**LINQ**。

## 什麼是 LINQ？

**LINQ (Language-Integrated Query)**，即「語言整合查詢」，是 .NET 平台的一項革命性創新。它將查詢功能直接整合到 C# 語言中，讓你能夠使用一種統一的、類似 SQL 的語法來查詢各種不同的資料來源，無論是記憶體中的物件集合 (Object)、關聯式資料庫 (SQL)、還是 XML 文件。

**LINQ 的核心優勢：**
- **統一的查詢語法**：學習一套語法，通吃各種資料來源。
- **編譯時型別檢查**：在編譯期間就能發現查詢語法中的錯誤（例如打錯欄位名），而不是在執行階段才拋出例外。
- **智慧感知 (IntelliSense)**：Visual Studio/VS Code 能為你的查詢提供完整的程式碼提示和自動完成功能。
- **簡潔與可讀性**：讓複雜的資料篩選、排序和分組操作變得極其簡潔和易於閱讀。

## LINQ 的兩種語法

LINQ 提供了兩種等效的寫法：**查詢語法 (Query Syntax)** 和 **方法語法 (Method Syntax)**。

假設我們有一個 `Product` 物件的集合：
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 1200, Category = "Electronics" },
    new Product { Id = 2, Name = "Keyboard", Price = 80, Category = "Electronics" },
    new Product { Id = 3, Name = "T-Shirt", Price = 25, Category = "Apparel" },
    new Product { Id = 4, Name = "Jeans", Price = 60, Category = "Apparel" },
};
```

現在，我們想找出所有價格低於 100 元的電子產品，並按價格排序。

### 1. 查詢語法 (Query Syntax)

這種語法刻意設計得與 SQL 非常相似，對於熟悉 SQL 的開發人員來說非常直觀。

```csharp
var cheapElectronics = from p in products
                       where p.Category == "Electronics" && p.Price < 100
                       orderby p.Price
                       select p;
```

### 2. 方法語法 (Method Syntax)

這種語法使用一系列的擴充方法（如 `Where()`, `OrderBy()`, `Select()`）和 Lambda 運算式來建構查詢。在編譯後，所有的查詢語法最終都會被轉換成方法語法。

```csharp
var cheapElectronics = products
    .Where(p => p.Category == "Electronics" && p.Price < 100)
    .OrderBy(p => p.Price);
```

在實務中，開發人員通常會混合使用兩者，或者根據個人或團隊的偏好選擇其中一種。方法語法在組合動態查詢時通常更具靈活性。

## 延遲執行 (Deferred Execution)

這是 LINQ 的一個核心特性。當你撰寫一個 LINQ 查詢時，它並不會立刻執行。它只會建立一個「查詢計畫」。只有當你真正需要存取查詢結果時（例如，透過 `foreach` 迴圈、`ToList()`、`FirstOrDefault()` 等方法），查詢才會被實際執行。

這個特性帶來了巨大的好處：
- **效率**：可以將多個查詢操作串連在一起，最終組合成一個更高效的單一查詢來執行。
- **靈活性**：可以在執行查詢之前動態地修改它。

## 結語

LINQ 是 .NET 開發人員的必備技能。它徹底改變了我們與資料互動的方式，讓程式碼變得更簡潔、更安全、更具表達力。無論你是在處理簡單的記憶體集合，還是在與龐大的資料庫打交道，LINQ 都將是你最得力的助手。

在下一個章節，我們將探討 **Entity Framework Core**，看看 LINQ 如何與這個強大的 ORM 框架結合，將 C# 物件查詢無縫轉換為高效的 SQL。

---

# Chapter 6.2: Entity Framework Core

## 前言

我們已經學會了用 LINQ 來查詢物件，但如何將這些優雅的 LINQ 查詢應用到實際的資料庫上呢？答案就是 **ORM (Object-Relational Mapper)**，而 **Entity Framework Core (EF Core)** 正是 .NET 領域中最主流、功能最強大的 ORM 框架。

## 什麼是 ORM？

ORM 是一種程式設計技術，它在「物件導向的程式語言」和「關聯式資料庫」這兩個不相容的世界之間建立起一座橋樑。它允許你：
- **用 C# 物件來操作資料庫**：你可以像操作普通 C# 物件一樣，對資料庫進行增、刪、改、查，而無需編寫任何 SQL 程式碼。
- **將資料庫的資料表 (Table) 對應到 C# 的類別 (Class)**。
- **將資料表的資料列 (Row) 對應到 C# 的物件 (Object)。**

EF Core 會在幕後將你的 LINQ to Entities 查詢翻譯成對應的 SQL 查詢，並將查詢結果轉換回 C# 物件。

## EF Core 核心概念

### 1. DbContext
`DbContext` 是 EF Core 的心臟。它代表了與資料庫的一次會話 (Session)，並提供了存取資料庫的主要入口。
- 你需要建立一個繼承自 `DbContext` 的類別。
- 在這個類別中，你會定義你的資料庫包含哪些資料表。
- 它還負責追蹤物件的變更，並在 `SaveChanges()` 被呼叫時，將這些變更寫入資料庫。

### 2. DbSet
`DbSet<T>` 代表資料庫中的一個資料表。`T` 是你定義的實體類別 (Entity Class)，例如 `Product`, `User`。
- `DbContext` 中的每一個 `DbSet` 屬性都對應到資料庫中的一個資料表。
- 你可以透過 `DbSet` 來對資料表進行 LINQ 查詢。

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}
```

### 3. 實體 (Entity)
實體就是一個簡單的 C# 類別 (POCO - Plain Old CLR Object)，它對應到資料庫中的一個資料表結構。

```csharp
public class Product
{
    public int Id { get; set; } // 會被自動識別為主鍵
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

### 4. 資料庫遷移 (Migrations)

當你的實體模型發生變化時（例如，為 `Product` 類別增加一個新屬性），你需要更新資料庫的結構來與之對應。**Migrations** 就是 EF Core 提供的解決方案。
- 它能比較你的程式碼模型和資料庫結構的差異。
- 自動產生 C# 程式碼來描述如何更新資料庫（例如 `AddColumn`, `CreateTable`）。
- 你可以執行這些遷移來安全地更新資料庫，而不會遺失現有資料。

## CRUD 操作範例

```csharp
// 注入 DbContext
private readonly ApplicationDbContext _context;

// Create
var newProduct = new Product { Name = "Wireless Mouse", Price = 45 };
_context.Products.Add(newProduct);
_context.SaveChanges();

// Read
var cheapProducts = _context.Products
    .Where(p => p.Price < 50)
    .ToList();

// Update
var productToUpdate = _context.Products.Find(1); // 根據主鍵查找
if (productToUpdate != null)
{
    productToUpdate.Price = 1150;
    _context.SaveChanges();
}

// Delete
var productToDelete = _context.Products.Find(2);
if (productToDelete != null)
{
    _context.Products.Remove(productToDelete);
    _context.SaveChanges();
}
```

## 結語

EF Core 極大地提高了 .NET 開發人員的生產力。它將開發人員從繁瑣的 SQL 撰寫和資料庫存取細節中解放出來，讓我們可以更專注於應用程式的商業邏輯。雖然它功能強大，但理解其背後的運作原理（例如它產生的 SQL）對於進行效能調校仍然至關重要。

然而，EF Core 並非唯一的選擇。在下一個章節，我們將介紹一個輕量級、以效能著稱的替代方案：**Dapper**。

---

# Chapter 6.3: Dapper

## 前言

雖然 EF Core 功能強大且方便，但在某些極端要求效能的場景下，ORM 的抽象層可能會帶來一些額外的開銷。如果你想回歸 SQL 的掌控力，同時又不想回到手動處理 `DataReader` 和參數的繁瑣時代，那麼 **Dapper** 就是你的最佳選擇。

## 什麼是 Dapper？

Dapper 是一個簡單、輕量級的 **Micro ORM**。它由 Stack Overflow 團隊開發，其首要目標就是 **效能**。

與 EF Core 這種「完全 ORM」不同，Dapper 並不試圖隱藏 SQL。相反，它擁抱 SQL。Dapper 的核心功能只有一個：**讓你輕鬆地執行 SQL 查詢，並將查詢結果快速地對應 (map) 到 C# 物件。**

**Dapper 的核心理念：**
- **效能至上**：Dapper 的速度極快，幾乎與手動撰寫 ADO.NET `DataReader` 程式碼一樣快。
- **SQL 的完全掌控**：你編寫和優化你自己的 SQL 查詢。
- **輕量級**：它只是一個 NuGet 套件，提供了一組 `IDbConnection` 的擴充方法，沒有複雜的設定和追蹤機制。

## Dapper 如何運作？

Dapper 作為 `IDbConnection` 的擴充方法存在。這意味著你可以在任何支援 ADO.NET 的資料庫連線上使用它。

```csharp
// 假設已有一個 IDbConnection 物件 conn

// 查詢多筆資料
string sql = "SELECT * FROM Products WHERE Price < @MaxPrice";
var cheapProducts = conn.Query<Product>(sql, new { MaxPrice = 50 });

// 查詢單筆資料
string sql = "SELECT * FROM Products WHERE Id = @ProductId";
var product = conn.QueryFirstOrDefault<Product>(sql, new { ProductId = 1 });

// 執行 INSERT, UPDATE, DELETE
string sql = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
int affectedRows = conn.Execute(sql, new { Name = "USB Hub", Price = 20 });
```

在上面的範例中：
- `Query<T>()` 方法執行查詢，並自動將結果的每一行對應到一個 `Product` 物件。
Dapper 會根據 SQL 查詢結果的欄位名稱，自動匹配 `Product` 類別中名稱相同的屬性。
- `Execute()` 方法用於執行不會返回查詢結果的 SQL 命令。
- 參數是透過一個匿名物件傳遞的，Dapper 會自動將其轉換為安全的資料庫參數，有效防止 **SQL Injection** 攻擊。

## Dapper vs. Entity Framework Core

| 特性 | Dapper | Entity Framework Core |
| :--- | :--- | :--- |
| **抽象層級** | 低 (Micro ORM) | 高 (Full ORM) |
| **SQL 控制** | 完全控制，手寫 SQL | 自動產生 SQL，也可執行原生 SQL |
| **效能** | 極高 | 良好，但有一定抽象開銷 |
| **開發速度** | 較慢 (需寫 SQL) | 非常快 (使用 LINQ) |
| **功能** | 專注於查詢與對應 | 功能豐富 (變更追蹤, 遷移, 快取) |
| **學習曲線** | 非常低 (如果你懂 SQL) | 較高 (需理解 DbContext, LINQ to Entities) |

## 何時選擇 Dapper？

- **效能是首要考量**：當你的應用程式有大量資料讀取操作，且需要榨乾每一分效能時。
- **需要精細控制 SQL**：當你需要編寫複雜、高度優化的查詢，而 ORM 產生的 SQL 無法滿足需求時。
- **專案較小或為讀取密集型**：對於一些簡單的應用或主要負責資料查詢的微服務，Dapper 的輕量級特性非常有吸引力。

在許多大型專案中，開發團隊會混合使用這兩種工具（CQRS 架構就是一個很好的例子）：
- **寫入操作 (Commands)**：使用 EF Core，因為可以利用其方便的變更追蹤和交易管理功能。
- **讀取操作 (Queries)**：使用 Dapper，繞過 EF Core 的追蹤機制，直接執行高效的 SQL 查詢以獲得最佳效能。

## 結語

Dapper 和 EF Core 並非互相排斥，而是可以互補的強大工具。EF Core 提供了無與倫比的開發效率和便利性，而 Dapper 則在需要時為你提供了通往極致效能和 SQL 完全控制權的途徑。

掌握這兩種 ORM，你將能夠根據不同的業務場景和效能需求，為你的 .NET 應用程式選擇最合適的資料存取策略。