using System.ComponentModel.DataAnnotations;
namespace WebApplication7.Models
{
    public class LkpUser
    {
        [Key]
        public decimal UserId { get; set; }

        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
