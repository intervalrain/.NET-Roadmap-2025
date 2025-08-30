# Chapter 15.1: ErrorOr

## 前言

在傳統的 C# 程式碼中，處理錯誤和傳回結果通常有兩種方式：
1.  **拋出例外 (Exceptions)**：當發生錯誤時，中斷正常的執行流程。這對於處理真正的、非預期的「例外」情況非常有效，但如果將其用於可預期的業務邏輯錯誤（例如「使用者名稱已存在」），則會有效能開銷，並可能使程式碼流程變得混亂。
2.  **傳回結果物件**：定義一個包含結果和錯誤訊息的物件。這種方式更好，但往往需要為每個方法都定義一個特定的結果類別，導致程式碼重複。

**ErrorOr** 是一個輕量級的函式庫，它提供了一種優雅、型別安全的方式來處理「一個操作要嘛成功傳回一個結果，要嘛失敗傳回一個錯誤」的場景。

## 核心概念

`ErrorOr<TValue>` 是一個泛型類型，它可以儲存兩種狀態之一：
- 一個 `TValue` 型別的成功結果。
- 一個或多個 `Error` 物件。

它迫使開發人員必須顯式地處理成功和失敗兩種情況，從而避免了忽略錯誤的可能性。

## 使用範例

```csharp
// 在一個 Service 中
public ErrorOr<User> GetUser(int id)
{
    if (_users.TryGetValue(id, out var user))
    {
        return user; // 傳回成功結果
    }

    // 傳回一個錯誤
    return Error.NotFound("User.NotFound", $"User with id {id} not found.");
}

// 在 Controller 中呼叫
[HttpGet("{id}")]
public IActionResult GetUser(int id)
{
    ErrorOr<User> result = _userService.GetUser(id);

    // 使用 Match 方法來分別處理成功和失敗的情況
    return result.Match(
        user => Ok(user), // 成功時，執行這個 Lambda
        errors => Problem(errors) // 失敗時，執行這個 Lambda
    );
}
```

`ErrorOr` 完美地結合了表達性和型別安全性，讓處理業務錯誤的程式碼變得既乾淨又可靠。

---

# Chapter 15.2: FluentValidation

## 前言

對傳入的資料進行驗證是任何應用程式都不可或缺的一部分。雖然可以在服務邏輯中手動編寫 `if-else` 判斷，但這會讓程式碼變得臃腫且難以維護。**FluentValidation** 提供了一種使用流暢介面 (fluent interface) 和 Lambda 運算式來建構強型別驗證規則的方式。

## 核心概念

- **將驗證邏輯分離**：為每個需要驗證的類別，建立一個對應的 `Validator` 類別。
- **流暢的 API**：使用鏈式呼叫來定義一組清晰、易讀的驗證規則。
- **豐富的內建規則**：提供了大量常用的驗證規則（如 `NotEmpty`, `Length`, `EmailAddress` 等）。
- **易於擴展**：可以輕鬆地撰寫自訂的驗證規則。

## 使用範例

```csharp
// 需要被驗證的 Model
public class CreateUserInput
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}

// 對應的 Validator
public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
{
    public CreateUserInputValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Age).InclusiveBetween(18, 99);
    }
}

// 在 ASP.NET Core 中可以輕鬆整合
// 1. 註冊驗證器
// builder.Services.AddValidatorsFromAssemblyContaining<CreateUserInputValidator>();

// 2. 在 Controller 中使用
// var validationResult = await _validator.ValidateAsync(input);
// if (!validationResult.IsValid) { ... }
```

FluentValidation 讓驗證邏輯變得集中、可重複使用且極其易讀。

---

# Chapter 15.3: MediatR

## 前言

在複雜的應用程式中，一個請求可能需要經過多個處理步驟，或者一個事件可能需要觸發多個獨立的處理程序。如果讓 Controller 直接呼叫多個 Service，會導致 Controller 變得臃腫且與具體實現緊密耦合。**MediatR** 是一個簡單的函式庫，它實現了「中介者模式 (Mediator Pattern)」，幫助你在應用程式中實現一個乾淨的、記憶體內的「請求/回應」和「發布/訂閱」訊息傳遞機制。

## 核心概念

- **解耦合**：請求的發送者 (Sender) 不知道誰是接收者 (Handler)，反之亦然。它們只依賴於 MediatR 的抽象。
- **CQRS (命令查詢責任分離)**：MediatR 非常適合用來實現 CQRS 模式。你可以將「命令 (Commands，會改變系統狀態的操作)」和「查詢 (Queries，只讀取資料的操作)」明確地分離開來。

**主要元件：**
- **`IRequest<TResponse>`**：定義一個會傳回 `TResponse` 型別結果的請求。
- **`IRequestHandler<TRequest, TResponse>`**：定義一個處理 `TRequest` 並傳回 `TResponse` 的處理程序。
- **`INotification`**：定義一個「發布/訂閱」模式的通知（事件）。
- **`INotificationHandler<TNotification>`**：定義一個處理 `TNotification` 的處理程序（可以有多個）。

## 使用範例

```csharp
// 1. 定義一個查詢及其結果
public record GetProductQuery(int Id) : IRequest<Product>;

// 2. 定義查詢的處理程序
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
{
    private readonly IProductRepository _repository;
    public Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetByIdAsync(request.Id);
    }
}

// 3. 在 Controller 中發送請求
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));
        return Ok(product);
    }
}
```

MediatR 有助於你寫出更乾淨、更遵循單一職責原則的程式碼，特別適合用於實現 Clean Architecture 或 CQRS 等架構模式。

---

# Chapter 15.4: Polly

## 前言

在分散式系統中，網路是不可靠的。對外部服務（如 API、資料庫）的呼叫可能會因為暫時性的網路問題、服務忙碌或短暫的故障而失敗。如果直接讓應用程式崩潰，使用者體驗會非常糟糕。**Polly** 是一個 .NET 的彈性和瞬時故障處理函式庫，它能讓你用一種非常流暢和優雅的方式，來定義重試、斷路器、超時等彈性策略。

## 核心策略

- **Retry (重試)**：當操作失敗時，自動進行重試。可以設定重試次數、重試之間的等待時間（例如指數退避）。
- **Circuit Breaker (斷路器)**：當偵測到某個服務的失敗次數在一定時間內超過閾值時，斷路器會「跳開」，在接下來的一段時間內，所有對該服務的呼叫都會立即失敗，而不會真的去嘗試呼叫。這可以防止你的應用程式反覆衝擊一個已經有問題的服務，給予它恢復的時間。
- **Timeout (超時)**：確保一個操作不會執行超過指定的時間。
- **Fallback (後備)**：當操作失敗時，提供一個預設的回應或執行一個替代邏輯。
- **Policy Wrap (策略包裝)**：可以將多個策略組合在一起，例如「先重試三次，如果還失敗，就跳開斷路器」。

## 使用範例 (與 `IHttpClientFactory` 整合)

Polly 與 `IHttpClientFactory` 的整合是其最常見的用法。

```csharp
builder.Services.AddHttpClient<MyApiClient>()
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy());

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    // 重試 3 次，每次等待時間為 2, 4, 8 秒
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    // 如果連續發生 5 次 HTTP 失敗，則跳開斷路器 30 秒
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}
```

Polly 是建構可靠、有彈性的分散式 .NET 應用的必備工具。

---

# Chapter 15.5: AutoMapper

## 前言

在多層架構的應用程式中，我們經常需要在不同的物件之間傳遞資料，例如將資料庫的實體物件 (Entity) 轉換為 API 的資料傳輸物件 (DTO)。手動編寫這些屬性對應的程式碼（`dto.Name = entity.Name;`）非常繁瑣、重複且容易出錯。**AutoMapper** 是一個約定優於設定 (convention-based) 的物件對應函式庫，它可以自動化地處理這些轉換。

## 核心概念

AutoMapper 的核心思想是，如果來源物件和目標物件的屬性名稱相同，它就能自動地將值從來源對應到目標，無需任何額外設定。

## 使用範例

```csharp
// 來源物件 (Entity)
public class User { public int Id { get; set; } public string FirstName { get; set; } public string LastName { get; set; } }

// 目標物件 (DTO)
public class UserDto { public int Id { get; set; } public string FullName { get; set; } }

// 1. 建立對應設定 (Profile)
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            // 對於名稱不一致的屬性，進行手動設定
            .ForMember(dest => dest.FullName, 
                       opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}

// 2. 在 Program.cs 中註冊 AutoMapper
// builder.Services.AddAutoMapper(typeof(UserProfile));

// 3. 在服務中注入 IMapper 並使用
public class UserService
{
    private readonly IMapper _mapper;
    public User GetUserDto(User userEntity)
    {
        // 執行對應
        return _mapper.Map<UserDto>(userEntity);
    }
}
```

AutoMapper 可以為你節省大量重複的對應程式碼，讓你的程式碼更乾淨、更專注於業務邏輯。

---

# Chapter 15.6: Autofac

## 前言

.NET 內建的依賴注入 (Dependency Injection, DI) 容器功能強大且易於使用，足以滿足大多數應用程式的需求。然而，在某些非常複雜的場景下，你可能需要一些內建容器不支援的進階 DI 功能。**Autofac** 是 .NET 生態系中最著名、功能最豐富的第三方 DI 容器之一。

## Autofac 的進階功能

- **屬性注入 (Property Injection)**：除了建構函式注入，Autofac 還支援自動注入公開的屬性。
- **自動組件掃描與註冊**：可以根據命名約定或其他規則，自動掃描一個組件 (Assembly) 並註冊其中的所有服務。
- **模組化註冊 (`Module`)**：可以將不同功能的服務註冊邏輯，組織到不同的 `Module` 類別中，使註冊程式碼更有結構。
- **生命週期事件**：可以在服務被建立或釋放時，掛載自訂的事件處理邏輯。
- **裝飾器模式 (Decorator Pattern)**：可以輕鬆地實現裝飾器模式，將一個服務的實例用另一個實例「包裝」起來，以增加額外的功能。

## 使用範例

```csharp
// 1. 安裝 NuGet 套件：Autofac.Extensions.DependencyInjection

// 2. 在 Program.cs 中替換預設容器
// builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// 3. 透過 Module 來設定註冊
// builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
// {
//     builder.RegisterModule(new MyAutofacModule());
// });

public class MyAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // 根據命名約定，自動註冊所有名稱以 "Repository" 結尾的類別
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces();
    }
}
```

## 結語

對於絕大多數 .NET 應用，內建的 DI 容器已經足夠好。但當你遇到需要更進階、更靈活的 DI 功能的複雜專案時，Autofac 提供了一個非常強大和成熟的選擇。它讓你能以更精細、更自動化的方式來管理你的依賴關係。