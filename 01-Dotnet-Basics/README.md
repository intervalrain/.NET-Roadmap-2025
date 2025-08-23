# 單元 1: .NET 基礎 (Dotnet Basics) - 總結

本單元涵蓋了進入 .NET 開發世界所需的最核心、最基礎的知識。從 C# 語言的基礎語法到 .NET 平台的演進，再到管理專案的命令列工具，我們建立了一個穩固的知識基礎。

## 核心學習成果

在本章中，我們探討了以下幾個關鍵主題：

1.  **C# 核心語法 (C# Basics):**
    - 我們學習了 C# 作為一種強型別語言的基礎，包括變數宣告、常用的資料型別 (`string`, `int`, `bool` 等)，以及如何使用 `if-else` 和 `for` 迴圈來控制程式流程。

2.  **現代 C# 語法糖 (Modern C# Features):**
    - 了解了如何使用 C# 6+ 的特性，如**字串內插 (`$"..."`)** 和 **Expression-bodied members (`=>`)**，來撰寫更簡潔、更具可讀性的程式碼。

3.  **Dotnet CLI:**
    - 掌握了 .NET 開發的瑞士刀——`dotnet CLI`。我們學習了幾個核心指令，包括：
      - `dotnet new`: 建立新專案。
      - `dotnet build`: 編譯專案。
      - `dotnet run`: 建置並執行專案。
      - `dotnet add package`: 從 NuGet 新增外部函式庫。

4.  **.NET 平台演進 (.NET Ecosystem):**
    - 釐清了 `.NET Framework` (Windows 專用、舊版)、`.NET Core` (跨平台、現代化) 與 **.NET 5+** (統一的未來) 之間的差異。最重要的結論是：**所有新專案都應該基於最新的 .NET 版本**。

5.  **.NET Standard 與 NuGet:**
    - 理解了 `.NET Standard` 作為一個跨平台 API 規格的歷史定位，以及 `NuGet` 作為 .NET 生態系中不可或缺的套件管理器所扮演的角色。

## 實作與成果

- 我們透過 `HelloWorld`、`Variables` 和 `WorkerApp` 等專案，將理論知識付諸實踐。
- 這些專案展示了如何從無到有建立一個主控台應用程式，並了解其專案結構 (`.csproj`, `Program.cs`)。

## 下一步

恭喜你完成了 .NET 的基礎入門！這些知識是後續所有學習路徑的基石。

接下來，我們將在 **「02-General-Development-Skills」** 章節中，學習更通用的軟體開發技能，這些技能將幫助你寫出更專業、更易於維護的程式碼。