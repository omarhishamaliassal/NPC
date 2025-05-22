using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal NewsId { get; set; }

        public string Title { get; set; }

      
        public string NewsText { get; set; }
        public string? SmallPhoto { get; set; }
        public string? LargPhoto { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? NewsSource { get; set; }
        public string? URL { get; set; }
        public int? OnClockTopic { get; set; }
        public int? OnMainPage { get; set; }
        public int? IsActive { get; set; }
        public decimal? EntryId { get; set; }
        public DateTime? EntryDate { get; set; }
        public decimal? ModifyId { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
