# Chapter 14.1: Observability & OpenTelemetry

## 前言

歡迎來到第十四章！在微服務和分散式系統的世界中，要理解一個請求的完整生命週期、診斷效能瓶頸或排查錯誤，變得比以往任何時候都更加困難。傳統的「監控 (Monitoring)」——即檢查已知的指標（如 CPU、記憶體）——已經不夠了。我們需要一種更深入的洞察力，這就是 **可觀測性 (Observability)**。

## 什麼是可觀測性？

可觀測性是一個系統特性，它衡量的是我們能夠從系統的 **外部輸出** 中，推斷其 **內部狀態** 的能力。換句話說，就是「我能多好地理解我的系統內部正在發生什麼，而無需修改程式碼或部署新版本？」

可觀測性通常基於三種主要的遙測資料類型，被稱為「可觀測性的三大支柱」：

1.  **日誌 (Logs)**：記錄了在特定時間點發生的、離散的事件。例如，一個請求的開始、一個錯誤的發生。
2.  **指標 (Metrics)**：在一段時間內測量的、可彙總的數值。例如，CPU 使用率、請求延遲、錯誤率。
3.  **追蹤 (Traces)**：記錄了一個請求在分散式系統中，從開始到結束所經過的所有服務和元件的完整路徑。追蹤對於理解效能瓶頸和錯誤在系統中的傳播路徑至關重要。

![Observability Pillars](https://i.imgur.com/3tG3Y7a.png)

## 問題：廠商鎖定 (Vendor Lock-in)

過去，每家可觀測性工具供應商（如 Datadog, New Relic, Dynatrace）都有自己專有的代理 (Agent) 和資料格式。如果你的應用程式使用了某一家廠商的工具，那麼未來想要更換供應商將會非常困難，因為你需要修改所有產生遙測資料的程式碼。這就是「廠商鎖定」。

## 解決方案：OpenTelemetry

**OpenTelemetry (OTel)** 是一個開源的、由雲端原生運算基金會 (CNCF) 維護的專案，它旨在為可觀測性資料（日誌、指標、追蹤）的產生、收集和匯出，提供一套 **統一的、與供應商無關的標準、API 和 SDK**。

**OpenTelemetry 的核心思想：**
- **統一的儀器化 (Instrumentation)**：你的應用程式程式碼只需要使用 OpenTelemetry 的 API 來產生遙測資料。
- **可插拔的匯出器 (Exporter)**：你可以設定不同的「匯出器」，將收集到的標準格式的遙T測資料，發送到任何你選擇的後端分析工具（如 Prometheus, Jaeger, Datadog, 或本機主控台）。

這意味著，你可以隨時更換後端分析工具，而 **無需修改任何一行應用程式的儀器化程式碼**，徹底解決了廠商鎖定的問題。

## .NET 中的 OpenTelemetry

.NET 平台從 .NET 6 開始，就深度整合了 OpenTelemetry。許多核心函式庫（如 ASP.NET Core, HttpClient）都已經內建了對 OpenTelemetry 的支援。

設定 OpenTelemetry 通常在 `Program.cs` 中完成：

```csharp
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("MyWebApp"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter() // 將追蹤匯出到主控台
        .AddOtlpExporter()    // 將追蹤匯出到一個 OTLP 相容的後端
    )
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter()
    );
```

## 結語

可觀測性是維運現代分散式系統的必備能力。OpenTelemetry 透過提供一套開放的、統一的標準，將開發人員從特定工具的鎖定中解放出來，讓建構可觀測的應用程式變得前所未有的簡單和靈活。它是雲端原生時代的基石之一。

在接下來的章節中，我們將介紹一些具體的開源工具，用於收集、儲存和視覺化由 OpenTelemetry 產生的遙測資料。

---

# Chapter 14.2: Prometheus & Grafana

## 前言

在可觀測性的三大支柱中，「指標 (Metrics)」對於理解系統的宏觀健康狀況和趨勢至關重要。在開源世界中，**Prometheus** 和 **Grafana** 的組合是收集、儲存、查詢和視覺化指標的黃金標準。

## Prometheus

Prometheus 是一個開源的系統監控和警報工具集。它的核心是一個 **時間序列資料庫 (Time Series Database, TSDB)**。

**核心工作模式：拉取模型 (Pull Model)**
- Prometheus 的工作方式與大多數監控系統不同。它不是被動地等待應用程式將指標「推送」給它，而是主動地、週期性地從應用程式的特定 HTTP 端點（通常是 `/metrics`）上「拉取 (scrape)」指標資料。
- 這種模式的優點是，Prometheus 的設定是集中式的，你可以輕鬆地控制要監控哪些目標，而無需修改每一個應用程式的設定。

**.NET 應用中的 Prometheus**
要在 .NET 應用中暴露 Prometheus 指標端點，你可以使用 `OpenTelemetry.Exporter.Prometheus.AspNetCore` 這個 NuGet 套件。

```csharp
// 在 Program.cs 中
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        // ... 其他設定
        .AddPrometheusExporter()); // 新增 Prometheus 匯出器

// ...
var app = builder.Build();
app.MapPrometheusScrapingEndpoint(); // 啟用 /metrics 端點
```

## Grafana

雖然 Prometheus 提供了強大的資料收集和查詢能力（透過其查詢語言 PromQL），但其內建的視覺化功能非常基礎。這就是 **Grafana** 發揮作用的地方。

Grafana 是一個開源的、領先的視覺化和分析平台。你可以將 Grafana 連接到各種不同的資料來源（不僅僅是 Prometheus，還包括 Loki, Elasticsearch, SQL 資料庫等），然後建立豐富、互動式的儀表板 (Dashboard)。

**Prometheus + Grafana 的組合：**
1.  你的 .NET 應用程式透過 OpenTelemetry SDK 產生指標。
2.  Prometheus Exporter 將這些指標以特定格式暴露在 `/metrics` 端點上。
3.  Prometheus Server 定期從你的應用程式拉取指標，並將其儲存在其時間序列資料庫中。
4.  Grafana 將 Prometheus 設定為其資料來源。
5.  你在 Grafana 中使用 PromQL 查詢 Prometheus 中的資料，並建立各種圖表（如折線圖、儀表盤、熱力圖）來視覺化你的應用程式指標。

![Prometheus & Grafana](https://i.imgur.com/YgLgG6e.png)

## 結語

Prometheus 和 Grafana 的組合為 .NET 開發人員提供了一套完全開源、功能極其強大的指標監控和視覺化解決方案。透過 OpenTelemetry 的標準化儀器化，你可以輕鬆地將你的應用程式指標接入這個強大的生態系統，從而深入洞察你的應用程式效能和健康狀況。

---

# Chapter 14.3: Loki & Promtail

## 前言

我們已經解決了指標的問題，但可觀測性的另一大支柱是「日誌 (Logs)」。傳統的日誌解決方案（如 ELK Stack）功能強大，但通常資源消耗巨大且設定複雜。Grafana Labs 受到 Prometheus 的啟發，創造了一個更輕量、更易於整合的日誌聚合系統：**Loki**。

## Loki 的核心思想

Loki 的設計哲學是：「像 Prometheus 一樣，但用於日誌」。

- **不對日誌內容進行全文索引**：這是 Loki 與 Elasticsearch 等系統最根本的區別。傳統日誌系統會對日誌的 **完整內容** 建立索引，這使得它們搜尋速度快，但也導致了巨大的儲存和記憶體開銷。Loki 反其道而行，它只對一組與日誌流相關的 **標籤 (Labels)** 進行索引（例如 `app="my-api"`, `level="error"`）。
- **依賴標籤進行查詢**：查詢時，你首先使用這些標籤來快速定位到相關的日誌流，然後 Loki 會對這些日誌流的內容進行即時的 `grep` (文本搜尋)。

這種設計的取捨使得 Loki 的儲存成本極低，且架構非常簡單。

## Promtail

Promtail 是 Loki 的官方日誌收集代理。它的工作是：
1.  發現日誌來源（例如，本地的日誌檔案、Kubernetes Pod 的日誌）。
2.  從日誌中提取並附加標籤。
3.  將日誌流推送到 Loki 伺服器。

## Loki + Grafana

Loki 與 Grafana 的整合是無縫的。你可以在 Grafana 中將 Loki 新增為一個資料來源，然後：
- 使用 **LogQL** (Loki Query Language) 來查詢你的日誌。
- 在 Grafana 的「Explore」視圖中，並排檢視來自 Prometheus 的指標和來自 Loki 的日誌。你可以根據時間範圍，將圖表中的效能尖峰與對應時間點的錯誤日誌關聯起來，這對於問題排查極其強大。

![Loki & Grafana](https://i.imgur.com/sF7g8aJ.png)

## 結語

Loki 為日誌聚合提供了一個高成本效益、易於維運的解決方案。它與 Prometheus 的理念一脈相承，並與 Grafana 完美整合，讓你能夠在同一個儀表板上，將指標和日誌這兩種關鍵的遙測資料關聯起來，極大地提升了問題排查的效率。

---

# Chapter 14.4: Portainer

## 前言

在我們深入研究了監控和可觀測性的各種工具之後，讓我們來看一個不同的但同樣重要的主題：容器管理。當你在本地開發或在單一主機上執行多個 Docker 容器時，使用命令列 (`docker ps`, `docker logs` 等) 來管理它們可能會變得非常繁瑣。**Portainer** 提供了一個圖形化的使用者介面 (GUI)，讓容器管理變得直觀而簡單。

## 什麼是 Portainer？

Portainer 是一個輕量級、開源的容器管理平台。它支援 Docker, Docker Swarm, Kubernetes 等多種容器環境。透過其友善的 Web 介面，你可以輕鬆地：

- **視覺化容器和映像**：查看正在執行的容器、它們的狀態、佔用的資源，以及本地存在的所有 Docker 映像。
- **管理容器生命週期**：啟動、停止、重啟、暫停、恢復和刪除容器。
- **查看日誌和統計資訊**：直接在 Web 介面中查看容器的即時日誌，以及 CPU、記憶體、網路的使用情況圖表。
- **進入容器終端**：直接在瀏覽器中打開一個到容器內部的 shell，方便進行偵錯。
- **管理網路和磁碟區 (Volumes)**：建立和刪除 Docker 網路和磁碟區。
- **部署應用 (Stacks)**：使用 Docker Compose 檔案來定義和部署多容器應用。

## 安裝與使用

Portainer 本身也是以一個 Docker 容器的形式執行的，安裝非常簡單：

```bash
# 1. 建立 Portainer 用於儲存資料的磁碟區
docker volume create portainer_data

# 2. 下載並啟動 Portainer 伺服器容器
docker run -d -p 8000:8000 -p 9443:9443 --name portainer \
    --restart=always \
    -v /var/run/docker.sock:/var/run/docker.sock \
    -v portainer_data:/data \
    portainer/portainer-ce:latest
```

安裝完成後，你就可以透過瀏覽器存取 `https://localhost:9443` 來使用 Portainer 了。

## 結語

Portainer 是一個非常實用的工具，特別是對於剛開始接觸 Docker 和容器的開發人員。它極大地降低了容器管理的複雜性，讓你能夠透過一個直觀的圖形化介面，快速地了解和操作你的容器環境。雖然在大型的、自動化的生產環境中，你可能更依賴於命令列工具和 CI/CD 管線，但在開發、測試和小型部署場景中，Portainer 是一個能顯著提升效率的利器。
