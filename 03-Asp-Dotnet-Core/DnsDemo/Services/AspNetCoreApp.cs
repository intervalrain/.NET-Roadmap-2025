using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class AspNetCoreApp(ILogger<AspNetCoreApp> logger) : IAspNetCoreApp
{
    public async Task<string> ProcessRequestAsync(string path, Dictionary<string, string> queryParams)
    {
        logger.LogInformation("[ASP.NET Core App] Processing request for path: {Path}", path);
        
        await Task.Delay(50);
        
        return path switch
        {
            "/add" when queryParams.ContainsKey("num1") && queryParams.ContainsKey("num2") => 
                ProcessAddition(queryParams["num1"], queryParams["num2"]),
            "/subtract" when queryParams.ContainsKey("num1") && queryParams.ContainsKey("num2") => 
                ProcessSubtraction(queryParams["num1"], queryParams["num2"]),
            "/multiply" when queryParams.ContainsKey("num1") && queryParams.ContainsKey("num2") => 
                ProcessMultiplication(queryParams["num1"], queryParams["num2"]),
            "/divide" when queryParams.ContainsKey("num1") && queryParams.ContainsKey("num2") => 
                ProcessDivision(queryParams["num1"], queryParams["num2"]),
            _ => GenerateErrorPage(path)
        };
    }
    
    private string ProcessAddition(string num1Str, string num2Str)
    {
        if (double.TryParse(num1Str, out var num1) && double.TryParse(num2Str, out var num2))
        {
            var result = num1 + num2;
            logger.LogInformation("[ASP.NET Core App] Calculated {Num1} + {Num2} = {Result}", num1, num2, result);
            return GenerateCalculatorPage("Addition", num1, num2, result, "+");
        }
        return GenerateErrorPage("/add", "Invalid numbers provided");
    }
    
    private string ProcessSubtraction(string num1Str, string num2Str)
    {
        if (double.TryParse(num1Str, out var num1) && double.TryParse(num2Str, out var num2))
        {
            var result = num1 - num2;
            logger.LogInformation("[ASP.NET Core App] Calculated {Num1} - {Num2} = {Result}", num1, num2, result);
            return GenerateCalculatorPage("Subtraction", num1, num2, result, "-");
        }
        return GenerateErrorPage("/subtract", "Invalid numbers provided");
    }
    
    private string ProcessMultiplication(string num1Str, string num2Str)
    {
        if (double.TryParse(num1Str, out var num1) && double.TryParse(num2Str, out var num2))
        {
            var result = num1 * num2;
            logger.LogInformation("[ASP.NET Core App] Calculated {Num1} × {Num2} = {Result}", num1, num2, result);
            return GenerateCalculatorPage("Multiplication", num1, num2, result, "×");
        }
        return GenerateErrorPage("/multiply", "Invalid numbers provided");
    }
    
    private string ProcessDivision(string num1Str, string num2Str)
    {
        if (double.TryParse(num1Str, out var num1) && double.TryParse(num2Str, out var num2))
        {
            if (num2 == 0)
            {
                return GenerateErrorPage("/divide", "Division by zero is not allowed");
            }
            var result = num1 / num2;
            logger.LogInformation("[ASP.NET Core App] Calculated {Num1} ÷ {Num2} = {Result}", num1, num2, result);
            return GenerateCalculatorPage("Division", num1, num2, result, "÷");
        }
        return GenerateErrorPage("/divide", "Invalid numbers provided");
    }
    
    private string GenerateCalculatorPage(string operation, double num1, double num2, double result, string symbol)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Calculator - {operation}</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; background-color: #f0f0f0; }}
        .container {{ max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .result {{ font-size: 24px; color: #2c5aa0; text-align: center; margin: 20px 0; }}
        .calculation {{ font-size: 20px; text-align: center; margin: 15px 0; }}
        .operation-title {{ color: #333; text-align: center; margin-bottom: 20px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1 class='operation-title'>Calculator Result - {operation}</h1>
        <div class='calculation'>{num1} {symbol} {num2}</div>
        <div class='result'>= {result}</div>
        <hr>
        <p><strong>Operation:</strong> {operation}</p>
        <p><strong>Server:</strong> ASP.NET Core Application</p>
        <p><strong>Status:</strong> Calculation completed successfully</p>
    </div>
</body>
</html>";
    }
    
    private string GenerateErrorPage(string path, string error = "")
    {
        var errorMessage = string.IsNullOrEmpty(error) ? "Page not found or invalid parameters" : error;
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Calculator - Error</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; background-color: #f0f0f0; }}
        .container {{ max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .error {{ color: #d32f2f; font-size: 18px; text-align: center; margin: 20px 0; }}
        .help {{ margin-top: 30px; padding: 20px; background: #f5f5f5; border-radius: 5px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1 style='color: #d32f2f; text-align: center;'>Calculator Error</h1>
        <div class='error'>{errorMessage}</div>
        <div class='help'>
            <h3>Available endpoints:</h3>
            <ul>
                <li>/add?num1=4&num2=8 - Addition</li>
                <li>/subtract?num1=10&num2=3 - Subtraction</li>
                <li>/multiply?num1=5&num2=6 - Multiplication</li>
                <li>/divide?num1=20&num2=4 - Division</li>
            </ul>
        </div>
        <p><strong>Requested Path:</strong> {path}</p>
        <p><strong>Server:</strong> ASP.NET Core Application</p>
    </div>
</body>
</html>";
    }
}