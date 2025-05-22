using System.ComponentModel.DataAnnotations;
namespace WebApplication7.Models
{
    public class NewsAPI
    {
       
        [Key]
        public decimal NewsId { get; set; }

        public string? Title { get; set; }
        public string? NewsText { get; set; }
        public string? SmallPhoto { get; set; }
        public string? LargPhoto { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? NewsSource { get; set; }
        public string? URL { get; set; }

     
        public int? OnClockTopic { get; set; }
        public int? OnMainPage { get; set; }

        public int? IsActive { get; set; }      

    }
}
