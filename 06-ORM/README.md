# Chapter 6: ORM 總結

在本章中，我們深入探討了物件關聯對應 (Object-Relational Mapping, ORM) 的世界，學習瞭如何在 .NET 應用程式中優雅、高效地與資料庫進行互動。

## 1. LINQ
我們首先學習了 **LINQ (Language-Integrated Query)**，這是 .NET 中一項革命性的功能。它將查詢能力直接整合到 C# 語言中。
- **核心優勢**：我們了解了 LINQ 如何提供統一的查詢語法、編譯時型別檢查和智慧感知 (IntelliSense)，從而讓程式碼更安全、更易讀。
- **兩種語法**：我們比較了類似 SQL 的「查詢語法」和基於 Lambda 運算式的「方法語法」。
- **延遲執行 (Deferred Execution)**：我們掌握了 LINQ 的核心特性，即查詢只在需要時才執行，這有助於提升效能和靈活性。

## 2. Entity Framework Core
接著，我們學習了 .NET 中最主流的全功能 ORM——**Entity Framework Core (EF Core)**。它在開發效率和功能豐富性之間取得了極佳的平衡。
- **核心概念**：我們熟悉了 `DbContext` (資料庫會話)、`DbSet<T>` (資料表) 和實體 (Entity) 的概念。
- **強大功能**：我們探討了 EF Core 如何將 LINQ 查詢自動轉換為 SQL，並介紹了其強大的「資料庫遷移 (Migrations)」功能，用於安全地管理資料庫結構的演進。
- **開發效率**：透過 CRUD 範例，我們看到了 EF Core 如何讓開發人員從繁瑣的 SQL 中解放出來，專注於業務邏輯。

## 3. Dapper
最後，我們介紹了一個輕量級的 Micro ORM——**Dapper**。它以極致的效能和對 SQL 的完全掌控為主要特點。
- **效能至上**：Dapper 的執行速度接近原生的 ADO.NET，是讀取密集型和高效能場景下的絕佳選擇。
- **SQL 的掌控力**：與 EF Core 不同，Dapper 鼓勵開發人員手寫和優化 SQL，同時簡化了參數傳遞和結果對應的過程。
- **工具對比**：我們比較了 Dapper 和 EF Core 的適用場景，並了解到在現代架構（如 CQRS）中，兩者常被結合使用，以兼顧開發效率和執行效能。

總結來說，本章為你提供了在 .NET 中進行資料庫存取的完整工具箱。掌握 LINQ 的優雅、EF Core 的高效開發以及 Dapper 的極致效能，將使你能夠根據不同專案的需求，選擇最合適的資料存取策略，打造出既穩健又高效的應用程式。