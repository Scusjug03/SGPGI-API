namespace UPSWCAPI.Model
{
    public class DemandInsertDto
    {
        //public int? DemandId { get; set; }
        public int? MakeId { get; set; }
        public decimal? OfficeDemandQty { get; set; }
        public decimal? HOAppQty { get; set; }
        public decimal? RmAppQty { get; set; }
        public int? StatusId { get; set; }
        public string? Status { get; set; }
        public int? UnitId { get; set; }
        public int? ItemId { get; set; }
        public string? OfficeRemarks { get; set; }
        public string? HORemarks { get; set; }
        public string? RMOfficeRemarks { get; set; }
        public int? UserId { get; set; }
    }
}
