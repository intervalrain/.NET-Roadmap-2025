# Chapter 5.1: Database Design

## 前言

歡迎來到第五章！在現代軟體開發中，幾乎所有應用程式都需要與資料互動。如何有效地儲存、組織和檢索這些資料，是決定應用程式效能、可擴展性和可維護性的關鍵。這一切都始於良好的 **Database Design**。

## 為什麼資料庫設計如此重要？

想像一下，如果你的衣櫃裡的衣服全部亂七八糟地堆在一起，每次找一件特定的T恤都像大海撈針。糟糕的資料庫設計就像這樣，會導致：
- **資料冗餘 (Data Redundancy)**：相同的資訊被重複儲存在多個地方，浪費空間且難以維護。
- **資料不一致 (Data Inconsistency)**：更新資料時，如果只更新了其中一處，就會導致版本衝突和錯誤。
- **查詢緩慢 (Slow Queries)**：隨著資料量增長，從結構不良的資料庫中檢索資訊會變得極其緩慢。
- **難以擴展 (Difficult to Scale)**：當需要新增功能或修改現有結構時，會變得非常困難和危險。

一個好的資料庫設計，就像一個分門別類、井井有條的衣櫃，可以確保資料的完整性、一致性，並提供高效的存取。

## 核心概念：關聯式資料庫 (Relational Databases)

雖然 NoSQL 資料庫越來越流行，但關聯式資料庫（如 SQL Server, PostgreSQL）仍然是許多應用的首選。其核心思想是將資料儲存在由「行」和「欄」組成的「資料表 (Table)」中，並在這些資料表之間建立「關聯 (Relationship)」。

### 1. 資料表 (Tables)、欄位 (Columns) 和資料列 (Rows)
- **資料表 (Table)**：代表一個實體，例如 `Users`, `Products`, `Orders`。
- **欄位 (Column/Field)**：代表實體的一個屬性，例如 `Users` 表中的 `Id`, `Username`, `Email`。每個欄位都有一個特定的資料型別 (如 `int`, `string`, `datetime`)。
- **資料列 (Row/Record)**：代表實體的一個具體實例，例如 `Users` 表中的一筆使用者記錄。

### 2. 主鍵 (Primary Key)
每個資料表都應該有一個 **主鍵**。它是一個（或一組）能唯一識別資料表中每一筆紀錄的欄位。
- **特性**：值必須是唯一的，且不能是 `NULL`。
- **範例**：`Users` 表中的 `Id` 欄位。

### 3. 外鍵 (Foreign Key)
**外鍵** 是用於建立和維護兩個資料表之間關聯的鍵。一個資料表中的外鍵，會對應到另一個資料表的主鍵。
- **範例**：假設我們有一個 `Orders` 表，其中有一個 `UserId` 欄位。這個 `UserId` 就是一個外鍵，它參考到 `Users` 表的 `Id` 主鍵，表示這個訂單是屬於哪個使用者的。

### 4. 關聯 (Relationships)
- **一對一 (One-to-One)**：例如，一個 `User` 對應一個 `UserProfile`。
- **一對多 (One-to-Many)**：最常見的關聯。例如，一個 `User` 可以有多個 `Orders`。
- **多對多 (Many-to-Many)**：例如，一個 `Product` 可以屬於多個 `Categories`，一個 `Category` 也可以包含多個 `Products`。這種關聯通常需要一個額外的「連結表 (Junction Table)」來實現（例如 `ProductCategories` 表）。

## 正規化 (Normalization)

正規化是組織資料庫中欄位和資料表的過程，旨在最小化資料冗餘。它有一系列的「範式 (Normal Forms, NF)」：
- **第一正規化 (1NF)**：確保每個欄位的值都是不可分割的「原子值」，並且沒有重複的資料列。
- **第二正規化 (2NF)**：滿足 1NF，且資料表中的所有非主鍵欄位都必須完全依賴於整個主鍵（主要針對複合主鍵）。
- **第三正規化 (3NF)**：滿足 2NF，且所有非主鍵欄位都不能依賴於其他的非主鍵欄位。

**簡單來說**：正規化的目標是確保資料庫中的每一條資訊都只儲存一次。雖然更高的正規化程度能減少冗餘，但有時為了查詢效能，會進行適度的「反正規化 (Denormalization)」。

## 結語

良好的資料庫設計是一門藝術，也是一門科學。它需要在減少冗餘和提高查詢效能之間找到平衡。理解資料表、主鍵、外鍵、關聯和正規化這些基本概念，是你成為一個優秀後端開發人員的必經之路。

在下一個章節，我們將學習如何使用 **SQL (Structured Query Language)** 來實際操作這些我們設計好的資料庫。

---

# Chapter 5.2: SQL

## 前言

**SQL (Structured Query Language)** 是與關聯式資料庫溝通的標準語言。無論你使用哪種關聯式資料庫系統，SQL 都是你用來定義、查詢、修改和控制資料存取的通用工具。掌握 SQL 是後端開發的基礎技能。

## SQL 命令分類

SQL 命令通常分為四大類：
1.  **DDL (Data Definition Language)**：資料定義語言，用於定義和管理資料庫物件。
    - `CREATE`：建立資料庫、資料表、索引等。
    - `ALTER`：修改現有資料庫物件的結構。
    - `DROP`：刪除資料庫物件。

2.  **DML (Data Manipulation Language)**：資料操作語言，用於處理資料表中的資料。
    - `SELECT`：查詢和檢索資料。
    - `INSERT`：向資料表中插入新的資料列。
    - `UPDATE`：更新資料表中已存在的資料。
    - `DELETE`：刪除資料表中的資料。

3.  **DCL (Data Control Language)**：資料控制語言，用於管理資料庫的存取權限。
    - `GRANT`：授予使用者權限。
    - `REVOKE`：撤銷使用者權限。

4.  **TCL (Transaction Control Language)**：交易控制語言，用於管理資料庫交易。
    - `COMMIT`：儲存交易。
    - `ROLLBACK`：復原交易。

## 核心 DML 操作

在日常開發中，DML 是最常使用的部分。

- **查詢資料 (`SELECT`)**
  ```sql
  -- 從 Users 表中選取所有使用者
  SELECT * FROM Users;

  -- 選取特定欄位，並使用 WHERE 進行篩選
  SELECT Username, Email FROM Users WHERE IsActive = 1;
  ```

- **插入資料 (`INSERT`)**
  ```sql
  INSERT INTO Users (Username, Email, PasswordHash) 
  VALUES ('john.doe', 'john.doe@example.com', 'hashed_password');
  ```

- **更新資料 (`UPDATE`)**
  ```sql
  UPDATE Users
  SET Email = 'new.email@example.com'
  WHERE Username = 'john.doe';
  ```

- **刪除資料 (`DELETE`)**
  ```sql
  DELETE FROM Users WHERE Username = 'john.doe';
  ```

## 聯結查詢 (`JOIN`)

`JOIN` 是 SQL 的精髓所在，它能讓你將來自多個資料表的資料列結合起來。

- **`INNER JOIN`**：只返回兩個資料表中聯結欄位值相符的資料列。
  ```sql
  -- 查詢每個訂單及其對應的使用者名稱
  SELECT o.Id, o.OrderDate, u.Username
  FROM Orders o
  INNER JOIN Users u ON o.UserId = u.Id;
  ```

- **`LEFT JOIN`**：返回左邊資料表的所有資料列，以及右邊資料表中符合條件的資料列。如果右邊沒有符合的，則為 `NULL`。
  ```sql
  -- 查詢所有使用者以及他們的訂單（即使他們沒有下過訂單）
  SELECT u.Username, o.Id
  FROM Users u
  LEFT JOIN Orders o ON u.Id = o.UserId;
  ```

## 彙總與分組 (`GROUP BY`)

`GROUP BY` 子句通常與彙總函式（如 `COUNT()`, `SUM()`, `AVG()`）一起使用，將具有相同值的資料列分組到摘要資料列中。

```sql
-- 計算每個使用者下了多少訂單
SELECT UserId, COUNT(Id) AS OrderCount
FROM Orders
GROUP BY UserId;
```

## 結語

SQL 是一門功能強大且表達能力豐富的語言。雖然 ORM (物件關聯對應) 框架在現代開發中很普遍，但深入理解 SQL 原理能讓你寫出更高效的查詢、更好地進行效能調校，並在複雜情境下遊刃有餘。

接下來，我們將介紹幾種在 .NET 生態系中常見的關聯式資料庫系統。

---

# Chapter 5.3: Relational Databases

## 前言

在了解了資料庫設計和 SQL 之後，我們來看看幾個主流的關聯式資料庫管理系統 (RDBMS)。每個系統都有其獨特的優缺點，選擇哪一個取決於你的專案需求、預算和團隊熟悉度。

## 1. SQL Server

由 Microsoft 開發的 SQL Server 是 .NET 生態系中的「原生」選項，與 Visual Studio、Azure 和其他微軟產品深度整合。

- **優點**：
    - **與 .NET 深度整合**：無縫的開發體驗，尤其是在使用 Entity Framework Core 時。
    - **強大的工具支援**：擁有 SQL Server Management Studio (SSMS) 和 Azure Data Studio 等成熟的管理工具。
    - **企業級功能**：提供高效能、高可用性和強大的安全性功能。
    - **文件齊全**：擁有龐大的社群和微軟官方的詳盡文件。
- **缺點**：
    - **授權費用**：雖然有免費的 Express 和 Developer 版本，但標準版和企業版的授權費用可能相當昂貴。
    - **平台限制**：雖然現在支援 Linux 和 Docker，但其最佳效能和最完整的功能仍然展現在 Windows 平台上。

## 2. PostgreSQL

PostgreSQL 是一個功能強大、開源且高度可擴展的物件關聯式資料庫系統。它以其穩定性、資料完整性和對 SQL 標準的高度遵循而聞名。

- **優點**：
    - **開源與免費**：沒有授權費用，可以自由使用和修改。
    - **跨平台**：在 Windows, macOS, Linux 等多種作業系統上都能良好運作。
    - **高度可擴展**：支援 JSON、XML 等進階資料型別，並允許自訂函式和資料型別。
    - **社群活躍**：擁有一個強大且活躍的全球社群。
- **缺點**：
    - **第三方工具**：雖然有 pgAdmin 等優秀工具，但在某些方面可能不如 SQL Server 的原生工具整合得那麼完美。
    - **在 Windows 上的效能**：傳統上，PostgreSQL 在 Linux 環境下的效能被認為略優於 Windows。

## 3. Azure SQL

Azure SQL Database 是 Microsoft 在其雲端平台 Azure 上提供的全受控關聯式資料庫服務 (DBaaS)。它本質上是雲端版本的 SQL Server。

- **優點**：
    - **全受控服務**：無需擔心硬體、作業系統、修補程式和備份。Microsoft 會處理所有基礎架構管理。
    - **彈性擴展**：可以根據需求輕鬆地調整計算和儲存資源，實現彈性擴展。
    - **高可用性**：內建高可用性和異地備援機制，確保服務的連續性。
    - **與 Azure 生態整合**：與 Azure 的其他服務（如 App Service, Functions）無縫整合。
- **缺點**：
    - **成本**：對於小型專案或持續運作的低流量應用，成本可能高於自行託管的資料庫。
    - **網路依賴**：所有連線都必須透過網路，可能會引入延遲。

## 結語

選擇哪種關聯式資料庫取決於多種因素。如果你在微軟技術棧中工作，並需要與 Windows 和 Azure 深度整合，**SQL Server** 或 **Azure SQL** 是自然之選。如果你尋求一個開源、跨平台且功能強大的解決方案，**PostgreSQL** 則是一個絕佳的選擇。在許多現代 .NET 專案中，PostgreSQL 因其靈活性和成本效益而變得越來越受歡迎。

在了解了關聯式資料庫之後，我們將探索另一種截然不同的資料儲存方式：NoSQL。

---

# Chapter 5.4: NoSQL Databases

## 前言

**NoSQL** (通常解釋為 "Not Only SQL") 是一系列資料庫技術的總稱，它們提供了一種與傳統關聯式資料庫 (RDBMS) 不同的資料儲存和檢索機制。當應用程式需要處理大量非結構化資料、要求極高的擴展性或靈活的資料模型時，NoSQL 資料庫便能大放異彩。

## 為什麼需要 NoSQL？

關聯式資料庫的「綱要 (Schema)」是固定的，這在確保資料一致性方面非常出色。但對於某些應用場景，這也成為了一種限制。NoSQL 的出現是為了解決：
- **大數據 (Volume & Velocity)**：需要處理 TB 甚至 PB 等級的資料，且資料寫入速度非常快。
- **非結構化/半結構化資料**：資料格式不固定，例如使用者日誌、IoT 感測器資料、社群媒體貼文等。
- **高擴展性 (Scalability)**：需要透過增加更多伺服器（水平擴展）來分散負載，而不是升級單一伺服器的硬體（垂直擴展）。
- **靈活的開發模型**：希望資料模型能隨著應用程式的快速迭代而輕鬆演進，無需進行複雜的資料庫遷移。

## NoSQL 資料庫類型

NoSQL 並非單一產品，而是一個龐大的家族，主要分為幾種類型：

1.  **文件資料庫 (Document Databases)**
    - **概念**：將資料儲存為類似 JSON 的「文件 (Document)」。每個文件都是一個獨立的單元，可以有自己獨特的結構。
    - **代表**：**MongoDB**, CouchDB, Azure Cosmos DB。
    - **適用場景**：內容管理、產品目錄、使用者設定檔等，資料結構多變且讀取操作頻繁。

2.  **鍵值資料庫 (Key-Value Stores)**
    - **概念**：最簡單的 NoSQL 類型，資料以「鍵-值」對的形式儲存。
    - **代表**：Redis, Amazon DynamoDB。
    - **適用場景**：快取、Session 管理、排行榜等，需要極快速的讀寫操作。

3.  **欄位家族資料庫 (Column-Family Stores)**
    - **概念**：以「欄位家族」為單位組織資料，適用於超大規模資料集。
    - **代表**：Apache Cassandra, HBase。

4.  **圖形資料庫 (Graph Databases)**
    - **概念**：專為儲存和導覽「關聯」而設計，使用節點、邊和屬性來表示和儲存資料。
    - **代表**：Neo4j, Amazon Neptune。
    - **適用場景**：社交網路、推薦引擎、知識圖譜。

## 1. MongoDB

MongoDB 是目前最受歡迎的文件資料庫之一。它將資料儲存在稱為 BSON (Binary JSON) 的格式中。

- **核心概念**：
    - **文件 (Document)**：一個 BSON 物件，對應關聯式資料庫中的一「行」。
    - **集合 (Collection)**：一組文件，對應關聯式資料庫中的一「表」。
- **優點**：
    - **靈活的綱要**：集合中的文件不需要有相同的結構。
    - **豐富的查詢語言**：支援強大的查詢、彙總和索引功能。
    - **易於水平擴展**：內建分片 (Sharding) 機制，可將資料分散到多台伺服器。

## 2. Azure Cosmos DB

Azure Cosmos DB 是微軟提供的一個全球分散式、多模型資料庫服務。它不僅僅是一個文件資料庫，它支援多種資料模型。

- **核心特性**：
    - **多模型支援**：可以透過相同的後端，使用 SQL (Core)、MongoDB、Cassandra、Gremlin (圖形) 和 Table 等多種 API 來存取資料。
    - **全球分散式**：可以輕鬆地將資料複製到全球任何 Azure 區域，為全球使用者提供低延遲的讀寫存取。
    - **可預測的效能**：提供基於「請求單位 (Request Units, RU)」的輸送量保證。
    - **高可用性**：提供業界領先的 SLA (服務等級協定)。

## 結語

NoSQL 資料庫並非要取代關聯式資料庫，而是為了解決不同類型的問題。在現代微服務架構中，通常會根據每個服務的特定需求，混合使用關聯式資料庫和 NoSQL 資料庫（稱為 "Polyglot Persistence"）。

了解何時使用、以及如何選擇合適的 NoSQL 資料庫，是現代雲端原生應用開發人員的關鍵技能。

完成本章後，你已經對資料庫的世界有了宏觀的認識。接下來，我們將學習如何透過 **ORM (物件關聯對應)**，在 .NET 應用程式中更優雅地與這些資料庫互動。