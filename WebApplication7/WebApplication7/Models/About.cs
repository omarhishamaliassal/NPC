using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class About
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal AboutId { get; set; }


        public string? AboutText { get; set; }
        public string? AboutImg { get; set; }

        [Required(ErrorMessage = "يجب إدخال كلمة الرئيس")]
        public string ChairmanWord { get; set; }

        public string? ChairmanImg { get; set; }
        public string? MinisterWord { get; set; }
        public string? MinisterImg { get; set; }
        public string? StructureImg { get; set; }
        public string? Pillar1 { get; set; }
        public string? Pillar2 { get; set; }
        public string? Pillar3 { get; set; }
        public string? Mechanisms { get; set; }
        public int? EntryId { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? ModifyId { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
