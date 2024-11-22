using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class LabSelectorController : Controller
{

    public LabSelectorController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
