using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NumToWordsCurrency.Interfaces;
using NumToWordsCurrency.Models;

namespace NumToWordsCurrency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INumToWords _numToWords;

        public HomeController(ILogger<HomeController> logger, INumToWords numToWords)
        {
            _logger = logger;
            _numToWords = numToWords;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Output(Form formInput)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Form formOutput = _numToWords.ConvertNumToWords(formInput);
                    return View("Index", formOutput);
                }
                catch (FormatException fe)
                {
                    ModelState.AddModelError("InvalidInput", fe.Message);
                    return View("Index", formInput);
                }
                catch(Exception)
                {
                    ModelState.AddModelError("Error", "An unknown exceptions has occured");
                    return View("Index", formInput);
                }
            }
           
            return View("Index", formInput);
        }

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
}
