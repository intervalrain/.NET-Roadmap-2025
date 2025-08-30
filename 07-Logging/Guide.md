# Chapter 7.1: Microsoft.Extensions.Logging

## 前言

歡迎來到第七章！在任何正式的應用程式中，「日誌 (Logging)」都是不可或缺的一環。良好的日誌不僅是追蹤錯誤、診斷問題的關鍵，也是監控應用程式健康狀況、分析使用者行為的重要依據。在 .NET 的世界裡，这一切都始於一個統一的日誌抽象層：`Microsoft.Extensions.Logging`。

## 為什麼需要日誌抽象？

在 .NET 生態系中有許多優秀的第三方日誌框架（如 Serilog, NLog）。如果你的應用程式直接依賴於其中某一個，那麼未來想要更換日誌框架時，就需要修改每一處呼叫日誌紀錄的程式碼，這將是一場災難。

為了解決這個問題，.NET 提供了一套通用的日誌 **抽象層**。它定義了一組標準的介面和 API (如 `ILogger`, `ILoggerFactory`)，讓你的應用程式程式碼只依賴於這些抽象，而不是任何具體的日誌框架。

這帶來的好處是：
- **解耦合**：你的應用程式與具體的日誌實現完全分離。
- **可插拔**：你可以隨時替換底層的日誌框架，而無需修改任何業務邏輯程式碼。只需要在應用程式的啟動設定中，將日誌提供者 (Provider) 更換掉即可。

## `Microsoft.Extensions.Logging` 核心元件

這個函式庫主要由以下幾個核心元件組成：

### 1. `ILogger`
`ILogger<T>` 是執行日誌紀錄的主要介面。通常，你會透過「依賴注入」在你的類別中取得它的實例。泛型參數 `T` 通常是你正在撰寫日誌的類別，這有助於在日誌輸出中標明來源。

```csharp
public class MyService
{
    private readonly ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("開始執行工作...");
        // ...
    }
}
```

### 2. 日誌級別 (Log Levels)
日誌級別用於標示日誌訊息的重要性，讓你可以在設定中過濾掉不關心的訊息。級別由低到高分別是：
- **Trace**：最詳細的日誌，通常只在開發時用於追蹤程式流程。
- **Debug**：用於開發和除錯的資訊。
- **Information**：應用程式正常執行時的流程性資訊（例如：服務啟動、請求處理完成）。
- **Warning**：發生了非預期的、但不會導致應用程式停止的事件（例如：使用了已棄用的 API）。
- **Error**：發生了錯誤，導致當前操作失敗，但應用程式仍可繼續執行。
- **Critical**：非常嚴重的錯誤，可能導致應用程式崩潰（例如：記憶體不足、磁碟空間滿）。

`ILogger` 介面為每個級別都提供了方便的擴充方法：
```csharp
_logger.LogTrace("這是一條追蹤訊息");
_logger.LogDebug("這是一條除錯訊息");
_logger.LogInformation("這是一條資訊訊息");
_logger.LogWarning("這是一條警告訊息");
_logger.LogError("這是一條錯誤訊息");
_logger.LogCritical("這是一條嚴重錯誤訊息");
```

### 3. 日誌提供者 (Log Providers)
日誌提供者是實際負責將日誌訊息寫入到特定目標的元件。`Microsoft.Extensions.Logging` 內建了幾個基本的提供者：
- **Console**：將日誌輸出到主控台。
- **Debug**：將日誌輸出到偵錯工具的輸出視窗。
- **EventSource** / **EventLog**：將日誌寫入到系統事件相關的服務中。

你可以同時設定多個提供者，讓日誌訊息同時輸出到多個地方。

```csharp
// 在 Program.cs 或 Startup.cs 中設定
builder.Logging.ClearProviders(); // 清除預設提供者
builder.Logging.AddConsole();     // 新增主控台提供者
builder.Logging.AddDebug();       // 新增偵錯提供者
```

### 4. 結構化日誌 (Structured Logging)

現代日誌系統強烈推薦使用「結構化日誌」。傳統的日誌是將所有資訊拼接成一個純文字字串，而結構化日誌則是將訊息範本和其對應的參數分開儲存。

**傳統日誌：**
```csharp
_logger.LogInformation($"使用者 {userId} 處理訂單 {orderId} 失敗。");
```

**結構化日誌：**
```csharp
_logger.LogInformation("使用者 {UserId} 處理訂單 {OrderId} 失敗。", userId, orderId);
```

**優點**：
- **可查詢性**：日誌後端系統（如 Seq, Splunk, Elasticsearch）可以將 `UserId` 和 `OrderId` 識別為獨立的欄位。你可以輕易地查詢「所有關於 `UserId` 為 123 的日誌」或「所有 `OrderId` 開頭為 `X` 的失敗日誌」。
- **可分析性**：更容易進行資料分析和視覺化。

## 結語

`Microsoft.Extensions.Logging` 為 .NET 應用程式提供了一個強大而靈活的日誌基礎。它透過統一的抽象，讓我們的應用程式保持了對具體日誌框架的獨立性。

雖然內建的提供者在開發階段很方便，但在生產環境中，我們通常會使用功能更強大的第三方日誌框架。在下一個章節，我們將學習如何整合目前社群中最受歡迎的結構化日誌框架：**Serilog**。

---

# Chapter 7.2: Serilog

## 前言

雖然 `Microsoft.Extensions.Logging` 提供了一套極佳的抽象，但其內建的日誌提供者功能相對基礎。要在生產環境中實現強大、靈活且高效的日誌紀錄，我們需要一個更專業的工具。**Serilog** 正是 .NET 社群中最受歡迎的結構化日誌框架之一。

## 為什麼選擇 Serilog？

Serilog 從一開始就為「結構化日誌」而設計，並圍繞這個核心理念打造了極其豐富的生態系。

- **卓越的結構化日誌能力**：它的 API 設計能讓你輕鬆地撰寫出真正有用的結構化日誌。
- **豐富的 Sinks (接收器)**：Sink 是 Serilog 中對應日誌輸出目標的術語。Serilog 擁有數十個由社群貢獻的 Sinks，可以將日誌寫入到你能想到的任何地方：
    - **檔案**：滾動日誌檔案 (Rolling File)
    - **主控台**：提供美觀、帶有色彩的格式化輸出
    - **資料庫**：SQL Server, PostgreSQL, MongoDB...
    - **日誌管理系統**：Seq, Splunk, Elasticsearch, Datadog, Application Insights...
- **動態設定**：可以從 `appsettings.json` 檔案中讀取設定，讓你可以在不重新編譯程式碼的情況下，調整日誌級別或更換 Sinks。
- **內容擴充 (Enrichment)**：可以輕鬆地為所有日誌訊息自動添加額外的上下文資訊，例如 `ThreadId`, `MachineName`, `CorrelationId` (關聯 ID) 等。

## 整合 Serilog 到 ASP.NET Core

整合 Serilog 非常簡單，主要分為兩步：

1.  **安裝 NuGet 套件**：
    - `Serilog.AspNetCore`：核心整合套件。
    - `Serilog.Sinks.Console`：寫入主控台的 Sink。
    - `Serilog.Sinks.File`：寫入檔案的 Sink。

2.  **在 `Program.cs` 中設定**：

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1. 將預設的 Logger 替換為 Serilog
builder.Host.UseSerilog((context, configuration) => 
{
    // 2. 從 appsettings.json 讀取設定
    configuration.ReadFrom.Configuration(context.Configuration);
});

// ... 你的其他服務設定

var app = builder.Build();

// ...

app.Run();
```

3.  **在 `appsettings.json` 中設定 Serilog**：

這是在生產環境中推薦的做法，因為它將日誌設定與程式碼分離。

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  }
}
```

在這個設定中：
- `MinimumLevel`：設定了日誌的最低級別。預設是 `Information`，但對於來自 `Microsoft` 命名空間的日誌，則提高到 `Warning` 級別，以過濾掉大量冗長的框架內部日誌。
- `WriteTo`：定義了要使用的 Sinks 陣列。這裡我們設定了 `Console` 和 `File` 兩個 Sinks。
- `File` Sink 的 `Args` 詳細設定了檔案路徑、滾動週期（每天一個新檔案）以及輸出的格式範本。
- `Enrich`：設定了要使用的內容擴充器。

## 結語

Serilog 與 `Microsoft.Extensions.Logging` 的抽象完美結合，提供了一套無與倫比的日誌解決方案。它讓 .NET 開發人員能夠輕鬆實現強大的結構化日誌記錄，這在現代分散式、雲端原生的應用程式中至關重要。

學會使用 Serilog，你將能夠為你的應用程式建立清晰、可查詢、易於分析的日誌，從而在問題發生時快速定位，並深入洞察應用程式的執行狀況。