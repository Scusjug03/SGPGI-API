namespace UPSWCAPI.Model
{
    public class StorageRequest
    {
        public DateTime ReceiveDate { get; set; }
        public int WarehouseId { get; set; }
        public string LotNumber { get; set; }
        public int CommodityId { get; set; }
        public int AgencyTypeId { get; set; }
        public decimal GrossWeight { get; set; }
        public int TotalNoOfBags { get; set; }
        public decimal TotalBagWeightKg { get; set; }
    }
}
