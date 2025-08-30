# Chapter 4: Client-Side .NET 總結

在本章中，我們全面探索了 .NET 在客戶端開發的四大核心技術，從 Web 到桌面，再到跨平台行動應用。

## 1. Razor
我們首先學習了 **Razor**，它是所有 .NET UI 技術的基石。我們掌握了其簡潔的 `@` 語法，學會如何在 HTML 中無縫嵌入 C# 程式碼、執行流程控制，並將 C# 的強大能力與 HTML 的結構優雅地結合。

## 2. Blazor
接著，我們進入了 **Blazor** 的世界，一個革命性的 Web 框架。我們探討了：
- **元件化架構**：如何將 UI 切割成可重複使用的 `.razor` 元件。
- **兩種託管模型**：
  - **Blazor Server**：在伺服器上執行邏輯，透過 SignalR 即時更新 UI。
  - **Blazor WebAssembly (Wasm)**：將 .NET 應用程式直接在瀏覽器的 WebAssembly 環境中執行，實現真正的客戶端運算。

## 3. WPF
我們將目光轉向傳統桌面，學習了 **Windows Presentation Foundation (WPF)**。這是一個成熟且強大的框架，我們了解了：
- **XAML**：一種宣告式的 UI 標記語言，用於定義豐富的 Windows UI。
- **資料繫結**：WPF 的核心功能，是實現 MVVM 設計模式的關鍵。
- **MVVM 模式**：一種將 UI (View)、邏輯 (ViewModel) 和資料 (Model) 分離的架構模式，有助於建立可維護、可測試的應用程式。

## 4. .NET MAUI
最後，我們學習了 .NET 的跨平台解決方案 **.NET MAUI**。它代表了 .NET 客戶端開發的未來，其核心理念包括：
- **單一專案，單一程式碼庫**：使用 C# 和 XAML 為 Android, iOS, macOS, 和 Windows 建立原生應用。
- **原生 UI**：將抽象的 UI 控制項轉譯為各平台對應的原生元件，確保最佳效能與使用者體驗。
- **Blazor Hybrid**：一種創新的模式，允許將 Blazor Web 元件嵌入到 MAUI 原生應用中，實現程式碼的最大化複用。

總結來說，.NET 提供了一套完整且多樣化的工具鏈，無論你的目標是 Web、桌面還是行動裝置，都能找到合適的解決方案來打造現代化、高效能的客戶端應用程式。