using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPSWCAPI.Model
{
    public class EnquiryStatus
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        [Required]
        public string status { get; set; } = string.Empty;
    }
}
