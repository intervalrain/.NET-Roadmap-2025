# 開發環境設定指南

歡迎來到 .NET 學習旅程的第一步！本指南將引導你完成開發環境的設定。請依照以下步驟操作，並在完成後勾選。

## 1. 安裝 Visual Studio Code
- [x] 前往 [Visual Studio Code 官方網站](https://code.visualstudio.com/) 下載並安裝適合你作業系統的版本。
- [x] 安裝完成後，請開啟 VS Code。

## 2. 安裝 VS Code 建議擴充功能
- [x] 在 VS Code 的擴充功能市場中，搜尋並安裝以下擴充功能：
  - `ms-dotnettools.csdevkit` (C# Dev Kit)
  - `ms-dotnettools.csharp` (C#)
  - `ms-dotnettools.vscode-dotnet-runtime` (.NET Install Tool)
  - `VisualStudioExptTeam.vscodeintellicode` (IntelliCode for C# Dev Kit)
  - `ms-azuretools.vscode-docker` (Docker)

## 3. 安裝 Docker Desktop
- [x] 前往 [Docker 官方網站](https://www.docker.com/products/docker-desktop/) 下載並安裝 Docker Desktop。
- [x] 根據官方指引完成安裝。安裝後，請啟動 Docker Desktop 並確保它正在執行。

## 4. 驗證安裝
- [x] 開啟你的終端機 (Terminal)。
- [x] 輸入 `code --version`，確認 VS Code 已成功安裝。
- [x] 輸入 `docker --version`，確認 Docker 已成功安裝。

## 5. 深入了解 Dev Container (開發容器)

Dev Container 是 Visual Studio Code 的一項強大功能，它讓你可以在一個隔離且一致的 Docker 容器中進行開發。

### 為什麼要使用 Dev Container？

想像一下，你剛加入一個新專案，光是設定好開發環境（安裝正確的 .NET 版本、資料庫、和其他工具）可能就要花上一整天，而且還可能因為作業系統的差異而遇到各種問題。Dev Container 就是為了解決這些問題而生的。

- **一致性**: 確保團隊中每位成員，無論使用 Windows, macOS, 或 Linux，都在完全相同的環境中開發、測試，從根本上解決了「在我電腦上可以跑」的問題。
- **隔離**: 專案所需的所有工具和 SDK 都被安裝在容器裡，不會「污染」你的本機作業系統。你可以同時開發多個需要不同工具版本的專案，而它們之間不會互相干擾。
- **易於設定**: 新成員只需要打開專案，VS Code 就會自動提示設定開發容器。點擊一下，所有環境就自動建置好了。

### 它是如何運作的？

Dev Container 的核心是一個名為 `devcontainer.json` 的設定檔，通常放在專案根目錄的 `.devcontainer` 資料夾中。這個檔案會告訴 VS Code 如何建立開發環境，例如：
- 要使用哪個 Docker 映像檔 (例如：一個預裝了 .NET SDK 的映像檔)。
- 需要自動安裝哪些 VS Code 擴充功能到容器裡。
- 容器建立後需要執行哪些命令 (例如：`dotnet restore`)。
- 需要轉發哪些網路連接埠。

### 快速體驗：你的第一個 Dev Container

讓我們來做一個簡單的實驗，讓你親身體驗 Dev Container 的魔力。

1.  **新增 Dev Container 設定檔**:
    - 在 VS Code 中，按下 `F1` 開啟命令選擇區。
    - 輸入 `Dev Containers: Add Dev Container Configuration Files...` 並執行。
    - 選擇 `Add Configuration to workspace`。
    - VS Code 會顯示一個定義列表，請選擇 `C# (.NET)`。
    - 接著選擇一個 .NET 版本 (建議選最新的 LTS 版本)。
    - 不需要勾選其他額外功能，直接點擊 "OK"。
    - VS Code 會自動建立 `.devcontainer` 資料夾和 `devcontainer.json` 檔案。

2.  **重新在容器中開啟專案**:
    - VS Code 建立完設定檔後，點擊 VS Code 左下角的 `Open a Remote Window`。
    - 點擊 `Reopen in container`。
    - VS Code 會開始建置 Docker 映像檔並設定環境。第一次會需要幾分鐘的時間。你可以從終端機的輸出來觀察進度。
    - 完成後，VS Code 會重新載入，但現在你已經身在容器中了！注意看左下角的綠色區塊，那裡會顯示你目前所在的容器環境。

3.  **在容器中工作**:
    - [x] 在 VS Code 的終端機中 (它現在是容器裡的 shell)，輸入 `dotnet --version`。看看輸出的版本是否就是你在 `devcontainer.json` 中指定的版本。
    - [x] 恭喜你！你已經成功進入了 Dev Container。現在，這個專案的所有開發工作都會在這個乾淨、獨立且可重複的環境中進行。
    - [x] 嘗試新建一個 Console App 並執行它，在終端機中輸入 `dotnet new console -o HelloWorld`，完成後再輸入 `dotnet run --project HelloWorld`。

---

完成以上步驟後，你的基礎開發環境就準備就緒了！我們可以進入下一個章節的學習。
