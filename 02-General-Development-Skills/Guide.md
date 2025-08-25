# 2.1 版控系統 - Git 指南 (Version Control - Git)

歡迎來到通用開發技能的第一站！版本控制系統 (Version Control System, VCS) 是開發者最重要的工具之一，而 Git 是目前全世界最普及的 VCS。

## 1. 為什麼需要版本控制？

想像一下你正在寫一份重要的報告，你可能會這樣保存檔案：

- `報告_v1.doc`
- `報告_v2.doc`
- `報告_最終版.doc`
- `報告_最終版_真的最終版.doc`

這很快就會變得一團混亂。版本控制系統解決了這個問題，它能幫助你：

- **追蹤變更**: 精確記錄每一次的檔案修改。
- **團隊協作**: 讓多位開發者可以同時在同一個專案上工作，而不會互相覆蓋彼此的程式碼。
- **回到過去**: 如果不小心把專案改壞了，可以輕鬆地回復到之前的某個穩定版本。
- **建立分支 (Branching)**: 可以安全地開發新功能或修復錯誤，而不影響主要版本的穩定性。

## 2. Git 的核心概念：三個區域

要理解 Git，首先要了解它的三個主要區域：

1.  **工作目錄 (Working Directory)**: 這就是你電腦上實際看到和編輯的專案資料夾。
2.  **暫存區 (Staging Area / Index)**: 這是一個「準備提交」的區域。你可以把工作目錄中的某些變更挑選出來，放到暫存區，代表這些變更將被包含在下一次的「提交」中。
3.  **本地倉庫 (Local Repository)**: 這是 Git 儲存專案所有歷史紀錄的地方 (位於你專案根目錄下的 `.git` 資料夾中)。當你「提交」(Commit) 時，暫存區的內容快照就會被永久保存在這裡。

**流程比喻：**
`工作目錄` (你的書桌) -> `git add` -> `暫存區` (打包好的箱子) -> `git commit` -> `本地倉庫` (倉庫儲藏室)

## 3. 核心指令實作

現在，讓我們透過實際操作來學習最重要的幾個指令。

請打開您的終端機 (Terminal)，並依照以下步驟操作。

### 步驟 1: 建立練習目錄與初始化倉庫

```bash
# 進入我們這個單元的資料夾
cd 02-General-Development-Skills

# 建立一個新的練習用資料夾
mkdir Git-Practice

# 進入該資料夾
cd Git-Practice

# 將這個資料夾初始化成一個 Git 本地倉庫
# 這會建立一個隱藏的 .git 資料夾
git init
```

### 步驟 2: 第一次提交 (Commit)

1.  **建立新檔案**
    在 `Git-Practice` 資料夾中，建立一個名為 `plan.txt` 的檔案，並在其中輸入以下文字：

    ```
    My Learning Plan:
    1. Master Git.
    ```

2.  **檢查狀態**
    回到終端機，輸入以下指令來查看當前倉庫的狀態：
    ```bash
    git status
    ```
    你會看到 `plan.txt` 被列為 `Untracked files` (未追蹤的檔案)。

3.  **加入暫存區**
    使用 `git add` 指令將這個新檔案加入暫存區：
    ```bash
    git add plan.txt
    ```
    再次執行 `git status`，你會看到 `plan.txt` 現在位於 `Changes to be committed` (準備提交的變更) 清單中。

4.  **提交到倉庫**
    使用 `git commit` 指令，將暫存區的內容提交到本地倉庫。`-m` 參數可以讓你直接在指令中輸入提交訊息。
    ```bash
    git commit -m "Initial commit: Create learning plan"
    ```

### 步驟 3: 第二次提交

1.  **修改檔案**
    編輯 `plan.txt`，在檔案最後新增一行，使其內容變為：
    ```
    My Learning Plan:
    1. Master Git.
    2. Learn about clean code.
    ```

2.  **查看變更**
    使用 `git diff` 指令，查看工作目錄中的檔案與上次提交的版本有何不同：
    ```bash
    git diff
    ```

3.  **加入暫存區並提交**
    這次我們使用 `.` 來代表「所有變更的檔案」。
    ```bash
    git add .
    git commit -m "Update plan to include clean code"
    ```

### 步驟 4: 查看歷史紀錄

使用 `git log` 指令來查看所有的提交紀錄：

```bash
git log

# 你也可以試試這個更簡潔的格式
git log --oneline
```

---

請依照以上步驟實際操作一次。

完成後，請告訴我您 `git log --oneline` 的輸出結果。如果您在過程中遇到任何問題，也請隨時提出。

---

# 2.2 資料結構與演算法 (Data Structures & Algorithms)

如果您覺得 Git 是開發者的「工具」，那麼資料結構與演算法就是開發者的「內功」。它能幫助您寫出更有效率、更優雅的程式碼，也是衡量一位工程師解決問題能力的重要指標。

## 1. 什麼是資料結構 (Data Structures)？

簡單來說，**資料結構是組織和儲存資料的方式**。選擇正確的資料結構，可以讓你的程式在存取和操作資料時變得飛快。

在 C# 中，你其實已經天天在使用它們了！

- **`List<T>` (列表)**
  - **底層結構**：通常是動態陣列 (Array)。
  - **特性**：透過索引 (index) 存取元素非常快。但在中間插入或刪除元素比較慢，因為需要移動後面的所有元素。
  - **適用場景**：當你需要一個有序的集合，並且經常需要透過索引讀取它時。

- **`Dictionary<TKey, TValue>` (字典)**
  - **底層結構**：通常是雜湊表 (Hash Table)。
  - **特性**：透過唯一的鍵 (Key) 來新增、尋找、刪除元素，速度極快，幾乎不受資料量的影響。
  - **適用場景**：當你需要快速查找資料時，例如：根據使用者 ID 尋找使用者物件。

## 2. 什麼是演算法 (Algorithms)？

**演算法是解決特定問題的一系列明確步驟**。例如，「將一組數字由小到大排序」就是一個問題，而「氣泡排序法」或「快速排序法」就是解決這個問題的演算法。

### 如何衡量演算法的好壞？- Big O Notation

Big O 是一種數學表示法，用來描述當輸入資料量 (n) 增加時，演算法的執行時間或空間使用量的增長趨勢。

- **O(1) - 常數時間**: 最理想的狀況。不論輸入資料有多大，執行時間都一樣。
  - **範例**: 從 `Dictionary` 中透過 Key 找一個值。

- **O(n) - 線性時間**: 執行時間隨著資料量 (n) 成正比增長。
  - **範例**: 在一個 `List` 中尋找某個特定元素，最壞情況下需要從頭找到尾。

- **O(n²) - 平方時間**: 執行時間隨著資料量的平方增長。通常發生在巢狀迴圈中。應盡量避免。
  - **範例**: 在一個 List 中，為每一個元素去遍歷一次整個 List。

## 3. 思考練習 (Thinking Exercise)

這次我們不做程式碼練習，而是做一個思考題：

**情境：** 你的應用程式中有 100,000 個使用者物件。你需要頻繁地根據使用者的 `string UserId` 來查詢特定的使用者資料。

**問題：** 你會選擇用 `List<User>` 還是 `Dictionary<string, User>` 來儲存這些使用者物件？為什麼？請試著用 Big O 的概念來解釋你的選擇。

---

請花點時間思考這個問題，然後告訴我你的答案和理由。

---

# 2.3 乾淨的程式碼 (Clean Code)

> 「任何一個傻瓜都能寫出電腦可以理解的程式碼。好的程式設計師寫出的是人類可以理解的程式碼。」 - Martin Fowler

這句話完美地詮釋了 Clean Code 的精髓。它不僅僅是讓程式碼可以運作，更是關於**可讀性、可維護性**和**團隊協作**的藝術。

## 核心原則

### 1. 有意義的命名 (Meaningful Names)

變數、方法、類別的名稱應該要能清楚地表達它們的用途。

**反面教材 👎**
```csharp
// d 代表什麼?
int d; 

// p又是什麼?
var p = GetP(); 
```

**正面教材 👍**
```csharp
int elapsedTimeInDays;

var activeUsers = GetActiveUsers();
```

### 2. 函式只做一件事 (Functions Should Do One Thing)

一個函式應該只專注於完成一個單一的、明確的任務。

**反面教材 👎**
```csharp
// 這個函式做了三件事：取得資料、格式化、儲存
public void ProcessUserData(int userId)
{
    // 1. 取得資料
    var user = _database.GetUserById(userId);
    if (user == null) return;

    // 2. 格式化報告
    var report = $"User Report:\nID: {user.Id}\nName: {user.Name}";

    // 3. 儲存檔案
    File.WriteAllText($"/reports/{user.Id}.txt", report);
}
```

**正面教材 👍**
```csharp
public void ProcessUserData(int userId)
{
    var user = GetUserFromDatabase(userId);
    if (user == null) return;

    var reportContent = FormatUserReport(user);

    SaveReportToFile(user.Id, reportContent);
}

// 每個函式只做一件事
private User GetUserFromDatabase(int userId) { /* ... */ }
private string FormatUserReport(User user) { /* ... */ }
private void SaveReportToFile(int userId, string content) { /* ... */ }
```

### 3. 避免使用魔法數字 (Avoid Magic Numbers)

程式碼中不應該出現意義不明的數字或字串。

**反面教材 👎**
```csharp
// 86400 是什麼?
if (seconds > 86400) { /* ... */ } 

// 2 代表什麼狀態?
if (order.Status == 2) { /* ... */ }
```

**正面教材 👍**
```csharp
const int SecondsInADay = 86400;
if (seconds > SecondsInADay) { /* ... */ }

// 使用 Enum (列舉) 是最好的方式
public enum OrderStatus { Pending = 1, Approved = 2, Shipped = 3 }
if (order.Status == OrderStatus.Approved) { /* ... */ }
```

## 程式碼審查練習 (Code Review Exercise)

看看下面這段 C# 程式碼。它完全可以正常運作，但卻很「髒」。

**情境:** 這個函式接收一個產品列表，計算總價。如果總價超過 100 元，就打 9 折；如果超過 50 元，就打 95 折。

```csharp
public class Calculator
{
    public double Calc(List<Product> p)
    {
        double t = 0;
        foreach (var item in p)
        {
            t += item.Price;
        }

        if (t > 100)
        {
            t = t * 0.9;
        }
        else if (t > 50)
        {
            t = t * 0.95;
        }
        return t;
    }
}
```

**問題:**
根據我們剛剛學到的 Clean Code 原則，你認為這段程式碼有哪些可以改進的地方？請列出你的想法。

---

請思考一下，然後告訴我你的重構建議。

---

# 2.4 重構 (Refactoring)

如果說「乾淨的程式碼」是我們的**目標**，那麼「重構」就是我們達成這個目標所使用的**方法**。

## 1. 什麼是重構？

重構是在**不改變軟體外部行為**的前提下，對其**內部結構進行修改**，以提高程式碼的可讀性、可維護性，並降低其複雜度。

關鍵在於「不改變外部行為」。重構不是修正錯誤 (bug fixing)，也不是增加新功能。它純粹是為了讓現有的、可以運作的程式碼變得更好。

## 2. 為何及何時重構？

- **為何？**
  - 讓程式碼更容易被理解。
  - 讓未來的 bug 更容易被找到。
  - 讓新增功能變得更快、更安全。
- **何時？**
  - **三次法則 (The Rule of Three):** 當你第三次複製貼上某段程式碼時，就應該將它提煉成一個獨立的函式。
  - **新增功能前:** 先整理周邊的程式碼，讓新功能的加入更順利。
  - **程式碼審查後 (Code Review):** 根據團隊成員的建議來改善程式碼。

## 3. 核心重構技巧

我們在上一節的練習中，其實已經手動進行了重構！讓我們把這些技巧名稱化：

- **重新命名 (Rename Method/Variable)**
  - 這是最簡單也最常見的重構。將 `Calc`, `p`, `t` 重新命名為 `CalculatePriceWithDiscount`, `products`, `totalPrice` 就是這個技巧的應用。

- **提煉函式 (Extract Method)**
  - 將一段較大的程式碼片段，從原始函式中「提煉」出來，成為一個新的、獨立的函式。
  - 在 Clean Code 的「函式只做一件事」範例中，我們將處理使用者資料的巨大函式，提煉成了 `GetUserFromDatabase`, `FormatUserReport`, `SaveReportToFile` 三個小函式，這就是提煉函式。

- **引入解釋性變數 (Introduce Explaining Variable)**
  - 有時候一個判斷式或計算很複雜，讓人難以一眼看懂。可以將這個複雜的表達式，用一個有意義的變數來取代。

**反面教材 👎**
```csharp
if (user.Age > 65 && user.HasPension && !user.IsEmployed)
{
    // ... apply senior discount
}
```

**正面教材 👍**
```csharp
bool isEligibleForSeniorDiscount = user.Age > 65 && user.HasPension && !user.IsEmployed;

if (isEligibleForSeniorDiscount)
{
    // ... apply senior discount
}
```

## 總結

Clean Code 是**名詞**，是我們的目的地。Refactoring 是**動詞**，是我們前往目的地的旅程。

它們是相輔相成的概念，目標都是產出高品質、易於維護的專業級軟體。

---

這個主題是建立在 Clean Code 之上的理論，因此沒有練習。

如果您理解了「重構」是達成「乾淨程式碼」的手段這個核心概念，請告訴我，我們就可以進入下一個主題：**設計模式 (Design Patterns)**。

---

# 2.5 設計模式 (Design Patterns)

設計模式是軟體開發領域中，針對特定問題的、可重用的、經過驗證的解決方案。你可以把它們想像成是前人總結出來的「武功招式」或「建築藍圖」。你不需要自己從零開始發明輪子，而是可以站在巨人的肩膀上，用更優雅、更有效率的方式解決常見的設計問題。

## 1. 為何重要？

- **提供共同語言**: 當你和同事說「這裡可以用工廠方法」，對方立刻就能理解你的意圖，大幅提升溝通效率。
- **經過驗證的解決方案**: 這些模式歷經了無數專案的考驗，能幫助你避免常見的設計陷阱。
- **提升程式碼品質**: 正確使用設計模式，可以讓你的程式碼更有彈性、更容易維護和擴充。

## 2. 設計模式的分類

最著名的分類來自「四人幫 (Gang of Four, GoF)」的《設計模式》一書，分為三大類：

1.  **建立型 (Creational)**: 專注於「如何建立物件」，將物件的建立過程與使用者端解耦，讓程式在決定要建立哪些物件時能有更大的彈性。
2.  **結構型 (Structural)**: 專注於「如何組合物件」，透過繼承、合約 (interface) 等方式，將類別或物件組合成更大的結構，同時保持結構的彈性與效率。
3.  **行為型 (Behavioral)**: 專注於「物件之間的互動」，定義物件間的溝通方式與責任分配，讓物件可以更有效地協同工作。

---

## 建立型模式 (Creational Patterns)

### 1. 單例模式 (Singleton)

- **目的**: 確保一個類別在整個應用程式中，永遠只有一個執行個體 (instance)，並提供一個全域的存取點。
- **範例**: `AppSettings`，日誌服務 (Logging Service)。
- **注意**: 單例模式雖然簡單，但可能隱藏了類別之間的依賴關係，並對單元測試不友善。在現代 .NET 開發中，我們更常使用「依賴注入容器 (DI Container)」來管理物件的生命週期 (例如設定為 Singleton)。

```csharp
// 使用 Lazy<T> 是實現單例的現代、安全方式
public sealed class AppSettings
{
    private static readonly Lazy<AppSettings> lazyInstance = 
        new Lazy<AppSettings>(() => new AppSettings());

    public static AppSettings Instance => lazyInstance.Value;

    private AppSettings()
    {
        Console.WriteLine("AppSettings instance created.");
    }
}
```

### 2. 工廠方法模式 (Factory Method)

- **目的**: 定義一個用於建立物件的介面，但讓子類別決定要實體化哪一個類別。工廠方法讓一個類別的實體化延遲到其子類別。
- **範例**: 一個通知服務，可以根據使用者的偏好設定，決定要建立 `EmailNotifier` 還是 `SmsNotifier`。

```csharp
// 產品介面
public interface INotifier { void Notify(string message); }
// 具體產品
public class EmailNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"Email: {m}"); }
public class SmsNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"SMS: {m}"); }

// 創造者 (工廠)
public abstract class NotifierFactory
{
    public abstract INotifier CreateNotifier();
}

// 具體的創造者
public class EmailNotifierFactory : NotifierFactory
{
    public override INotifier CreateNotifier() => new EmailNotifier();
}
public class SmsNotifierFactory : NotifierFactory
{
    public override INotifier CreateNotifier() => new SmsNotifier();
}
```

### 3. 建造者模式 (Builder)

- **目的**: 將一個複雜物件的建構過程與其表示分離，使得同樣的建構過程可以建立不同的表示。
- **範例**: 建構一份複雜的報告，需要設定標題、頁首、內容、頁尾等多個部分。

```csharp
// 複雜的物件
public class Report
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
    public void Display() => Console.WriteLine($"{Title}\n{Content}\n{Footer}");
}

// Builder 介面
public interface IReportBuilder
{
    void SetTitle(string title);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

// 具體的 Builder
public class PdfReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetTitle(string title) => _report.Title = $"PDF Title: {title}";
    public void SetContent(string c) => _report.Content = c;
    public void SetFooter(string f) => _report.Footer = f;
    public Report GetReport() => _report;
}
```

---

## 結構型模式 (Structural Patterns)

### 4. 轉接器模式 (Adapter)

- **目的**: 將一個類別的介面，轉換成用戶端期望的另一個介面。Adapter 能讓原本介面不相容的類別可以合作無間。
- **範例**: 假設我們有一個新的日誌系統只接受 JSON 格式，但我們需要沿用一個舊的、只能輸出 XML 的日誌服務。

```csharp
// 舊的、不相容的服務 (Adaptee)
public class XmlLogger { public void LogXml(string data) => Console.WriteLine($"<log>{data}</log>"); }

// 新系統期望的目標介面 (Target)
public interface IJsonLogger { void LogJson(string jsonData); }

// 轉接器 (Adapter)
public class LoggerAdapter : IJsonLogger
{
    private readonly XmlLogger _xmlLogger = new XmlLogger();
    public void LogJson(string jsonData)
    {
        string xmlData = $"{{ \"json\": \"{jsonData}\" }}"; // Simplified conversion
        _xmlLogger.LogXml(xmlData);
    }
}
```

### 5. 裝飾者模式 (Decorator)

- **目的**: 動態地將額外的功能附加到一個物件上。相較於使用繼承，裝飾者模式提供了更有彈性的替代方案來擴充功能。
- **範例**: 一個基本的通知服務，我們可以動態地為它加上 SMS、Slack 等額外的通知渠道。

```csharp
// 組件介面
public interface INotifierComponent { void Send(string message); }
// 具體組件
public class BasicNotifier : INotifierComponent { public void Send(string m) => Console.WriteLine($"Basic notification: {m}"); }

// 裝飾者基底
abstract class NotifierDecorator : INotifierComponent
{
    protected INotifierComponent _component;
    public NotifierDecorator(INotifierComponent c) => _component = c;
    public virtual void Send(string m) => _component.Send(m);
}

// 具體裝飾者
class SmsDecorator : NotifierDecorator
{
    public SmsDecorator(INotifierComponent c) : base(c) { } 
    public override void Send(string m)
    {
        base.Send(m);
        Console.WriteLine($"Sent by SMS: {m}");
    }
}
// 使用: new SmsDecorator(new BasicNotifier()).Send("Hi");
```

### 6. 外觀模式 (Facade)

- **目的**: 為一個複雜的子系統提供一個單一的、簡化的介面。Facade 定義了一個更高層次的介面，讓子系統更容易使用。
- **範例**: 一個影片轉檔服務，內部可能包含讀取檔案、解析音訊、處理視訊、編碼等多個複雜步驟，但對外只提供一個 `ConvertVideo` 的簡單方法。

```csharp
// 複雜的子系統
class VideoFileHandler { public void Read(string f) => Console.WriteLine("Reading file..."); }
class AudioProcessor { public void Process() => Console.WriteLine("Processing audio..."); }
class VideoProcessor { public void Process() => Console.WriteLine("Processing video..."); }

// 外觀
public class VideoConversionFacade
{
    public void ConvertVideo(string fileName)
    {
        new VideoFileHandler().Read(fileName);
        new AudioProcessor().Process();
        new VideoProcessor().Process();
        Console.WriteLine("Conversion complete.");
    }
}
```

---

## 行為型模式 (Behavioral Patterns)

### 7. 策略模式 (Strategy)

- **目的**: 定義一系列演算法，並將每一個演算法封裝起來，讓它們可以互相替換。策略模式讓演算法的選擇可以和使用演算法的用戶端分開。
- **範例**: 一個訂單的運費計算，可以根據不同的貨運公司 (FedEx, UPS) 或地區，動態切換不同的計算策略。

```csharp
// 策略介面
public interface IShippingStrategy { decimal Calculate(Order order); }
// 具體策略
public class FedExStrategy : IShippingStrategy { public decimal Calculate(Order o) => 5.0m; }
public class UpsStrategy : IShippingStrategy { public decimal Calculate(Order o) => 6.5m; }

// Context
public class ShippingCostCalculator
{
    private IShippingStrategy _strategy;
    public void SetStrategy(IShippingStrategy s) => _strategy = s;
    public decimal CalculateShippingCost(Order order) => _strategy.Calculate(order);
}
```

### 8. 觀察者模式 (Observer)

- **目的**: 定義物件之間一種一對多的依賴關係，當一個物件的狀態改變時，所有依賴它的物件都會被通知並自動更新。
- **範例**: 一個股票價格佈告欄 (主題)，多個股民 (觀察者) 訂閱了它。當股票價格變動時，所有股民都會收到通知。

```csharp
// 觀察者介面
public interface IInvestor { void Update(Stock stock); }
// 主題
public abstract class Stock
{
    private List<IInvestor> _investors = new List<IInvestor>();
    public void Attach(IInvestor i) => _investors.Add(i);
    public void Notify() { foreach (var i in _investors) { i.Update(this); } }
}

// 具體主題
public class IbmStock : Stock { /* ... */ }
```

### 9. 責任鏈模式 (Chain of Responsibility)

- **目的**: 讓多個物件都有機會處理請求，從而避免請求的傳送者和接收者之間的耦合關係。將這些物件連成一條鏈，並沿著這條鏈傳遞該請求，直到有一個物件處理它為止。
- **範例**: 一個公司的費用報銷流程，需要經過經理、總監、副總等層層審批。

```csharp
// Handler 介面
public abstract class Approver { 
    protected Approver _successor;
    public void SetSuccessor(Approver s) => _successor = s;
    public abstract void ProcessRequest(Purchase purchase);
}

// 具體 Handler
public class Manager : Approver
{
    public override void ProcessRequest(Purchase p)
    {
        if (p.Amount < 1000) Console.WriteLine("Manager approved.");
        else if (_successor != null) _successor.ProcessRequest(p);
    }
}
// ... 其他 Approver 如 Director, VP
```

---

設計模式的學習非一蹴可幾，重點是理解每個模式背後所要解決的「問題」為何。

當您未來在開發中遇到類似的設計問題時，能夠想起「這裡似乎可以用某某模式」，便是最大的收穫。

如果您已閱讀完這份擴充指南，請告訴我，我們就可以進入本單元的最後一個主題：**SOLID 原則**。

---

# 2.6 SOLID 原則

如果說設計模式是具體的「招式」，那麼 SOLID 就是心法，是指導我們寫出良好物件導向設計的五大基本原則。遵循 SOLID 原則，可以讓我們的系統更易於維護、擴充、測試，並能更好地應對變化。

SOLID 是以下五個原則的縮寫：

- **S** - Single Responsibility Principle (單一職責原則)
- **O** - Open/Closed Principle (開放封閉原則)
- **L** - Liskov Substitution Principle (里氏替換原則)
- **I** - Interface Segregation Principle (介面隔離原則)
- **D** - Dependency Inversion Principle (依賴反轉原則)

---

### S - 單一職責原則 (Single Responsibility Principle)

> 一個類別應該只有一個改變的理由。

這意味著一個類別應該只專注於一項功能。如果一個類別有多於一個的職責，那麼其中一個職責的改變就可能會影響到另一個職責。

**反面教材 👎**
```csharp
// 這個類別有兩個職責：儲存報告內容 和 儲存報告到檔案
public class Report
{
    public string Content { get; set; }

    public void SaveToFile(string filePath)
    {
        File.WriteAllText(filePath, this.Content);
    }
}
```

**正面教材 👍**
```csharp
// 職責分離
public class Report
{
    public string Content { get; set; }
}

public class ReportPersistence
{
    public void SaveToFile(Report report, string filePath)
    {
        File.WriteAllText(filePath, report.Content);
    }
}
```

---

### O - 開放封閉原則 (Open/Closed Principle)

> 軟體實體 (類別、模組、函式等) 應該對於擴充是開放的，但對於修改是封閉的。

當需求變更時，我們應該透過增加新的程式碼來擴充功能，而不是修改現有的、已經可以運作的程式碼。

**反面教材 👎**
```csharp
// 每增加一種折扣類型，就要修改這個類別，違反了「對修改封閉」
public class DiscountCalculator
{
    public decimal Calculate(decimal price, string type)
    {
        if (type == "Standard") return price * 0.95m;
        if (type == "Premium") return price * 0.9m;
        // ... 新增 VIP, Gold 等類型時，要一直改這裡
        return price;
    }
}
```

**正面教材 👍**
```csharp
// 對於擴充是開放的：可以輕易地增加新的折扣策略類別
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal price);
}

public class StandardDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.95m;
}

public class PremiumDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.9m;
}
```

---

### L - 里氏替換原則 (Liskov Substitution Principle)

> 子類別必須可以替換掉它們的父類別，而不會產生錯誤或非預期的行為。

簡單來說，任何使用父類別的地方，都應該可以直接用其子類別來替换，而程式的功能仍然保持正常。

**反面教材 👎**
```csharp
public class Bird
{
    public virtual void Fly() => Console.WriteLine("I am flying.");
}

// 企鵝是鳥，但牠不會飛。覆寫 Fly 方法卻什麼都不做或丟出例外，
// 就違反了里氏替換原則，因為它改變了父類別的預期行為。
public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException("Penguins can't fly!");
    }
}
```

**正面教材 👍**
```csharp
// 將「會飛」這個行為抽象成獨立的介面
public class Bird { /* ... */ }
public interface IFlyable { void Fly(); }

public class Eagle : Bird, IFlyable
{
    public void Fly() => Console.WriteLine("Eagle is soaring high.");
}

public class Penguin : Bird
{
    // Penguin is a Bird, but it does not implement IFlyable.
}
```

---

### I - 介面隔離原則 (Interface Segregation Principle)

> 用戶端不應該被迫依賴它用不到的介面。

與其建立一個龐大、臃腫的介面，不如建立多個小而專一的介面。

**反面教材 👎**
```csharp
// 這個介面太臃腫了
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

// 機器人被迫實作它不需要的 Eat 和 Sleep 方法
public class Robot : IWorker
{
    public void Work() => Console.WriteLine("Robot working.");
    public void Eat() => throw new NotImplementedException();
    public void Sleep() => throw new NotImplementedException();
}
```

**正面教材 👍**
```csharp
// 將大介面拆分成小而專一的介面
public interface IWorkable { void Work(); }
public interface IFeedable { void Eat(); }

public class Human : IWorkable, IFeedable
{
    public void Work() { /* ... */ }
    public void Eat() { /* ... */ }
}

public class Robot : IWorkable
{
    public void Work() { /* ... */ }
}
```

---

### D - 依賴反轉原則 (Dependency Inversion Principle)

> 1. 高階模組不應該依賴於低階模組。兩者都應該依賴於抽象。
> 2. 抽象不應該依賴於細節。細節應該依賴於抽象。

這意味著我們的類別應該依賴「合約」(介面或抽象類別)，而不是「具體的實作」。這是實現「依賴注入 (Dependency Injection)」的核心。

**反面教材 👎**
```csharp
// 高階模組 Notification 直接依賴低階模組 EmailSender
public class EmailSender
{
    public void Send() => Console.WriteLine("Sending Email.");
}

public class Notification
{
    private EmailSender _email = new EmailSender(); // 直接 new 一個具體實作

    public void SendNotification() => _email.Send();
}
```

**正面教材 👍**
```csharp
// 兩者都依賴於抽象 IMessageSender
public interface IMessageSender
{
    void SendMessage();
}

public class EmailSender : IMessageSender
{
    public void SendMessage() => Console.WriteLine("Sending Email.");
}

// 高階模組 Notification 依賴抽象，而不是細節
public class Notification
{
    private readonly IMessageSender _sender;

    // 依賴是從外部「注入」進來的
    public Notification(IMessageSender sender)
    {
        _sender = sender;
    }

    public void SendNotification() => _sender.SendMessage();
}
```

---

SOLID 原則是高品質軟體架構的基礎。理解並實踐它們，將使你的程式碼提升到一個新的層次。

如果您已理解這五大原則，請告訴我，我們就來進行本單元的最後一項：**軟體架構 (Software Architectures)**。
```
