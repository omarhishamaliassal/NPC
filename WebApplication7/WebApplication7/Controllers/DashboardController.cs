/*api using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication7.Data;
using WebApplication7.Models;
using Microsoft.AspNetCore.Http; // For session access

namespace WebApplication7.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult About(About model)
        {
            Console.WriteLine("POST About called");
            Console.WriteLine($"ChairmanWord: {model.ChairmanWord}");
            Console.WriteLine($"AboutId: {model.AboutId}");

            if (ModelState.IsValid)
            {
                try
                {
                    // ===== Get current user ID from session =====
                    var currentUserIdString = HttpContext.Session.GetString("UserId");
                    if (decimal.TryParse(currentUserIdString, out decimal currentUserId))
                    {
                        model.EntryId = currentUserId; // Set EntryId on new About record
                    }
                    else
                    {
                        Console.WriteLine("UserId not found in session or invalid.");
                        // You might want to handle unauthorized case here
                    }
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
        */
/*api
        public IActionResult Indicators() => View("Page", "مؤشرات المبادرة");
        public IActionResult Services() => View("Page", "خدمات المبادرة");
        public IActionResult Media() => View();
        public IActionResult Contact() => View("Page", "تواصل معنا");

        // 🆕 Updated MediaNews functionality with Title and NewsText only

        [HttpGet]
        public IActionResult MediaNews(int? id)
        {
            News model;

            if (id.HasValue)
            {
                // Load the news to edit
                model = _context.News.FirstOrDefault(n => n.NewsId == id.Value);

                if (model == null)
                {
                    ViewBag.Message = "❌ الخبر المطلوب غير موجود.";
                    model = new News(); // fallback to empty form
                }
            }
            else
            {
                model = new News(); // empty form for new news
            }

            // Load all news including NewsId for edit button forms
            var allNews = _context.News
                .Select(n => new
                {
                    n.NewsId,
                    n.Title,
                    n.NewsText,
                    n.URL,
                    n.IsActive,
                    n.EntryId,
                    n.OnClockTopic,
                    n.NewsSource,
                    n.OnMainPage,
                    n.PublishDate
                })
                .ToList();

            ViewBag.AllNews = allNews;
            return View(model);
        }

        [HttpPost]
        public IActionResult MediaNews(News model, IFormFile? SmallPhotoFile, IFormFile? LargPhotoFile)
        {
            Console.WriteLine("POST MediaNews hit");
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"❌ Error in field: {modelState.Key} - {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                var currentUserIdString = HttpContext.Session.GetString("UserId");
                if (!decimal.TryParse(currentUserIdString, out decimal currentUserId))
                {
                    ViewBag.Message = "❌ لم يتم تحديد المستخدم الحالي.";
                    return View(model);
                }
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Upfiles", "News");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                // 📂 Save uploaded file to Upfiles/News
                if (SmallPhotoFile != null && SmallPhotoFile.Length > 0)
                {
                   

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(SmallPhotoFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        SmallPhotoFile.CopyTo(fileStream);
                    }

                    model.SmallPhoto = uniqueFileName; // Save only the name to DB
                }
                else
                {
                    model.SmallPhoto = model.SmallPhoto; // redundant, but clear
                }

                if (LargPhotoFile != null && LargPhotoFile.Length > 0)
                {
                   

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(LargPhotoFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        LargPhotoFile.CopyTo(fileStream);
                    }

                    model.LargPhoto = uniqueFileName; // Save only the name to DB
                }
                else
                {
                    model.LargPhoto = model.LargPhoto; // redundant, but clear
                }


                if (model.NewsId > 0)
                {
                    // 🔁 Edit existing
                    var existingNews = _context.News.FirstOrDefault(n => n.NewsId == model.NewsId);
                    if (existingNews != null)
                    {
                        existingNews.Title = model.Title;
                        existingNews.NewsText = model.NewsText;
                        existingNews.URL = model.URL;
                        existingNews.IsActive = model.IsActive;
                        existingNews.PublishDate = model.PublishDate;
                        existingNews.OnClockTopic = model.OnClockTopic;
                        existingNews.NewsSource = model.NewsSource;
                        existingNews.OnMainPage = model.OnMainPage;
                        existingNews.ModifyId = currentUserId;
                        existingNews.ModifyDate = DateTime.Now;

                        if (!string.IsNullOrEmpty(model.SmallPhoto))
                        {
                            existingNews.SmallPhoto = model.SmallPhoto;
                        }
                        if (!string.IsNullOrEmpty(model.LargPhoto))
                        {
                            existingNews.LargPhoto = model.LargPhoto;
                        }

                        _context.SaveChanges();
                        ViewBag.Message = "✅ تم تعديل الخبر بنجاح.";
                    }
                }
                else
                {
                    // 🆕 Insert
                    model.EntryDate = DateTime.Now;
                    model.EntryId = currentUserId;
                    _context.News.Add(model);
                    _context.SaveChanges();

                    ViewBag.Message = "✅ تم حفظ الخبر بنجاح.";
                }

                // Reload AllNews for display
                ViewBag.AllNews = _context.News
                    .Select(n => new
                    {
                        n.NewsId,
        n.Title,
        n.NewsText,
        n.OnMainPage,
        n.IsActive,
        n.NewsSource,
        n.OnClockTopic,
        n.URL,
        n.SmallPhoto,
        n.LargPhoto,
        n.EntryId,
        n.PublishDate
                    }).ToList();

                return model.NewsId > 0 ? View(model) : View(new News());
            }

            ViewBag.Message = "❌ يرجى التأكد من صحة البيانات.";
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNews(decimal id)
        {
            Console.WriteLine("✅ DeleteNews called with ID: " + id);

            var news = _context.News.FirstOrDefault(n => n.NewsId == id);
            if (news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
                Console.WriteLine("✅ News deleted.");
            }
            else
            {
                Console.WriteLine("❌ News not found.");
            }

            return RedirectToAction("MediaNews");
        }


        // Optional: Other Media sections
        public IActionResult MediaPhotos() => View("Page", "ألبومات الصور");
        public IActionResult MediaVideos() => View("Page", "مكتبة الفيديوهات");
        public IActionResult MediaInfographics() => View("Page", "قائمة الإنفوجراف");
        public IActionResult MediaAwareness() => View("Page", "الرسائل التوعوية");
    }
}
api*/