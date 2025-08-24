using CoRSample.Models;
using CoRSample.Repositories;

namespace CoRSample;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Rain Hu",
            AnnualLeavesInHrs = 24
        };

        builder.Services.AddSingleton(employee);
        builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

        // Build the approver chain: ReqValidator -> Supervisor -> Manager -> Notifier, expose the chain as Approver
        builder.Services.AddSingleton(sp => new Notifier(sp.GetRequiredService<ILogger<Notifier>>(), sp.GetRequiredService<IEmployeeRepository>(), null));
        builder.Services.AddSingleton(sp => new Manager(sp.GetRequiredService<ILogger<Manager>>(), sp.GetRequiredService<IEmployeeRepository>(), sp.GetRequiredService<Notifier>()));
        builder.Services.AddSingleton(sp => new Supervisor(sp.GetRequiredService<ILogger<Supervisor>>(), sp.GetRequiredService<IEmployeeRepository>(), sp.GetRequiredService<Manager>()));
        builder.Services.AddSingleton(sp => new ReqValidator(sp.GetRequiredService<ILogger<ReqValidator>>(), sp.GetRequiredService<IEmployeeRepository>(), sp.GetRequiredService<Supervisor>()));
        builder.Services.AddSingleton<Approver>(sp => sp.GetRequiredService<ReqValidator>());

        builder.Services.AddHostedService<WorkdayService>();

        var app = builder.Build();

        await app.RunAsync();
    }
}