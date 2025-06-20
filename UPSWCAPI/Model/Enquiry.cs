using System.ComponentModel.DataAnnotations;

namespace UPSWCAPI.Model
{
    public class Enquiry
    {
        [Key]
        public int enquiryId { get; set; }
        [Required]
        public int enquiryTypeId { get; set; }
        public int enquiryStatusId { get; set; }
        public string customerName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public DateTime createdDate { get; set; }
        public string resolution { get; set; } = string.Empty;
        public int ProcId { get; set; }

    }
}
