# 2.1 版控系統 - Git 指南 (Version Control - Git)

歡迎來到通用開發技能的第一站！版本控制系統 (Version Control System, VCS) 是開發者最重要的工具之一，而 Git 是目前全世界最普及的 VCS。

## 1. 為什麼需要版本控制？

想像一下你正在寫一份重要的報告，你可能會這樣保存檔案：

- `報告_v1.doc`
- `報告_v2.doc`
- `報告_最終版.doc`
- `報告_最終版_真的最終版.doc`

這很快就會變得一團混亂。版本控制系統解決了這個問題，它能幫助你：

- **追蹤變更**: 精確記錄每一次的檔案修改。
- **團隊協作**: 讓多位開發者可以同時在同一個專案上工作，而不會互相覆蓋彼此的程式碼。
- **回到過去**: 如果不小心把專案改壞了，可以輕鬆地回復到之前的某個穩定版本。
- **建立分支 (Branching)**: 可以安全地開發新功能或修復錯誤，而不影響主要版本的穩定性。

## 2. Git 的核心概念：三個區域

要理解 Git，首先要了解它的三個主要區域：

1.  **工作目錄 (Working Directory)**: 這就是你電腦上實際看到和編輯的專案資料夾。
2.  **暫存區 (Staging Area / Index)**: 這是一個「準備提交」的區域。你可以把工作目錄中的某些變更挑選出來，放到暫存區，代表這些變更將被包含在下一次的「提交」中。
3.  **本地倉庫 (Local Repository)**: 這是 Git 儲存專案所有歷史紀錄的地方 (位於你專案根目錄下的 `.git` 資料夾中)。當你「提交」(Commit) 時，暫存區的內容快照就會被永久保存在這裡。

**流程比喻：**
`工作目錄` (你的書桌) -> `git add` -> `暫存區` (打包好的箱子) -> `git commit` -> `本地倉庫` (倉庫儲藏室)

## 3. 核心指令實作

現在，讓我們透過實際操作來學習最重要的幾個指令。

請打開您的終端機 (Terminal)，並依照以下步驟操作。

### 步驟 1: 建立練習目錄與初始化倉庫

```bash
# 進入我們這個單元的資料夾
cd 02-General-Development-Skills

# 建立一個新的練習用資料夾
mkdir Git-Practice

# 進入該資料夾
cd Git-Practice

# 將這個資料夾初始化成一個 Git 本地倉庫
# 這會建立一個隱藏的 .git 資料夾
git init
```

### 步驟 2: 第一次提交 (Commit)

1.  **建立新檔案**
    在 `Git-Practice` 資料夾中，建立一個名為 `plan.txt` 的檔案，並在其中輸入以下文字：

    ```
    My Learning Plan:
    1. Master Git.
    ```

2.  **檢查狀態**
    回到終端機，輸入以下指令來查看當前倉庫的狀態：
    ```bash
git status
```
    你會看到 `plan.txt` 被列為 `Untracked files` (未追蹤的檔案)。

3.  **加入暫存區**
    使用 `git add` 指令將這個新檔案加入暫存區：
    ```bash
git add plan.txt
```
    再次執行 `git status`，你會看到 `plan.txt` 現在位於 `Changes to be committed` (準備提交的變更) 清單中。

4.  **提交到倉庫**
    使用 `git commit` 指令，將暫存區的內容提交到本地倉庫。`-m` 參數可以讓你直接在指令中輸入提交訊息。
    ```bash
git commit -m "Initial commit: Create learning plan"
```

### 步驟 3: 第二次提交

1.  **修改檔案**
    編輯 `plan.txt`，在檔案最後新增一行，使其內容變為：
    ```
    My Learning Plan:
    1. Master Git.
    2. Learn about clean code.
    ```

2.  **查看變更**
    使用 `git diff` 指令，查看工作目錄中的檔案與上次提交的版本有何不同：
    ```bash
git diff
```

3.  **加入暫存區並提交**
    這次我們使用 `.` 來代表「所有變更的檔案」。
    ```bash
git add .
git commit -m "Update plan to include clean code"
```

### 步驟 4: 查看歷史紀錄

使用 `git log` 指令來查看所有的提交紀錄：

```bash
git log

# 你也可以試試這個更簡潔的格式
git log --oneline
```

---

請依照以上步驟實際操作一次。

完成後，請告訴我您 `git log --oneline` 的輸出結果。如果您在過程中遇到任何問題，也請隨時提出。

---

# 2.2 資料結構與演算法 (Data Structures & Algorithms)

如果您覺得 Git 是開發者的「工具」，那麼資料結構與演算法就是開發者的「內功」。它能幫助您寫出更有效率、更優雅的程式碼，也是衡量一位工程師解決問題能力的重要指標。

## 1. 什麼是資料結構 (Data Structures)？

簡單來說，**資料結構是組織和儲存資料的方式**。選擇正確的資料結構，可以讓你的程式在存取和操作資料時變得飛快。

在 C# 中，你其實已經天天在使用它們了！

- **`List<T>` (列表)**
  - **底層結構**：通常是動態陣列 (Array)。
  - **特性**：透過索引 (index) 存取元素非常快。但在中間插入或刪除元素比較慢，因為需要移動後面的所有元素。
  - **適用場景**：當你需要一個有序的集合，並且經常需要透過索引讀取它時。

- **`Dictionary<TKey, TValue>` (字典)**
  - **底層結構**：通常是雜湊表 (Hash Table)。
  - **特性**：透過唯一的鍵 (Key) 來新增、尋找、刪除元素，速度極快，幾乎不受資料量的影響。
  - **適用場景**：當你需要快速查找資料時，例如：根據使用者 ID 尋找使用者物件。

## 2. 什麼是演算法 (Algorithms)？

**演算法是解決特定問題的一系列明確步驟**。例如，「將一組數字由小到大排序」就是一個問題，而「氣泡排序法」或「快速排序法」就是解決這個問題的演算法。

### 如何衡量演算法的好壞？- Big O Notation

Big O 是一種數學表示法，用來描述當輸入資料量 (n) 增加時，演算法的執行時間或空間使用量的增長趨勢。

- **O(1) - 常數時間**: 最理想的狀況。不論輸入資料有多大，執行時間都一樣。
  - **範例**: 從 `Dictionary` 中透過 Key 找一個值。

- **O(n) - 線性時間**: 執行時間隨著資料量 (n) 成正比增長。
  - **範例**: 在一個 `List` 中尋找某個特定元素，最壞情況下需要從頭找到尾。

- **O(n²) - 平方時間**: 執行時間隨著資料量的平方增長。通常發生在巢狀迴圈中。應盡量避免。
  - **範例**: 在一個 List 中，為每一個元素去遍歷一次整個 List。

## 3. 思考練習 (Thinking Exercise)

這次我們不做程式碼練習，而是做一個思考題：

**情境：** 你的應用程式中有 100,000 個使用者物件。你需要頻繁地根據使用者的 `string UserId` 來查詢特定的使用者資料。

**問題：** 你會選擇用 `List<User>` 還是 `Dictionary<string, User>` 來儲存這些使用者物件？為什麼？請試著用 Big O 的概念來解釋你的選擇。

---

請花點時間思考這個問題，然後告訴我你的答案和理由。

---

# 2.3 乾淨的程式碼 (Clean Code)

> 「任何一個傻瓜都能寫出電腦可以理解的程式碼。好的程式設計師寫出的是人類可以理解的程式碼。」 - Martin Fowler

這句話完美地詮釋了 Clean Code 的精髓。它不僅僅是讓程式碼可以運作，更是關於**可讀性、可維護性**和**團隊協作**的藝術。

## 核心原則

### 1. 有意義的命名 (Meaningful Names)

變數、方法、類別的名稱應該要能清楚地表達它們的用途。

**反面教材 👎**
```csharp
// d 代表什麼?
int d; 

// p又是什麼?
var p = GetP(); 
```

**正面教材 👍**
```csharp
int elapsedTimeInDays;

var activeUsers = GetActiveUsers();
```

### 2. 函式只做一件事 (Functions Should Do One Thing)

一個函式應該只專注於完成一個單一的、明確的任務。

**反面教材 👎**
```csharp
// 這個函式做了三件事：取得資料、格式化、儲存
public void ProcessUserData(int userId)
{
    // 1. 取得資料
    var user = _database.GetUserById(userId);
    if (user == null) return;

    // 2. 格式化報告
    var report = $"User Report:\nID: {user.Id}\nName: {user.Name}";

    // 3. 儲存檔案
    File.WriteAllText($"/reports/{user.Id}.txt", report);
}
```

**正面教材 👍**
```csharp
public void ProcessUserData(int userId)
{
    var user = GetUserFromDatabase(userId);
    if (user == null) return;

    var reportContent = FormatUserReport(user);

    SaveReportToFile(user.Id, reportContent);
}

// 每個函式只做一件事
private User GetUserFromDatabase(int userId) { /* ... */ }
private string FormatUserReport(User user) { /* ... */ }
private void SaveReportToFile(int userId, string content) { /* ... */ }
```

### 3. 避免使用魔法數字 (Avoid Magic Numbers)

程式碼中不應該出現意義不明的數字或字串。

**反面教材 👎**
```csharp
// 86400 是什麼?
if (seconds > 86400) { /* ... */ } 

// 2 代表什麼狀態?
if (order.Status == 2) { /* ... */ }
```

**正面教材 👍**
```csharp
const int SecondsInADay = 86400;
if (seconds > SecondsInADay) { /* ... */ }

// 使用 Enum (列舉) 是最好的方式
public enum OrderStatus { Pending = 1, Approved = 2, Shipped = 3 }
if (order.Status == OrderStatus.Approved) { /* ... */ }
```

## 程式碼審查練習 (Code Review Exercise)

看看下面這段 C# 程式碼。它完全可以正常運作，但卻很「髒」。

**情境:** 這個函式接收一個產品列表，計算總價。如果總價超過 100 元，就打 9 折；如果超過 50 元，就打 95 折。

```csharp
public class Calculator
{
    public double Calc(List<Product> p)
    {
        double t = 0;
        foreach (var item in p)
        {
            t += item.Price;
        }

        if (t > 100)
        {
            t = t * 0.9;
        }
        else if (t > 50)
        {
            t = t * 0.95;
        }
        return t;
    }
}
```

**問題:**
根據我們剛剛學到的 Clean Code 原則，你認為這段程式碼有哪些可以改進的地方？請列出你的想法。

---

請思考一下，然後告訴我你的重構建議。

---

# 2.4 重構 (Refactoring)

如果說「乾淨的程式碼」是我們的**目標**，那麼「重構」就是我們達成這個目標所使用的**方法**。

## 1. 什麼是重構？

重構是在**不改變軟體外部行為**的前提下，對其**內部結構進行修改**，以提高程式碼的可讀性、可維護性，並降低其複雜度。

關鍵在於「不改變外部行為」。重構不是修正錯誤 (bug fixing)，也不是增加新功能。它純粹是為了讓現有的、可以運作的程式碼變得更好。

## 2. 為何及何時重構？

- **為何？**
  - 讓程式碼更容易被理解。
  - 讓未來的 bug 更容易被找到。
  - 讓新增功能變得更快、更安全。
- **何時？**
  - **三次法則 (The Rule of Three):** 當你第三次複製貼上某段程式碼時，就應該將它提煉成一個獨立的函式。
  - **新增功能前:** 先整理周邊的程式碼，讓新功能的加入更順利。
  - **程式碼審查後 (Code Review):** 根據團隊成員的建議來改善程式碼。

## 3. 核心重構技巧

我們在上一節的練習中，其實已經手動進行了重構！讓我們把這些技巧名稱化：

- **重新命名 (Rename Method/Variable)**
  - 這是最簡單也最常見的重構。將 `Calc`, `p`, `t` 重新命名為 `CalculatePriceWithDiscount`, `products`, `totalPrice` 就是這個技巧的應用。

- **提煉函式 (Extract Method)**
  - 將一段較大的程式碼片段，從原始函式中「提煉」出來，成為一個新的、獨立的函式。
  - 在 Clean Code 的「函式只做一件事」範例中，我們將處理使用者資料的巨大函式，提煉成了 `GetUserFromDatabase`, `FormatUserReport`, `SaveReportToFile` 三個小函式，這就是提煉函式。

- **引入解釋性變數 (Introduce Explaining Variable)**
  - 有時候一個判斷式或計算很複雜，讓人難以一眼看懂。可以將這個複雜的表達式，用一個有意義的變數來取代。

**反面教材 👎**
```csharp
if (user.Age > 65 && user.HasPension && !user.IsEmployed)
{
    // ... apply senior discount
}
```

**正面教材 👍**
```csharp
bool isEligibleForSeniorDiscount = user.Age > 65 && user.HasPension && !user.IsEmployed;

if (isEligibleForSeniorDiscount)
{
    // ... apply senior discount
}
```

## 總結

Clean Code 是**名詞**，是我們的目的地。Refactoring 是**動詞**，是我們前往目的地的旅程。

它們是相輔相成的概念，目標都是產出高品質、易於維護的專業級軟體。

---

這個主題是建立在 Clean Code 之上的理論，因此沒有練習。

如果您理解了「重構」是達成「乾淨程式碼」的手段這個核心概念，請告訴我，我們就可以進入下一個主題：**設計模式 (Design Patterns)**。
