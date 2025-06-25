namespace UPSWCAPI.Model
{
    public class EmpLicDetailModel
    {
        public int EmpId { get; set; }
        public int LicId { get; set; }
        public string LicNo { get; set; } = string.Empty;
        public decimal LicAmount { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public int EndDate { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

}
