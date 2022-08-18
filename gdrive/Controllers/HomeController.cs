using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gdrive.Models;
using System.Reflection;

namespace gdrive.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }
   
    [HttpPost]
    public ActionResult CreateFolder(String FolderName)
    {
        GoogleDriveFilesRepository.CreateFolder(FolderName);
        return Ok("folder has been created");
    }




}

