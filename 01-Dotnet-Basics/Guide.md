# C# 基礎語法指南 (Basics of C# 6 - 13)

> **(什麼是 C# 6 - 13?)**
>
> C# 語言本身是跟著 .NET 平台一起演進的。每個新版的 .NET 都會帶來新版的 C#。
> - **C# 6** (與 .NET Framework 4.6/.NET Core 1.0 一同發布) 加入了許多方便的「語法糖」，讓程式碼更簡潔 (例如本指南提到的字串內插)。
> - **C# 12** 是撰寫本指南時的最新版本 (與 .NET 8 一同發布)。
>
> 本指南的標題意味著我們將涵蓋從 C# 6 開始引入的現代 C# 核心語法，並一路延伸至最新的版本。

本指南將帶你了解 C# 的基礎語法。所有程式碼範例都將直接呈現在這裡，讓你專注於閱讀和理解概念。

## 1. 變數與資料型別 (Variables and Data Types)

C# 是一種強型別語言，這意味著每個變數都必須有明確的型別。

### 常用資料型別

- **整數**: `int`, `long`
- **浮點數**: `float`, `double`, `decimal`
- **布林值**: `bool` (`true` / `false`)
- **字元**: `char` (單一字元)
- **字串**: `string` (文字序列)

### 範例

```csharp
string message = "Hello, C#!"; // string
int year = 2025;             // int
bool isLearning = true;        // bool
double temperature = 28.5;     // double
char grade = 'A';              // char
decimal price = 99.95m;        // decimal (m後綴)

Console.WriteLine(message);
```

## 2. 流程控制 (Control Flow)

### if-else 條件判斷

```csharp
if (temperature > 30)
{
    Console.WriteLine("天氣炎熱！");
}
else
{
    Console.WriteLine("天氣舒適。");
}
```

### for 迴圈

```csharp
for (int i = 1; i <= 3; i++)
{
    Console.WriteLine("迴圈執行第 " + i + " 次");
}
```

## 3. 方法 (Methods)

方法是可重複使用的程式碼區塊。

```csharp
// 定義一個方法
void Greet(string name)
{
    Console.WriteLine("Hello, " + name + "!");
}

// 呼叫方法
Greet("Rain"); // 輸出: Hello, Rain!
```

## 4. C# 6+ 的語法糖 (Syntactic Sugar)

現代 C# 版本加入了許多簡潔的寫法。

### 字串內插 (String Interpolation)

這是一種比用 `+` 號串接字串更清晰的寫法。

```csharp
string name = "Rain";
int year = 2025;

// C# 6 以前
string oldWay = "Hello, " + name + ". The year is " + year + ".";

// C# 6+
string newWay = $"Hello, {name}. The year is {year}.";
Console.WriteLine(newWay);
```

### Expression-bodied members

如果方法只有一行程式碼，可以簡化寫法。

```csharp
// 傳統寫法
int Add(int a, int b)
{
    return a + b;
}

// C# 6+ 的 Expression-bodied 寫法
int SimpleAdd(int a, int b) => a + b;

Console.WriteLine(SimpleAdd(5, 3)); // 輸出: 8
```
---

你已閱讀完 C# 基礎語法的核心概念。如果都理解了，請告訴我。我會為你更新 `README.md` 的進度，然後我們再繼續下一個主題：**Dotnet CLI**。


# Dotnet CLI
# Dotnet CLI 指南

`dotnet CLI` (Command-Line Interface) 是 .NET 開發者的核心工具之一。它讓你可以直接在終端機中建立、建置、測試和發佈 .NET 應用程式，而不需要依賴圖形化介面 (如 Visual Studio)。

## 1. 核心命令 (Core Commands)

以下是幾個最常用且最重要的 `dotnet` 指令：

### `dotnet new`

用於建立新的專案或檔案。`.NET` 提供了許多內建的範本。

**範例：**
- `dotnet new console`: 建立一個新的主控台應用程式。
- `dotnet new webapi`: 建立一個新的 Web API 專案。
- `dotnet new sln`: 建立一個新的解決方案檔案 (Solution File)，用於管理多個專案。

你可以使用 `dotnet new list` 來查看所有可用的範本。

### `dotnet build`

編譯專案及其所有相依性。它會將 C# 程式碼轉換為中繼語言 (IL) 並產生一個組件 (Assembly)。

**範例：**
- `dotnet build`: 編譯目前目錄下的專案。

### `dotnet run`

這是一個方便的複合指令，它會先 `build` 專案，然後再執行它。非常適合在開發過程中快速查看結果。

**範例：**
- `dotnet run`: 建置並執行目前目錄下的專案。

### `dotnet test`

執行專案中的單元測試。你需要一個測試專案 (例如，使用 `dotnet new xunit` 建立) 才能使用此指令。

### `dotnet publish`

將應用程式打包成可部署的形式。它會包含所有執行應用程式所需的檔案。

**範例：**
- `dotnet publish -c Release`: 以 `Release` 組態建置專案，並將結果發佈到 `bin/Release/netX.X/publish/` 目錄下。`-c` 是 `--configuration` 的縮寫。

## 2. 專案與相依性管理

### `dotnet add package`

從 NuGet 套件管理器新增一個套件參考到你的專案檔 (`.csproj`)。

**範例：**
- `dotnet add package Newtonsoft.Json`: 將 `Newtonsoft.Json` 這個熱門的 JSON 處理函式庫新增到專案中。

### `dotnet add reference`

在同一個解決方案中，新增一個專案對另一個專案的參考。

### `dotnet restore`

還原專案檔中定義的所有相依性 (NuGet 套件)。通常在 `new`, `build`, `run` 等指令執行時會自動觸發，但有時需要手動修改了 `.csproj` 檔案時手動執行。

---

你已閱讀完 `Dotnet CLI` 的核心概念。

如果都理解了，請告訴我。我會為你更新 `README.md` 的進度，然後我們再繼續下一個主題：**.NET Framework**。

# .NET Framework vs. .NET Core vs. .NET (5+)

當你剛開始學習 .NET 時，可能會對 `.NET Framework`, `.NET Core`, 和現在的 `.NET 8` 等名詞感到困惑。了解它們的演進對於選擇正確的技術堆疊至關重要。

## 簡史與差異

### .NET Framework
- **元老級框架**: 這是最早的 .NET 版本，首次發行於 2002 年。
- **僅限 Windows**: 它與 Windows 作業系統深度整合，主要用於開發 Windows 桌面應用程式 (Windows Forms, WPF) 和 Web 應用程式 (ASP.NET)。
- **生命週期**: .NET Framework 4.8 是其最後一個主要版本，未來只會有安全性更新，不再有新功能。

### .NET Core
- **現代化、開源、跨平台**: 為了應對現代開發需求 (如雲端、容器化、跨平台)，Microsoft 從頭打造了 .NET Core。
- **核心特性**: 高效能、模組化、輕量級，並且可以在 Windows, macOS, 和 Linux 上執行。
- **版本**: 從 1.0 到 3.1，它為 .NET 的未來奠定了基礎。

### .NET (5 及之後版本)
- **統一的未來**: 從 .NET 5 開始，Microsoft 將 `.NET Framework` 和 `.NET Core` 的優點整合到一個統一的平台中，並簡稱為 **.NET**。
- **命名**: 不再有 "Core" 或 "Framework" 的後綴。版本號從 5、6、7、8 依序遞增，以避免與 .NET Framework 4.x 混淆。
- **當前主流**: 這是目前和未來 .NET 開發的唯一方向。所有新專案都應該使用最新的 .NET 版本 (例如 .NET 8)。

## 快速比較表

| 特性 | .NET Framework | .NET Core | .NET (5+) |
| :--- | :--- | :--- | :--- |
| **平台** | Windows | Windows, macOS, Linux | Windows, macOS, Linux, iOS, Android 等 |
| **開源** | 部分開源 | 完全開源 | 完全開源 |
| **效能** | 良好 | 優異 | 頂尖 |
| **開發模型** | WinForms, WPF, ASP.NET | ASP.NET Core, Console | ASP.NET Core, MAUI, Blazor, Console |
| **未來發展** | 維護模式 | 已被 .NET 5+ 取代 | **積極開發中** |

## 你該如何選擇？

**簡單原則：永遠選擇最新的 .NET LTS (長期支援) 版本或標準版來開始新專案。**

- **新專案**: 使用 .NET 8 (或更新版本)。
- **維護舊專案**: 如果你接手一個基於 `.NET Framework` 的舊專案，你才需要了解它的特定知識。

---

你已了解 .NET 各版本之間的差異。

如果都理解了，請告訴我。我會為你更新 `README.md` 的進度，然後我們再繼續下一個主題。

# .NET Standard 與 NuGet

這是本單元的最後一個理論主題，它們是 .NET 生態系中不可或缺的基石。

## .NET Standard

你可以把 `.NET Standard` 想像成一個「通用規格書」。

- **目的**: 在 .NET Core 和 .NET Framework 仍並存的時代，開發者需要一種方法來撰寫可以同時在這兩個平台，甚至是 Xamarin (用於手機開發) 上運行的函式庫。`.NET Standard` 就是為此而生的 API 規格。
- **運作方式**: 如果你開發一個函式庫專案，並將其目標設定為 `.NET Standard 2.0`，那麼這個函式庫就可以被任何支援 `.NET Standard 2.0` 規格的 .NET 平台所引用，例如 .NET Framework 4.6.1+、.NET Core 2.0+ 等。
- **現況**: 隨著 .NET 5 將所有平台統一，`.NET Standard` 的重要性已經降低。對於新的函式庫，你通常會直接選擇最新的 .NET 版本 (如 `.net8.0`) 作為目標。但你仍然會在許多舊的或需要廣泛相容的 NuGet 套件中看到它的身影。

## NuGet

- **.NET 的套件管理員**: NuGet 是 .NET 平台的官方套件管理器。它讓開發者可以輕鬆地建立、分享和取用可重用的 .NET 函式庫 (這些函式庫被稱為 "NuGet Packages")。
- **`nuget.org`**: 這是官方的、公開的 NuGet 套件儲存庫，上面有數以萬計的開源函式庫可供你使用。
- **與 CLI 整合**: 我們在前面 `dotnet CLI` 章節學到的 `dotnet add package Newtonsoft.Json` 指令，實際上就是從 `nuget.org` 下載 `Newtonsoft.Json` 套件，並自動將其新增到你的專案檔中。

---

恭喜！你已經完成了 .NET 基礎理論的所有核心概念！

接下來，我們將更新 `README.md` 來標記你的進度，並結束這個單元。
