# Chapter 9.1: Unit Testing

## 前言

歡迎來到第九章！軟體開發不僅僅是撰寫能夠運作的程式碼，更重要的是確保程式碼的 **正確性**、**可靠性** 和 **可維護性**。而「測試」正是達成這些目標的核心手段。在本章中，我們將從最基礎、最重要的「單元測試 (Unit Testing)」開始。

## 什麼是單元測試？

單元測試是針對應用程式中最小的可測試單元——通常是一個方法或一個類別——進行驗證的過程。它的目標是隔離地測試一小段程式碼，確保它在各種預期和非預期的輸入下，都能產生正確的輸出。

**一個好的單元測試應該具備 "FIRST" 原則：**
- **Fast (快速)**：單元測試應該執行得非常快。一個大型專案可能有數千個單元測試，它們需要在幾秒鐘內完成，以便開發人員可以頻繁地執行它們。
- **Independent/Isolated (獨立/隔離)**：每個測試都應該是獨立的，不依賴於其他測試的執行順序或結果。它們不應該與外部依賴（如資料庫、檔案系統、網路）直接互動。
- **Repeatable (可重複)**：測試在任何環境下執行都應該產生相同的結果。
- **Self-Validating (自我驗證)**：測試應該能夠自動判斷成功或失敗，無需人工介入。測試結果應該是明確的布林值（成功/失敗）。
- **Timely (及時)**：單元測試應該在被測試的程式碼撰寫完成後立即撰寫，或者在「測試驅動開發 (TDD)」中甚至在之前撰寫。

## 單元測試的結構：Arrange-Act-Assert (3A)

一個結構清晰的單元測試通常遵循 3A 模式：

1.  **Arrange (安排)**：設定測試所需的所有前提條件。這包括初始化物件、設定模擬 (Mock) 物件的行為、準備測試資料等。
2.  **Act (執行)**：呼叫被測試的方法。
3.  **Assert (斷言)**：驗證執行的結果是否符合預期。如果驗證失敗，測試框架會將該測試標記為失敗。

```csharp
// 一個簡單的計算器類別
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

// 對 Add 方法的單元測試
public class CalculatorTests
{
    [Fact] // [Fact] 是 xUnit 中用於標記測試方法的屬性
    public void Add_ShouldReturnCorrectSum_WhenGivenTwoIntegers()
    {
        // Arrange
        var calculator = new Calculator();
        int a = 5;
        int b = 10;

        // Act
        int result = calculator.Add(a, b);

        // Assert
        Assert.Equal(15, result);
    }
}
```

## 為什麼單元測試如此重要？

- **提供安全網 (Safety Net)**：當你進行程式碼重構或新增功能時，單元測試是你的安全網。如果你的修改意外地破壞了現有功能，測試會立即失敗，讓你能快速定位並修復問題。
- **改善設計**：難以測試的程式碼通常是設計不良的程式碼。編寫單元測試會「迫使」你寫出更鬆耦合、更遵循單一職責原則的程式碼，因為這樣的程式碼才更容易被隔離和測試。
- **作為文件**：單元測試是最好的「活文件」。透過閱讀一個類別的測試，你可以清楚地了解這個類別的預期行為和各種邊界情況。

## 結語

單元測試是專業軟體開發的基石。它不僅關乎品質保證，更是一種能提升程式碼設計、促進重構、增強團隊信心的開發實踐。雖然編寫測試需要投入額外的時間，但從長遠來看，它所帶來的高品質、高可維護性和開發速度的提升，將遠遠超過其成本。

在接下來的章節中，我們將介紹 .NET 中流行的測試框架和「模擬 (Mocking)」技術，讓你能夠為更複雜的場景編寫單元測試。

---

# Chapter 9.2: Unit Testing Frameworks

## 前言

要撰寫和執行單元測試，你需要一個測試框架。測試框架提供了一套執行測試、組織測試和斷言結果的工具和 API。在 .NET 生態系中，有三個主要的測試框架：**xUnit.net**, **MSTest**, 和 **NUnit**。我們將重點介紹前兩者，其中 xUnit 是目前社群中最受歡迎的現代化選擇。

## 1. xUnit.net

xUnit 是一個免費、開源、以社群為中心的 .NET 測試框架。它的設計哲學是提供一個現代、可擴展且易於使用的測試體驗。

**核心特性：**
- **實例化行為**：xUnit 為每個測試方法建立一個新的測試類別實例。這確保了測試之間的完全隔離，避免了因共享狀態而導致的測試互相影響。
- **`[Fact]` 與 `[Theory]`**：
    - **`[Fact]`**：用於標記一個無參數的、簡單的測試方法。
    - **`[Theory]`**：用於標記一個參數化的測試方法。你可以提供多組 `[InlineData]`，xUnit 會為每一組資料獨立執行一次測試。這對於測試多種邊界情況非常有用。

```csharp
public class CalculatorTests
{
    [Fact]
    public void Add_ShouldReturnCorrectSum()
    {
        var calculator = new Calculator();
        Assert.Equal(4, calculator.Add(2, 2));
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, 1, 0)]
    [InlineData(0, 0, 0)]
    public void Add_ShouldReturnCorrectSum_ForVariousInputs(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(a, b);
        Assert.Equal(expected, result);
    }
}
```

## 2. MSTest

MSTest 是由 Microsoft 開發的官方測試框架，它與 Visual Studio 深度整合。雖然它比 xUnit 更傳統，但在 .NET 6/7 中也進行了許多現代化改進。

**核心特性：**
- **傳統的實例化行為**：預設情況下，MSTest 只為整個測試類別建立一個實例，所有測試方法共享這個實例。這意味著你需要小心處理測試之間的共享狀態。
- **`[TestMethod]`**：這是 MSTest 中用於標記測試方法的主要屬性。
- **`[DataRow]`**：類似於 xUnit 的 `[InlineData]`，用於提供參數化測試的資料。

```csharp
[TestClass] // 標記這是一個測試類別
public class CalculatorTests
{
    [TestMethod] // 標記這是一個測試方法
    public void Add_ShouldReturnCorrectSum()
    {
        var calculator = new Calculator();
        Assert.AreEqual(4, calculator.Add(2, 2));
    }

    [DataTestMethod]
    [DataRow(1, 2, 3)]
    [DataRow(-1, 1, 0)]
    public void Add_ShouldReturnCorrectSum_ForVariousInputs(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(a, b);
        Assert.AreEqual(expected, result);
    }
}
```

## 結論

對於新的 .NET 專案，**xUnit** 通常是推薦的選擇，因為它的設計鼓勵更好的測試隔離實踐。然而，MSTest 也是一個完全可行且功能強大的選擇，特別是如果你習慣於它與 Visual Studio 的傳統整合方式。兩者都能很好地完成工作，選擇哪一個通常取決於團隊的偏好。

---

# Chapter 9.3: Mocking

## 前言

我們在前面提到，單元測試應該是 **隔離的**。但現實世界中的程式碼充滿了依賴：一個服務可能需要呼叫另一個服務、從資料庫讀取資料、或與檔案系統互動。如果我們在單元測試中直接使用這些真實的依賴，測試將會變得：
- **緩慢**：網路和磁碟 I/O 非常耗時。
- **不穩定**：測試可能會因為網路問題或資料庫中斷而失敗。
- **難以設定**：很難將資料庫或外部 API 設定到一個特定的狀態來測試某個邊界情況。

為了解決這個問題，我們引入了 **模擬 (Mocking)** 技術。

## 什麼是 Mocking？

Mocking 是指在測試中，使用一個「假的」物件來取代一個真實的依賴。這個假物件的行為完全在我們的控制之下。

假設我們有一個 `OrderProcessor` 類別，它依賴於一個 `INotificationService` 來發送訂單確認郵件。

```csharp
public interface INotificationService
{
    void SendOrderConfirmation(int orderId);
}

public class OrderProcessor
{
    private readonly INotificationService _notificationService;

    public OrderProcessor(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void ProcessOrder(int orderId)
    {
        // ... 處理訂單的邏輯 ...

        // 發送確認郵件
        _notificationService.SendOrderConfirmation(orderId);
    }
}
```

在測試 `OrderProcessor` 時，我們不關心郵件是否真的被發送出去，我們只關心 `SendOrderConfirmation` 方法是否被 **正確地呼叫** 了。這時，我們就可以使用一個 Mocking 框架來建立一個假的 `INotificationService`。

## Mocking 框架

.NET 中有兩個流行的 Mocking 框架：**NSubstitute** 和 **Moq**。NSubstitute 以其更簡潔、更自然的語法而受到許多開發人員的喜愛。

### 1. NSubstitute

NSubstitute 的語法非常接近自然語言。

```csharp
[Fact]
public void ProcessOrder_ShouldCallNotificationService()
{
    // Arrange
    // 1. 建立一個 INotificationService 的替代品 (substitute)
    var notificationServiceMock = Substitute.For<INotificationService>();
    var orderProcessor = new OrderProcessor(notificationServiceMock);
    int orderId = 123;

    // Act
    orderProcessor.ProcessOrder(orderId);

    // Assert
    // 2. 驗證 SendOrderConfirmation 方法是否被呼叫了，且參數是 123
    notificationServiceMock.Received().SendOrderConfirmation(orderId);
}
```

### 2. Moq

Moq 是另一個非常流行且功能強大的 Mocking 框架，它的語法更依賴於 Lambda 運算式。

```csharp
[Fact]
public void ProcessOrder_ShouldCallNotificationService()
{
    // Arrange
    // 1. 建立一個 Mock 物件
    var notificationServiceMock = new Mock<INotificationService>();
    var orderProcessor = new OrderProcessor(notificationServiceMock.Object); // 注意要傳入 .Object
    int orderId = 123;

    // Act
    orderProcessor.ProcessOrder(orderId);

    // Assert
    // 2. 驗證方法是否被呼叫
    notificationServiceMock.Verify(s => s.SendOrderConfirmation(orderId), Times.Once());
}
```

## 結語

Mocking 是單元測試中不可或缺的一項核心技能。它讓我們能夠將被測試的單元與其依賴完全隔離，從而寫出快速、穩定且專注的測試。學會使用像 NSubstitute 或 Moq 這樣的框架，你將能夠為任何複雜的、具有外部依賴的程式碼編寫出高效率的單元測試。

---

# Chapter 9.4: Integration Testing

## 前言

單元測試非常適合用來驗證獨立的程式碼單元，但它們無法告訴我們這些單元組合在一起時是否能正常工作。**整合測試 (Integration Testing)** 正是為了解決這個問題而生。它的目標是測試應用程式中多個元件之間的互動，確保它們能夠協同運作。

在 ASP.NET Core 中，整合測試通常意味著在記憶體中啟動整個 Web 應用程式，並像真實的客戶端一樣，向其傳送 HTTP 請求，然後驗證回傳的 HTTP 回應。

## `WebApplicationFactory`

`Microsoft.AspNetCore.Mvc.Testing` 這個 NuGet 套件提供了一個神奇的工具：`WebApplicationFactory<T>`。它能夠在記憶體中啟動你的整個 ASP.NET Core 應用程式，包括所有的依賴注入設定、中介軟體和路由。

**核心優點：**
- **真實的執行環境**：測試是在一個與生產環境極其相似的環境中執行，而不是在模擬的物件上。
- **無需網路**：所有的請求和回應都在記憶體中完成，速度非常快，且不受網路影響。
- **可替換依賴**：你可以在測試中輕鬆地替換掉外部依賴（如資料庫、外部 API），換成測試專用的版本（如記憶體內資料庫或 Mock 服務）。

### 使用範例

```csharp
public class BasicTests : IClassFixture<WebApplicationFactory<Program>> // Program 是你的主控台應用程式進入點
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Home/Privacy")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient(); // 建立一個 HttpClient 來傳送請求

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // 斷言狀態碼為 2xx
        Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
    }
}
```

## `TestServer`

`TestServer` 是 `WebApplicationFactory` 底層使用的核心元件，它代表了在記憶體中執行的伺服器。在某些進階場景下，你可能會直接使用 `TestServer` 來獲得更精細的控制。

## `Testcontainers`

當你的應用程式需要與真實的外部基礎設施（如資料庫、訊息佇列）進行整合測試時，`WebApplicationFactory` 的記憶體內測試就不夠了。這時，**Testcontainers** 就派上用場了。

Testcontainers 是一個函式庫，它能讓你以程式化的方式，在測試執行期間，輕鬆地啟動和銷毀 Docker 容器。

**核心優點：**
- **真實的依賴**：你的測試是針對一個真實的 PostgreSQL 資料庫或 RabbitMQ 執行個體進行的，而不是記憶體內的替代品。
- **隔離且一致**：每個測試（或測試類別）都可以擁有自己專屬的、乾淨的容器實例，確保測試之間互不干擾。
- **簡化 DevOps**：無需手動管理測試用的資料庫伺服器，一切都在測試程式碼中自動完成。

### 使用範例

```csharp
public class DatabaseTests : IAsyncLifetime
{
    // 1. 定義一個 PostgreSQL 容器
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithDatabase("testdb")
        .WithUsername("testuser")
        .WithPassword("testpass")
        .Build();

    // 2. 在測試開始前啟動容器
    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
    }

    [Fact]
    public async Task TestDatabaseConnection()
    {
        // 3. 使用容器提供的連線字串來連線資料庫
        await using var connection = new NpgsqlConnection(_postgres.GetConnectionString());
        await connection.OpenAsync();

        // ... 執行你的資料庫測試 ...
    }

    // 4. 在測試結束後銷毀容器
    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }
}
```

## 結語

整合測試是確保應用程式各個部分能夠正確協同工作的關鍵。`WebApplicationFactory` 為 ASP.NET Core 應用程式提供了強大的記憶體內測試能力，而 `Testcontainers` 則將整合測試提升到了一個新的層次，讓你能夠針對真實的依賴進行可靠、隔離的測試。將單元測試和整合測試結合起來，你就能為你的應用程式建立一個全面而堅固的品質保證體系。