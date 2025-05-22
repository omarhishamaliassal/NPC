/*using Microsoft.AspNetCore.Mvc;
using WebApplication7.Data;
using WebApplication7.Models;
using System;

namespace WebApplication7.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View("~/Views/Dashboard/About.cshtml");
        }

        [HttpPost]
        public IActionResult About(About model)
        {
            Console.WriteLine("POST About called");
            Console.WriteLine($"ChairmanWord: {model.ChairmanWord}");
            Console.WriteLine($"AboutId: {model.AboutId}");

            if (ModelState.IsValid)
            {
                try
                {
                    model.EntryDate = DateTime.Now;
                    _context.About.Add(model);
                    int rows = _context.SaveChanges();
                    Console.WriteLine($"SaveChanges successful, rows affected: {rows}");

                    ViewBag.Message = "✅ تم الحفظ بنجاح.";
                    return View("~/Views/Dashboard/About.cshtml", model);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    ViewBag.Message = $"❌ خطأ أثناء الحفظ: {ex.Message}";
                    return View("~/Views/Dashboard/About.cshtml", model);
                }
            }
            else
            {
                foreach (var v in ModelState.Values)
                {
                    foreach (var error in v.Errors)
                    {
                        Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                    }
                }
            }

            ViewBag.Message = "❌ النموذج غير صالح، تحقق من البيانات.";
            return View("~/Views/Dashboard/About.cshtml", model);
        }
    }
}
*/