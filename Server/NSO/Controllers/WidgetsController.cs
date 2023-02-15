using Microsoft.AspNetCore.Mvc;
using NSO.Requests;

namespace NSO.Controllers;

[ApiController]
[Route("[controller]")]
public class WidgetsController : ControllerBase
{
    private readonly ILogger<WidgetsController> _logger;

    public WidgetsController(ILogger<WidgetsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetWidgets")]
    public IActionResult GetWidgets()
    {
        try
        {
            _logger.LogDebug($"***************** Start GetWidgets ****************");
            System.Console.Write("Start GwtWidgets");
            var widgets = NSO.BL.WidgetsBL.GetWidgetsList();
            _logger.LogDebug($"***************** Done GetWidgets ****************");
            System.Console.WriteLine("Done GwtWidgets");
            return Ok(widgets);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"GwtWidgets ERROR: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AddWidget")]
    public async Task<IActionResult> AddWidget([FromBody] AddWidgetReq obj)
    {
        try
        {
            _logger.LogDebug($"***************** Start AddWidget ****************");
            //var test = (AddWidgetReq)obj;
            System.Console.Write("Start AddWidget");
            System.Console.Write(obj);
            var widgets = await NSO.BL.WidgetsBL.AddWidgetBL(obj);
            _logger.LogDebug($"***************** Done AddWidget ****************");
            System.Console.WriteLine("Done AddWidget");
            return Ok(widgets);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"AddWidget ERROR: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("UpdateWidget")]
    public async Task<IActionResult> UpdateWidget([FromBody] AddWidgetReq obj)
    {
        try
        {
            _logger.LogDebug($"***************** Start UpdateWidget ****************");
            //var test = (AddWidgetReq)obj;
            System.Console.Write("Start AddWidget");
            System.Console.Write(obj);
            var widgets = await NSO.BL.WidgetsBL.UpdateWidgetBL(obj);
            _logger.LogDebug($"***************** Done UpdateWidget ****************");
            System.Console.WriteLine("Done UpdateWidget");
            return Ok(widgets);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"UpdateWidget ERROR: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("RemoveWidget")]
    public async Task<IActionResult> RemoveWidget([FromBody] AddWidgetReq obj)
    {
        try
        {
            _logger.LogDebug($"***************** Start RemoveWidget ****************");
            //var test = (AddWidgetReq)obj;
            System.Console.Write("Start RemoveWidget");
            System.Console.Write(obj);
            var widgets = await NSO.BL.WidgetsBL.RemoveWidgetBL(obj);
            _logger.LogDebug($"***************** Done RemoveWidget ****************");
            System.Console.WriteLine("Done RemoveWidget");
            return Ok(widgets);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"RemoveWidget ERROR: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
}

