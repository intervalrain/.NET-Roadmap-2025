# Chapter 11.1: IHostedService

## 前言

歡迎來到第十一章！在許多應用程式中，除了回應外部請求（如 HTTP 請求）外，還需要在背景持續執行一些任務。例如：
- 定時清理過期的快取。
- 從訊息佇列中拉取訊息並進行處理。
- 監控某個資料夾的變化。

在 .NET 中，實現這種背景任務的標準化、優雅的方式就是使用 **`IHostedService`** 介面。

## 什麼是 `IHostedService`？

`IHostedService` 是 .NET 通用主機 (Generic Host) 提供的一個介面，它讓開發人員能夠將一個長時間執行的任務「掛載」到應用程式的生命週期中。當應用程式啟動時，主機會自動啟動所有註冊的 `IHostedService` 實作；當應用程式準備關閉時，主機會優雅地通知這些服務停止。

這個介面非常簡單，只包含兩個方法：

```csharp
public interface IHostedService
{
    // 當應用程式主機準備好啟動服務時呼叫
    Task StartAsync(CancellationToken cancellationToken);

    // 當應用程式主機執行優雅關閉時呼叫
    Task StopAsync(CancellationToken cancellationToken);
}
```

## 如何使用 `IHostedService`

1.  **建立一個實作 `IHostedService` 的類別**

    ```csharp
    public class MyHostedService : IHostedService
    {
        private readonly ILogger<MyHostedService> _logger;

        public MyHostedService(ILogger<MyHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("託管服務啟動中...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("託管服務停止中...");
            return Task.CompletedTask;
        }
    }
    ```

2.  **在 `Program.cs` 中註冊服務**

    你需要將你的實作註冊到依賴注入容器中。

    ```csharp
    var builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddHostedService<MyHostedService>();

    var host = builder.Build();
    host.Run();
    ```

當你執行應用程式時，你將在主控台中看到服務啟動和停止的日誌訊息。

## `IHostedService` 的限制

`IHostedService` 非常適合那些只需要在應用程式啟動時執行一次性設定工作的任務。然而，如果你想在 `StartAsync` 中執行一個長時間的迴圈任務（例如，每隔5秒鐘做一次某件事），你會發現一個問題：`StartAsync` 方法會「阻塞」整個應用程式的啟動過程。在 `StartAsync` 完成之前，Web 應用不會開始監聽請求，其他託管服務也不會啟動。

為了解決這個問題，.NET 提供了一個更方便的基礎類別，它專為長時間執行的背景任務而設計。

## 結語

`IHostedService` 為在 .NET 應用程式中執行背景任務提供了一個標準化的入口點，並將這些任務與應用程式的生命週期優雅地整合在一起。它是理解 .NET 中所有背景工作模式的基礎。

在下一個章節，我們將學習如何使用 **`BackgroundService`**，一個繼承自 `IHostedService` 的抽象基礎類別，來更輕鬆地實現長時間執行的背景任務。

---

# Chapter 11.2: BackgroundService

## 前言

正如上一章所討論的，直接實作 `IHostedService` 來執行長時間的迴圈任務會阻塞應用程式的啟動。為了解決這個問題，.NET 團隊提供了一個抽象基礎類別 **`BackgroundService`**，它繼承自 `IHostedService`，並極大地簡化了長時間執行背景任務的開發。

## `BackgroundService` 如何運作？

`BackgroundService` 在其內部已經處理好了 `StartAsync` 和 `StopAsync` 的生命週期管理。它將 `StartAsync` 的邏輯封裝為呼叫一個新的抽象方法 `ExecuteAsync`。這個 `ExecuteAsync` 方法的設計就是用來包含你的長時間執行邏輯的。

當 `StartAsync` 被呼叫時，`BackgroundService` 會在一個新的背景執行緒上啟動 `ExecuteAsync` 方法，然後 `StartAsync` 會 **立即完成**。這就確保了你的背景任務不會阻塞主應用程式的啟動流程。

你只需要繼承 `BackgroundService` 並覆寫 `ExecuteAsync` 這一個方法即可。

## 使用範例

讓我們來建立一個每隔三秒鐘就印出一條日誌的簡單背景服務。

```csharp
public class MyBackgroundWorker : BackgroundService
{
    private readonly ILogger<MyBackgroundWorker> _logger;

    public MyBackgroundWorker(ILogger<MyBackgroundWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("背景工作執行緒已啟動");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("背景工作正在執行中，時間: {time}", DateTimeOffset.Now);
            await Task.Delay(3000, stoppingToken);
        }

        _logger.LogInformation("背景工作執行緒已停止");
    }
}
```

在這個範例中：
- 我們繼承了 `BackgroundService`。
- 所有的業務邏輯都在 `ExecuteAsync` 方法中。
- `while` 迴圈會持續執行，直到應用程式發出關閉訊號。
- `stoppingToken.IsCancellationRequested` 會在應用程式關閉時變為 `true`，從而優雅地結束迴圈。
- `Task.Delay` 也接受了 `stoppingToken`，這確保了如果在延遲等待期間應用程式關閉，`Task.Delay` 會立即被取消，而不是繼續等待。

註冊方式與 `IHostedService` 完全相同：
```csharp
builder.Services.AddHostedService<MyBackgroundWorker>();
```

## 結語

`BackgroundService` 是在 .NET 中實現長時間執行背景任務的 **推薦方式**。它提供了一個乾淨、簡單且正確的模式，處理了所有與應用程式生命週期整合的複雜性，讓你能夠專注於編寫你的核心背景邏輯。

然而，`BackgroundService` 主要處理的是「執行緒管理」。如果你需要更複雜的「任務排程」功能（例如：在每天凌晨三點執行、每週一執行、或使用 Cron 表達式），你就需要一個更專業的排程函式庫。

---

# Chapter 11.3: Quartz.NET

## 前言

`BackgroundService` 非常適合持續執行的背景任務，但如果你的需求是基於時間的、更複雜的排程邏輯，例如「每五分鐘執行一次」、「每個工作日的下午五點執行」或「每月最後一天的凌晨執行」，那麼你需要一個功能齊全的任務排程函式庫。在 .NET 生態系中，**Quartz.NET** 是最著名、最廣泛使用的開源排程解決方案。

## 什麼是 Quartz.NET？

Quartz.NET 是一個從 Java 世界著名的 Quartz 專案移植過來的、功能完整的企業級任務排程器。它可以被整合到任何 .NET 應用程式中，用於排程和執行大量的任務。

**核心概念：**
1.  **Job (任務)**：這是一個實作 `IJob` 介面的類別。它的 `Execute` 方法中包含了你想要執行的實際工作。
2.  **Trigger (觸發器)**：觸發器定義了 Job 何時被執行。你可以建立簡單的觸發器（例如：在5秒後執行、每10秒重複一次），也可以建立基於日曆的、非常複雜的觸發器（使用 Cron 表達式）。
3.  **Scheduler (排程器)**：排程器是 Quartz.NET 的核心。它負責將 Job 和 Trigger 註冊在一起，並在正確的時間觸發 Job 的執行。

## 使用範例

1.  **安裝 NuGet 套件**
    - `Quartz.Extensions.Hosting`：用於與 .NET 的通用主機和依賴注入進行整合。

2.  **建立一個 Job**
    ```csharp
    // 加上 [DisallowConcurrentExecution] 可以防止同一個 Job 在前一次執行尚未完成時，又被重複觸發
    [DisallowConcurrentExecution]
    public class MyJob : IJob
    {
        private readonly ILogger<MyJob> _logger;

        public MyJob(ILogger<MyJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello from Quartz.NET! 時間: {time}", DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
    ```

3.  **在 `Program.cs` 中設定 Quartz.NET**
    ```csharp
    // 1. 註冊 Quartz 服務
    builder.Services.AddQuartz(q =>
    {
        // 2. 註冊我們的 Job
        var jobKey = new JobKey(nameof(MyJob));
        q.AddJob<MyJob>(opts => opts.WithIdentity(jobKey));

        // 3. 建立一個觸發器，設定排程
        q.AddTrigger(opts => opts
            .ForJob(jobKey) // 關聯到我們的 Job
            .WithIdentity($"{nameof(MyJob)}-trigger")
            .WithSimpleSchedule(s => s
                .WithIntervalInSeconds(10) // 每 10 秒執行一次
                .RepeatForever()));
    });

    // 4. 註冊 Quartz.NET 的託管服務
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    ```

## Cron Triggers

對於複雜的排程，Quartz.NET 支援標準的 Cron 表達式，提供了極大的靈活性。

```csharp
.WithCronSchedule("0 15 10 ? * MON-FRI") // 每個工作日的上午 10:15 執行
```

## 結語

Quartz.NET 是一個非常成熟和強大的任務排程框架。當你需要超越 `BackgroundService` 提供的簡單迴圈，實現基於時間的、複雜的排程邏輯時，Quartz.NET 是 .NET 開發中的不二之選。它與 .NET 通用主機的無縫整合，使其在現代 .NET 應用中的使用變得非常簡單。