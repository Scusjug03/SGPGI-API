using System.ComponentModel.DataAnnotations;

namespace UPSWCAPI.Model
{
    public class IssueStorage
    {
        public DateTime? IssueDate { get; set; }
        public int? WarehouseId { get; set; }
        public int? FinancialYear { get; set; }
        public int? ReceiveId { get; set; }
        public decimal? RequestedIssueQuantity { get; set; }
        //public string? SerialNumber { get; set; }
        public string? LotNumber { get; set; }
        public int? CommodityId { get; set; }
        public int? AgencyTypeId { get; set; }
        public string? SenderName { get; set; }
        public string? SubSenderName { get; set; }
        //public decimal? RemainingStock { get; set; }
        //public int? RemainingBags { get; set; }
        public decimal? GrossWeight_kg { get; set; }
        public decimal? TruckWeight_kg { get; set; }
        public int? NoOfBag_1 { get; set; }
        public decimal? WeightPerBag_1_kg { get; set; }
        public decimal? BagWeight_1_kg { get; set; }
        public int? NoOfBag_2 { get; set; }
        public decimal? WeightPerBag_2_kg { get; set; }
        public decimal? BagWeight_2_kg { get; set; }
        public int? TotalNoOf_bag { get; set; }
        public decimal? TotalBagWeight { get; set; }
        public decimal? NetWeight { get; set; }
        public decimal? MaterialWeight_kg { get; set; }
        public string? VehicleNo { get; set; }
        public string? GatePassNo { get; set; }
        public string? IssuerName { get; set; }
        public string? WeightingWay { get; set; }

    }


    //class BulkIssueStorage
    //{
    //    [Required(ErrorMessage = "RequestedTotalIssueQuantity is required")]
    //    public string RequestedTotalIssueQuantity { get; set; }

    //    [Required]
    //    public DateTime DataOfIssue { get; set; }

    //    [Required]
    //    public List<IssueStorage> Details { get; set; } = new();
    //}


   
    public class ReceiveStorageReportRequest
    {
        public int RegionId { get; set; }
        public int OfficeId { get; set; }
        public int GodownId { get; set; }
        public int AgencyId { get; set; }
        public int CommodityId { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public int  ProcId { get; set; }
    }

}
