using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsControllerAPI(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
            public async Task<ActionResult<IEnumerable<NewsAPI>>> GetNews()

        {
            var news = await _context.News
                .Where(n => n.IsActive == 1)
                .Select(n => new
                {
                    n.NewsId,
                    n.Title,
                    n.NewsText,
                    n.SmallPhoto,
                    n.LargPhoto,
                    n.PublishDate,
                    n.NewsSource,
                    n.URL,
                    n.OnClockTopic,
                    n.OnMainPage
                })
                .ToListAsync();

            return Ok(news);
        }

      
        [HttpGet("GetById")]
        public async Task<ActionResult<NewsAPI>> GetById([FromQuery] decimal id)
        {
            var newsItem = await _context.News
                .Where(n => n.IsActive == 1 && n.NewsId == id)
                .Select(n => new
                {
                    n.NewsId,
                    n.Title,
                    n.NewsText,
                    n.SmallPhoto,
                    n.LargPhoto,
                    n.PublishDate,
                    n.NewsSource,
                    n.URL,
                    n.OnClockTopic,
                    n.OnMainPage
                })
                .FirstOrDefaultAsync();

            if (newsItem == null)
            {
                return NotFound(new { message = "News not found." });
            }

            return Ok(newsItem);
        }
    }
}
