using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPSWCAPI.Model
{
    public class EnquiryType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int typeId { get; set; }
        [Required]
        public string typeName { get; set; } = string.Empty;
    }
}
