using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportMicroservice.Models
{
    public class Support
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerSupportId { get; set; }
        public string Query { get; set; }
        public int Status { get; set; }
        public string Reply { get; set; }
        public int BookingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Assignee { get; set; }
    }
}
