namespace SingletonSample;

public sealed class AppSettings
{
    // 1. 使用 Lazy<T> 來延遲初始化，並且保證執行緒安全
    //    裡面的 () => new AppSettings() 只有在第一次被存取時才會執行。
    private static readonly Lazy<AppSettings> _lazyInstance = new(() => new AppSettings());

    // 2. 提供一個公開的靜態屬性來存取唯一的執行個體。
    public static AppSettings Instance => _lazyInstance.Value;

    // 3. 將建構函式設為 private，防止外部直接 new 來建立多個執行個體。
    private AppSettings()
    {
        Console.WriteLine("AppSettings instance created. Loading settings...");
    }

    public string ConnectionString => "Server=myServer;Database=myDatabase";
}