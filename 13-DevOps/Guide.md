# Chapter 13.1: DevOps Concepts

## 前言

歡迎來到第十三章！在現代軟體開發中，**DevOps** 已經從一個時髦的術語演變為一種主流的文化、實踐和工具的集合。它旨在打破傳統開發 (Development) 和維運 (Operations) 之間的壁壘，以實現更快、更可靠的軟體交付。

## 什麼是 DevOps？

DevOps 的核心是 **C.A.L.M.S.** 模型：

- **Culture (文化)**：這是 DevOps 的基石。它強調團隊之間的協作、共同的責任感和透明的溝通。開發團隊和維運團隊不再是互相指責的「孤島」，而是共同為產品的整個生命週期負責。
- **Automation (自動化)**：將軟體交付過程中的所有手動、重複性的任務自動化。從程式碼的建構、測試到部署，自動化可以減少人為錯誤，提高效率和一致性。
- **Lean (精實)**：借鑒精實生產的理念，專注於為客戶創造價值，並消除浪費（例如：不必要的功能、過長的等待時間、繁瑣的流程）。
- **Measurement (度量)**：對開發流程和生產環境中的一切進行度量。透過收集和分析資料（例如：部署頻率、變更失敗率、平均修復時間），團隊可以了解瓶頸所在，並持續改進。
- **Sharing (分享)**：在團隊和組織之間分享知識、經驗和工具。這有助於建立學習型組織，並促進最佳實踐的傳播。
	
## DevOps 的生命週期

DevOps 的實踐涵蓋了軟體的整個生命週期，通常被描繪成一個無限循環的迴圈：

1.  **Plan (計畫)**：定義功能、排定優先級、規劃工作。
2.  **Code (編碼)**：開發人員編寫程式碼，並使用版本控制系統（如 Git）進行管理。
3.  **Build (建構)**：將原始碼編譯、打包成可執行的產物。這一步通常由 **持續整合 (Continuous Integration, CI)** 伺服器自動完成。
4.  **Test (測試)**：自動執行單元測試、整合測試等，以確保程式碼的品質。
5.  **Release (發布)**：將建構好的產物儲存到一個「產物庫 (Artifact Repository)」中，準備進行部署。
6.  **Deploy (部署)**：將應用程式部署到測試環境或生產環境。**持續部署 (Continuous Deployment, CD)** 指的是將通過所有測試的程式碼自動部署到生產環境。
7.  **Operate (維運)**：維護和管理正在執行的應用程式。
8.  **Monitor (監控)**：持續監控應用程式的效能、健康狀況和使用者行為，並將收集到的資料回饋到「計畫」階段，形成一個閉環。

![DevOps Lifecycle](https://i.imgur.com/8y7mJ2P.png)

## 核心實踐：CI/CD

**持續整合 (Continuous Integration, CI)** 和 **持續部署/交付 (Continuous Deployment/Delivery, CD)** 是 DevOps 中最核心的技術實踐。

- **持續整合 (CI)**：開發人員頻繁地（每天多次）將他們的程式碼變更合併到主幹分支。每次合併都會自動觸發一次建構和測試。這有助於及早發現整合錯誤，避免了在專案後期進行痛苦的大規模整合。

- **持續交付 (Continuous Delivery)**：在 CI 的基礎上，將任何通過所有測試的建構版本，自動地部署到一個類似生產的環境（例如預備環境 Staging）。這確保了你 **隨時** 都有一個可以部署到生產環境的版本。

- **持續部署 (Continuous Deployment)**：這是 CI/CD 的終極形式。將任何通過所有測試的建構版本 **自動地** 部署到 **生產環境**，無需任何人工介入。

## 結語

DevOps 不僅僅是一套工具或流程，它更是一種文化轉變。它透過打破組織壁壘、擁抱自動化和持續改進，旨在實現更快、更頻繁、更可靠的價值交付。理解 DevOps 的核心理念和實踐，對於任何想要在現代軟體產業中取得成功的開發人員或團隊來說，都至關重要。

在接下來的章節中，我們將介紹兩種實現 CI/CD 的主流平台：**GitHub Actions** 和 **Azure Pipelines**。

---

# Chapter 13.2: GitHub Actions

## 前言

**GitHub Actions** 是一個直接整合在 GitHub 平台中的 CI/CD 工具。如果你的程式碼已經託管在 GitHub 上，那麼使用 GitHub Actions 來自動化你的建構、測試和部署流程，將會是一種無與倫比的無縫體驗。

## GitHub Actions 核心概念

1.  **Workflow (工作流程)**：工作流程是一個可設定的自動化流程，由一個或多個 **Job** 組成。你可以在你的程式碼倉庫中，透過一個 YAML 檔案來定義工作流程。這個檔案通常位於 `.github/workflows/` 目錄下。

2.  **Event (事件)**：事件是觸發工作流程執行的特定活動。例如：
    - `push`：當有程式碼被推送到某個分支時。
    - `pull_request`：當一個拉取請求被建立或更新時。
    - `schedule`：按照 Cron 排程定時執行。
    - `workflow_dispatch`：手動觸發執行。

3.  **Job (作業)**：作業是在同一個 **Runner** 上執行的一系列 **Step**。預設情況下，一個工作流程中的多個作業是並行執行的。

4.  **Runner (執行器)**：執行器是一個會執行你的工作流程的伺服器。GitHub 提供了由他們託管的執行器（支援 Ubuntu, Windows, macOS），你也可以自行託管執行器。

5.  **Step (步驟)**：步驟是作業中的一個獨立任務。一個步驟可以是一個 shell 命令，也可以是一個 **Action**。

6.  **Action (動作)**：動作是 GitHub Actions 平台的核心。它是一個可重複使用的程式碼單元，可以被組合到工作流程中。你可以使用由社群開發的、發布在 [GitHub Marketplace](https://github.com/marketplace?type=actions) 上的數千個動作，也可以自己撰寫動作。

## .NET CI 工作流程範例

以下是一個典型的、用於 .NET 專案的 CI 工作流程範例 (`.github/workflows/dotnet.yml`)：

```yaml
name: .NET CI

# 1. 定義觸發事件：當有 push 到 main 分支，或有 pull request 指向 main 分支時觸發
on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  build:
    # 2. 設定執行器：使用最新版的 Ubuntu
    runs-on: ubuntu-latest

    steps:
    # 3. 第一步：簽出 (Checkout) 你的程式碼
    - uses: actions/checkout@v3

    # 4. 第二步：設定 .NET 環境
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x

    # 5. 第三步：還原 NuGet 套件
    - name: Restore dependencies
      run: dotnet restore

    # 6. 第四步：建構專案
    - name: Build
      run: dotnet build --no-restore

    # 7. 第五步：執行測試
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

## 結語

GitHub Actions 以其與 GitHub 的深度整合、簡潔的 YAML 設定和龐大的 Marketplace 生態系，迅速成為了開源專案和許多企業的首選 CI/CD 平台。它讓自動化工作流程的建立和管理變得前所未有的簡單和直觀。

---

# Chapter 13.3: Azure Pipelines

## 前言

**Azure Pipelines** 是微軟在 **Azure DevOps** 服務套件中提供的一個功能強大、語言無關、平台無關的 CI/CD 服務。它非常成熟，特別適合需要處理複雜發布流程、多階段部署和精細權限控制的企業級應用。

## Azure Pipelines 核心概念

Azure Pipelines 的概念與 GitHub Actions 非常相似，但術語略有不同：

1.  **Pipeline (管線)**：管線是定義你的 CI/CD 流程的單位，由一個或多個 **Stage** 組成。
2.  **Stage (階段)**：階段是管線中的一個主要劃分，例如 `Build` 階段、`DeployToStaging` 階段、`DeployToProduction` 階段。預設情況下，階段是按順序執行的。
3.  **Job (作業)**：作業是階段中的一個執行單元，由一系列 **Step** 組成。同一個階段內的多個作業可以並行執行。
4.  **Step (步驟)**：步驟是作業中的最小單位。一個步驟可以是一個腳本 (`script`)，也可以是一個預先封裝好的 **Task**。
5.  **Task (任務)**：任務是 Azure Pipelines 中可重複使用的建構區塊，類似於 GitHub Actions 中的 Action。Azure Pipelines 提供了數百個內建的任務，用於處理各種常見的建構和部署操作。

## YAML 管線範例

Azure Pipelines 也使用 YAML 來定義管線，檔案通常命名為 `azure-pipelines.yml`。

```yaml
# 定義觸發器
trigger:
- main

# 定義執行器集區
pool:
  vmImage: 'ubuntu-latest'

stages:
- stage: Build
  displayName: 'Build stage'
  jobs:
  - job: BuildJob
    displayName: 'Build .NET Project'
    steps:
    # 1. 使用一個「任務」來指定要使用的 .NET Core SDK 版本
    - task: UseDotNet@2
      inputs:
        packageType: 'sdk'
        version: '8.x'

    # 2. 執行 dotnet restore
    - script: dotnet restore
      displayName: 'Restore dependencies'

    # 3. 執行 dotnet build
    - script: dotnet build --no-restore
      displayName: 'Build project'

    # 4. 執行 dotnet test
    - script: dotnet test --no-build
      displayName: 'Run tests'

    # 5. 將建構結果打包並發布為一個「產物 (Artifact)」
    - task: PublishBuildArtifacts@1
      inputs:
        PathtoPublish: '$(Build.ArtifactStagingDirectory)'
        ArtifactName: 'drop'
```

## GitHub Actions vs. Azure Pipelines

| 特性 | GitHub Actions | Azure Pipelines |
| :--- | :--- | :--- |
| **整合性** | 與 GitHub 生態無縫整合 | 與 Azure DevOps 和 Azure 雲端平台深度整合 |
| **核心理念** | 簡單、直觀，社群驅動 | 功能全面、企業級，提供更複雜的發布管理功能 |
| **免費方案** | 對公開儲存庫非常慷慨 | 提供免費方案，但有一些並行作業和時間限制 |
| **發布管理** | 較為基礎 | 提供非常強大的「發布管線 (Release Pipelines)」，支援多階段審批、閘道等進階功能 |

## 結語

Azure Pipelines 是一個非常成熟和強大的 CI/CD 平台。如果你的團隊已經在使用 Azure DevOps 進行專案管理，或者你需要處理複雜的、多環境的發布流程，Azure Pipelines 將是一個絕佳的選擇。對於與 Azure 雲端服務的部署整合，它也提供了最無縫的體驗。