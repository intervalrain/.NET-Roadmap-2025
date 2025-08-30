# Chapter 4.1: Razor

## 前言

歡迎來到 .NET 客戶端開發的世界！在我們深入研究 Blazor、MAUI 等現代 UI 框架之前，我們必須先掌握它們共同的基礎：**Razor**。Razor 是一種強大的樣板標記語法 (template markup syntax)，用於將基於伺服器的程式碼嵌入到網頁中。它最初是為 ASP.NET MVC 設計的，但現在已成為所有 .NET UI 技術的核心。

## 什麼是 Razor？

Razor 的核心思想是讓你在 HTML 中無縫地編寫 C# 程式碼。它使用 `@` 符號作為從 HTML 切換到 C# 的標記。Razor 引擎會解析這些檔案 (`.cshtml`)，執行其中的 C# 程式碼，並動態生成最終的 HTML，然後將其傳送給使用者的瀏覽器。

**主要優點：**
- **簡潔流暢**：語法緊湊且易於學習，讓 HTML 和 C# 程式碼的結合看起來非常自然。
- **易於學習**：如果你已經了解 HTML 和 C#，那麼學習 Razor 會非常快。
- **智慧感知 (IntelliSense)**：Visual Studio 和 VS Code 為 Razor 提供了強大的 IntelliSense 支援，包括程式碼完成、錯誤檢查和重構。
- **強大且靈活**：你可以使用所有 C# 的功能，從簡單的變數顯示到複雜的條件 lógica 和迴圈。

## Razor 語法基礎

### 1. `@` 符號

`@` 是 Razor 的魔法棒。它告訴 Razor 引擎：「嘿，接下來是 C# 程式碼了！」

- **單行表達式 (Inline Expressions)**: 用於輸出來自變數或方法的文字。
  ```csharp
  <p>Hello, my name is @Model.Name and today is @DateTime.Now.ToString("yyyy-MM-dd").</p>
  ```

- **程式碼區塊 (Code Blocks)**: 用於執行多行 C# 程式碼。
  ```csharp
  @{
      var name = "John Doe";
      var age = 30;
      var message = $"Name: {name}, Age: {age}";
  }
  <p>@message</p>
  ```

### 2. 隱含與明確表達式 (Implicit and Explicit Expressions)

- **隱含 Razor 表達式**: 以 `@` 開頭，後面跟著 C# 程式碼。Razor 通常足夠聰明，可以自動檢測表達式的結束位置。
  ```csharp
  <p>@Model.Name</p> 
  ```

- **明確 Razor 表達式**: 如果表達式包含空格或特殊字元，你需要用括號 `()` 將其包圍。
  ```csharp
  <p>@(Model.FirstName + " " + Model.LastName)</p>
  ```

### 3. 流程控制 (Control Structures)

你可以直接在 HTML 中使用標準的 C# 流程控制陳述式，如 `if`, `else`, `switch`, `for`, `foreach`, `while`。

- **條件判斷 (`if/else`)**:
  ```csharp
  @if (Model.IsAdmin)
  {
      <p>Welcome, Administrator!</p>
  }
  else
  {
      <p>Welcome, User!</p>
  }
  ```

- **迴圈 (`foreach`)**:
  ```csharp
  <ul>
      @foreach (var product in Model.Products)
      {
          <li>@product.Name - @product.Price.ToString("C")</li>
      }
  </ul>
  ```

### 4. 註解

- **Razor 註解**: 使用 `@* ... *@`。這些註解在伺服器上處理，不會出現在最終的 HTML 輸出中。
  ```csharp
  @* 這是一個伺服器端註解，使用者看不到 *@
  ```

- **HTML 註解**: 標準的 `<!-- ... -->`。這些註解會被傳送到瀏覽器。

## 結語

Razor 是 .NET Web 開發中不可或缺的一項基礎技術。它優雅地將 C# 的強大功能與 HTML 的結構結合在一起。無論你未來是使用 MVC、Razor Pages 還是 Blazor，對 Razor 的熟練掌握都將是成功的關鍵。

在下一個章節，我們將探討 **Blazor**，看看 Razor 如何被用來建構功能豐富的單頁應用程式 (SPA)，甚至完全在客戶端執行 .NET 程式碼！

---

# Chapter 4.2: Blazor

## 前言

現在你已經掌握了 Razor 語法，讓我們來看看它最令人興奮的應用之一：**Blazor**。Blazor 是一個現代化的 Web UI 框架，它允許你使用 C# 和 .NET 來建構互動式的客戶端 Web 應用程式，徹底改變了 .NET 開發人員建立 Web 應用的方式。

## 什麼是 Blazor？

傳統上，Web 前端開發由 JavaScript 主導。如果你想在瀏覽器中執行程式碼，就必須使用 JavaScript。Blazor 打破了這個規則。它讓你可以用 C# 取代 JavaScript 來撰寫前端邏輯。

**核心概念：元件 (Components)**
Blazor 應用程式是由一個個可重複使用的 UI 元件所組成。一個元件就是一個 `.razor` 檔案，其中包含了：
- **HTML 結構**：定義元件的視覺外觀。
- **C# 邏輯**：處理事件、管理狀態、執行運算。
- **CSS 樣式**：定義元件的樣式。

這些元件可以被巢狀嵌套、重複使用，並在元件之間傳遞參數，形成複雜的 UI。

一個簡單的計數器元件 (`Counter.razor`):
```csharp
<h1>Counter</h1>

<p>Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```
在這個範例中：
- `@currentCount` 顯示 C# 變數的值。
- `@onclick` 將按鈕的點擊事件綁定到 C# 的 `IncrementCount` 方法。
- `@code` 區塊包含了元件的 C# 程式碼。

當使用者點擊按鈕時，`IncrementCount` 方法會執行，`currentCount` 的值會增加，Blazor 會自動偵測到這個變化，並只更新畫面上 `<p>` 標籤中的數字，而不需要重新載入整個頁面。

## Blazor 託管模型 (Hosting Models)

Blazor 提供了兩種主要的託管模型，讓你可以根據應用程式的需求選擇最適合的方案。

### 1. Blazor Server

- **運作方式**：UI 元件在伺服器上執行。當使用者與頁面互動時 (例如點擊按鈕)，事件會透過一個名為 **SignalR** 的即時通訊管道傳送到伺服器。伺服器處理事件，更新元件狀態，然後計算出 UI 的變動部分，再將這些變動傳回瀏覽器進行更新。
- **優點**：
    - **載入速度快**：初始下載的檔案非常小。
    - **安全性高**：應用程式的程式碼保留在伺服器上。
    - **完整 .NET 功能**：可以直接存取所有伺服器端的 .NET API。
- **缺點**：
    - **需要網路連線**：與伺服器的連線中斷會導致應用程式停止運作。
    - **延遲**：每次互動都需要一次網路來回，可能會有效能瓶頸。

### 2. Blazor WebAssembly (Wasm)

- **運作方式**：整個應用程式 (包括 C# 程式碼和 .NET 執行環境) 都會被編譯成 **WebAssembly** 格式，並下載到使用者的瀏覽器中執行。應用程式完全在客戶端運作，就像傳統的 JavaScript SPA (單頁應用程式) 一樣。
- **優點**：
    - **離線運作**：一旦下載完成，應用程式可以在沒有網路連線的情況下執行。
    - **低延遲**：所有 UI 互動都在瀏覽器中立即處理，反應速度快。
    - **靜態部署**：可以將應用程式部署到任何靜態網站託管服務 (如 GitHub Pages)。
- **缺點**：
    - **初始載入慢**：需要下載 .NET 執行環境和應用程式本身，檔案較大。
    - **瀏覽器限制**：受限於瀏覽器的沙箱環境，無法直接存取某些系統資源。

## 結語

Blazor 為 .NET 開發人員開啟了通往現代 Web 開發的大門，讓我們可以使用熟悉的語言和工具來建構高效能、互動式的 Web UI。無論你選擇 Blazor Server 的即時性，還是 Blazor WebAssembly 的客戶端能力，你都在使用 C# 和 Razor 元件來打造卓越的使用者體驗。

在下一個章節，我們將暫時離開 Web，探索如何使用 .NET 來建構傳統的桌面應用程式，從 **WPF** 開始。

---

# Chapter 4.3: WPF

## 前言

在探索了 Web 之後，我們將目光轉向桌面。**Windows Presentation Foundation (WPF)** 是一個歷史悠久但功能依然強大的 UI 框架，專門用於建立功能豐富的 Windows 桌面應用程式。它提供了對 UI、圖形和媒體的精細控制能力。

## 什麼是 WPF？

WPF 的核心是一種名為 **XAML** (Extensible Application Markup Language) 的宣告式 XML 標記語言。你可以使用 XAML 來定義應用程式的 UI 結構，而使用 C# 來撰寫其背後的商業邏輯。

**主要特點：**
- **宣告式 UI (XAML)**：將 UI 的「外觀」與「行為」分離，使設計師和開發人員可以更好地協作。
- **強大的資料繫結 (Data Binding)**：能夠輕鬆地將 UI 控制項與資料物件同步，是 MVVM 設計模式的基礎。
- **樣式與範本 (Styling and Templating)**：可以完全自訂控制項的外觀和行為，而無需繼承它們。
- **向量圖形**：UI 是基於向量的，這意味著它可以無限縮放而不會失真。
- **硬體加速**：利用現代顯示卡的強大功能來呈現流暢的動畫和複雜的視覺效果。

一個簡單的 WPF 視窗 (`MainWindow.xaml`):
```xml
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="450" Width="800">
    <StackPanel>
        <TextBlock Text="{Binding Greeting}" FontSize="24" HorizontalAlignment="Center" />
        <Button Content="Click Me" Click="Button_Click" />
    </StackPanel>
</Window>
```
對應的 C# 程式碼 (`MainWindow.xaml.cs`):
```csharp
public partial class MainWindow : Window
{
    public string Greeting { get; set; } = "Hello, WPF!";

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Button was clicked!");
    }
}
```

## MVVM 設計模式

WPF 的開發與 **Model-View-ViewModel (MVVM)** 設計模式緊密相關。這個模式有助於將 UI (View) 與其邏輯 (ViewModel) 和資料 (Model) 分離。
- **Model**：代表應用程式的資料和業務邏輯 (例如 `User`, `Product`)。
- **View**：代表 UI (XAML 檔案)。它透過資料繫結與 ViewModel 互動。
- **ViewModel**：作為 View 和 Model 之間的中介。它從 Model 中取得資料，並將其轉換為 View 可以顯示的格式。它也處理來自 View 的命令 (例如按鈕點擊)。

## 結語

WPF 雖然是一個成熟的框架，但它在需要高度自訂視覺效果和複雜互動的 Windows 企業級應用中仍然佔有一席之地。學習 WPF 不僅能讓你建立強大的桌面應用，更能讓你深入理解資料繫結和 MVVM 等核心概念，這些概念在許多現代 UI 框架中都至關重要。

接下來，我們將探索 .NET 的終極目標：一次編寫，到處執行。歡迎來到 **.NET MAUI** 的世界。

---

# Chapter 4.4: .NET MAUI

## 前言

歡迎來到 .NET 客戶端開發的未來！**.NET Multi-platform App UI (.NET MAUI)** 是 .NET 平台的演進，它實現了一個長久以來的夢想：使用單一專案和單一程式碼庫，為 Android、iOS、macOS 和 Windows 建立原生應用程式。

## 什麼是 .NET MAUI？

.NET MAUI 是 Xamarin.Forms 的下一代產品，經過了全面的重新架構，以提高效能和擴充性。它允許開發人員共享更多的程式碼，同時提供對每個平台原生功能的深度存取。

**核心理念：**
- **單一專案**：一個專案管理所有平台的資源，如字型、圖片、樣式等。
- **單一程式碼庫**：使用 C# 和 XAML 共享 UI 和後端邏輯。
- **原生 UI**：.NET MAUI 會將你定義的抽象 UI (例如 `<Button>`) 對應到每個平台的原生控制項 (Android 的 `Button`，iOS 的 `UIButton` 等)。這確保了應用程式在每個平台上都有一流的效能和外觀。
- **平台 API 存取**：當需要時，你可以輕易地呼叫特定平台的 API，以實現無法在通用層面完成的功能。

一個 .NET MAUI 頁面 (`MainPage.xaml`):
```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiApp.MainPage">

    <VerticalStackLayout Spacing="25" Padding="30">
        <Label Text="Hello, .NET MAUI!" FontSize="32" HorizontalOptions="Center" />
        
        <Button Text="Click me"
                Clicked="OnCounterClicked"
                HorizontalOptions="Center" />
    </VerticalStackLayout>

</ContentPage>
```
與 Blazor 和 WPF 類似，事件處理和邏輯都在 C# 程式碼後置檔案 (`MainPage.xaml.cs`) 中完成。

## Blazor Hybrid

.NET MAUI 還帶來了一種令人興奮的可能性：**Blazor Hybrid**。你可以將 Blazor 元件直接嵌入到 .NET MAUI 應用程式中。這意味著：
- 你可以在原生桌面和行動應用中，重複使用為 Web 開發的 Blazor 元件。
- 你的應用可以混合使用原生 UI 控制項和基於 Web 技術的 Blazor 元件。
- Blazor 元件在這種模式下執行於本機，並透過一個內部通道渲染到 `WebView2` (Windows) 或 `WKWebView` (macOS/iOS) 控制項中，擁有對原生功能的完整存取權限。

## 結語

.NET MAUI 是 .NET 在客戶端開發領域的集大成者。它整合了過去的經驗，提供了一個統一、現代且高效的框架，讓開發人員能夠用一套技能觸及最廣泛的裝置平台。無論是開發純原生應用，還是結合 Blazor 的 Hybrid 應用，.NET MAUI 都為 .NET 開發人員提供了前所未有的靈活性和強大功能。

完成本章後，你已經對 .NET 在客戶端開發的四大支柱 (Razor, Blazor, WPF, .NET MAUI) 有了全面的了解。現在，你可以根據專案需求，選擇最適合的工具來打造卓越的使用者體驗。
