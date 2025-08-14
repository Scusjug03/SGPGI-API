namespace UPSWCAPI.Model
{
    public class ReceiveStockAtWindow
    {
        public int WarehouseId { get; set; }
        public int OfficeId { get; set; }
        public int FinancialYear { get; set; }
        public int CommodityId { get; set; }
        public int AgencyTypeId { get; set; }
        public string VehicleNumber { get; set; }
        public string ChallanNumber { get; set; }
        public int UserID { get; set; }
    }


    public class IssueStockAtWindow
    {
        public int WarehouseId { get; set; }
        public int OfficeId { get; set; }
        public int FinancialYear { get; set; }
        public int CommodityId { get; set; }
        public int AgencyTypeId { get; set; }
        public string VehicleNumber { get; set; }
        public string ChallanNumber { get; set; }
        public int UserID { get; set; }
    }
}
