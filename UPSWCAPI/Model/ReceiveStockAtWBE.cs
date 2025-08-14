namespace UPSWCAPI.Model
{
    public class ReceiveStockAtWBE
    {

        public int ReceiveId { get; set; }
        public string? ChallanNumber { get; set; }
        public int ReceivedBy { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string? FinancialYear { get; set; }
        public string? WarehouseName { get; set; }
        public string? CommodityName { get; set; }
        public string? AgencyName { get; set; }
        public string? EmpName { get; set; }
        public string? VehicleNumber { get; set; }
        public int TruckWeight { get; set; }
        public string? BagType { get; set; }
        public double? BagTypeWeight { get; set; }
    }

    public class IssueStockAtWBE
    {

        public int IssueId { get; set; }
        public int ReceiveId { get; set; }
        public string? ChallanNumber { get; set; }
        public int ReceivedBy { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string? FinancialYear { get; set; }
        public string? WarehouseName { get; set; }
        public string? CommodityName { get; set; }
        public string? AgencyName { get; set; }
        public string? EmpName { get; set; }
        public string? VehicleNumber { get; set; }
        public int TruckWeight { get; set; }
        public string? BagType { get; set; }
        public double? BagTypeWeight { get; set; }
    }
}
