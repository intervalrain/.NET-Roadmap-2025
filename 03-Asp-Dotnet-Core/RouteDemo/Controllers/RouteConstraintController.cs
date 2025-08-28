using Microsoft.AspNetCore.Mvc;

namespace RouteDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteConstraintController : ControllerBase
{
    [HttpGet("int/{id:int}")]
    public IActionResult GetIntAsync(int id) => Ok(id);

    [HttpGet("alpha/{id:alpha}")]
    public IActionResult GetAlphaAsync(string id) => Ok(id);

    [HttpGet("bool/{id:bool}")]
    public IActionResult GetBoolAsync(bool id) => Ok(id);
    
    [HttpGet("datetime/{id:datetime}")]
    public IActionResult GetDateTimeAsync(DateTime id) => Ok(id);

    [HttpGet("decimal/{id:decimal}")]
    public IActionResult GetDecimalAsync(decimal id) => Ok(id);

    [HttpGet("double/{id:double}")]
    public IActionResult GetDoubleAsync(double id) => Ok(id);

    [HttpGet("float/{id:float}")]
    public IActionResult GetFloatAsync(float id) => Ok(id);

    [HttpGet("guid/{id:guid}")]
    public IActionResult GetGuidAsync(Guid id) => Ok(id);

    [HttpGet("length/{id:length(12)}")]
    public IActionResult GetLengthAsync(string id) => Ok(id);
    
    [HttpGet("maxlength/{id:maxlength(8)}")]
    public IActionResult GetMaxLengthAsync(string id) => Ok(id);

    [HttpGet("minlength/{id:minlength(4)}")]
    public IActionResult GetMinLengthAsync(string id) => Ok(id);

    [HttpGet("range/{id:range(18,120)}")]
    public IActionResult GetRangeAsync(int id) => Ok(id);

    [HttpGet("min/{id:min(18)}")]
    public IActionResult GetMinAsync(int id) => Ok(id);

    [HttpGet("max/{id:max(120)}")]
    public IActionResult GetMaxAsync(int id) => Ok(id);

    [HttpGet("email/{id:regex(^[[a-zA-Z0-9._%+-]]+@[[a-zA-Z0-9.-]]+\\.[[a-zA-Z]]+$)}")]
    public IActionResult GetEmailAsync(string id) => Ok(id);
}