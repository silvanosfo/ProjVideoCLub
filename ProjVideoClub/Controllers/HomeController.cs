using ProjVideoClub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;

namespace ProjVideoClub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}


