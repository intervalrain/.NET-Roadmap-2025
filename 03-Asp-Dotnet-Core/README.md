# Chapter 3: ASP.NET Core - 總結與回顧

恭喜！你已經完成了 ASP.NET Core 的深度探索之旅。在本章節中，我們從最基礎的 Web 運作原理開始，一路建構到功能完整、安全可靠的現代 Web API。這份文件將對本章的核心概念進行總結，並鞏固你的所學。

## 1. Web 運作原理 (Web Fundamentals)

我們首先打下了堅實的基礎，理解了支撐整個網際網路的基石：

-   **主從式架構 (Client-Server Model)**: 理解了瀏覽器（客戶端）如何向伺服器發送請求，以及伺服器如何回傳回應。
-   **DNS & HTTP(S)**: 學習了 DNS 如何將域名解析為 IP 位址，以及 HTTP/HTTPS 作為客戶端與伺服器之間通訊的語言，其請求和回應的結構。
-   **一個完整的請求流程**: 我們將所有概念串連起來，完整地走過了一遍從在瀏覽器輸入 URL 到頁面最終呈現的全過程，這對於理解問題排查至關重要。

## 2. ASP.NET Core 核心 (Core Concepts)

接下來，我們深入 ASP.NET Core 框架的內部，掌握了其核心建構模組：

-   **Host & Kestrel**: 了解了應用程式的啟動器 (Host) 和高效能的內建 Web 伺服器 (Kestrel)。
-   **中介軟體 (Middleware)**: 掌握了構成請求處理管線 (Request Pipeline) 的核心概念，理解了其順序的重要性。
-   **依賴注入 (Dependency Injection)**: 學習了 ASP.NET Core 的核心設計模式，以及 `Transient`, `Scoped`, `Singleton` 三種服務生命週期。
-   **設定 (Configuration)**: 探索了從 `appsettings.json`、環境變數等多個來源讀取設定的靈活機制，以及使用強型別的選項模式 (Options Pattern)。
-   **路由 (Routing)**: 學習了如何將傳入的 URL 請求對應到正確的處理程式，包括約定式路由和屬性路由。
-   **錯誤處理 (Error Handling)**: 掌握了如何在開發和生產環境中優雅地處理例外，以及為 API 提供標準化的錯誤回應 (Problem Details)。

## 3. 應用程式模型 (Application Models)

我們探索了使用 ASP.NET Core 建構應用程式的兩種主要方式：

-   **ASP.NET Core MVC**: 學習了經典的 Model-View-Controller 設計模式，它透過分離關注點來組織大型應用程式。我們也實作了 **Filters**，這是在 Action 執行前後添加共通邏輯的強大工具。
-   **Web API & Minimal APIs**: 我們專注於建構 HTTP 服務。首先學習了使用 `ControllerBase` 和 `[ApiController]` 的傳統 Web API，然後探索了 .NET 6+ 中引入的、更簡潔的 Minimal APIs，並比較了它們的適用場景。

## 4. API 設計與通訊 (API Design & Communication)

在能夠建構 API 的基礎上，我們學習了如何設計出優秀的 API：

-   **REST**: 學習了設計可擴展 Web 服務的六大架構指導原則，理解了什麼是真正的 "RESTful"。
-   **JSON**: 掌握了現代 API 最常用的資料交換格式，以及如何在 ASP.NET Core 中使用 `System.Text.Json` 進行序列化和反序列化。
-   **GraphQL**: 探索了作為 REST 替代方案的查詢語言，它允許客戶端精確地請求所需的資料，解決了 Over-fetching 和 Under-fetching 的問題。

## 5. 安全性 (Security)

最後，我們為應用程式加上了至關重要的安全防護：

-   **驗證 (Authentication) vs. 授權 (Authorization)**: 釐清了「你是誰」和「你能做什麼」的根本區別。
-   **驗證機制**: 學習了適用於傳統 Web 應用的 **Cookie 驗證**和適用於現代 API 的**權杖 (Token) 驗證**。
-   **JWT (JSON Web Token)**: 深入了解了 JWT 的三段式結構、聲明 (Claims) 和簽章機制，它是現代無狀態驗證的核心。
-   **授權策略**: 從簡單的 `[Authorize]` 和基於角色的授權，到更強大、更靈活的**基於策略的授權**，以及完全自訂的授權需求。
-   **OAuth 2.0 & OIDC**: 理解了用於**授權委託**的 OAuth 2.0 標準，以及在其之上建立的**身分驗證**層 OpenID Connect，它們是實現第三方登入和服務整合的基石。

---

你已經為使用 ASP.NET Core 建構任何類型的 Web 應用或服務打下了堅實的基礎。做得非常好！接下來，我們將把目光轉向客戶端，開始 **Client-Side .NET** 的學習之旅。