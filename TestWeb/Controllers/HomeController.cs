using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;

namespace TestWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITodoService _todoService;

    public HomeController(ILogger<HomeController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Index!!!");
        var tasks = await _todoService.GetPendingTodos();
        return View(tasks);
    }

    /* Más comodo con Task<IActionResult> Equivalente a:
    public IActionResult Index()
    {
        _logger.LogInformation("Index!!!");
        IEnumerable<TodoTask> tasks;
        _todoService.GetPendingTodos().ContinueWith( (t) => {
            tasks = t.Result;
        });
        return View(tasks);
    }   
    */

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
