namespace UPSWCAPI.Model
{
    public class NAFEDReportRequest
    {
        public int WarehouseId { get; set; }
        public int AgencyTypeId { get; set; }
        public int CommodityId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int ProcId { get; set; }
        public string Remark { get; set; } = string.Empty;
    }

    public class BillRequestDto
    {
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public int? Agencytypeid { get; set; }
        public int? CommodityId { get; set; }   // optional
        public int? WarehouseId { get; set; }   // optional
        public int? UesrId { get; set; }
        public string Remark { get; set; } = string.Empty;
    }

    public class NafedForwardDto
    {
        public int WarehouseId { get; set; }
        public int AgencyTypeId { get; set; }
        public int CommodityId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int ProcId { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
