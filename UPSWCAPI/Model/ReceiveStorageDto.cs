namespace UPSWCAPI.Model
{
    public class ReceiveStorageDto
    {
        public int WarehouseId { get; set; }
        public int CommodityId { get; set; }
        public int AgencyTypeId { get; set; }
        public string SerialNumber { get; set; }
        public string LotNumber { get; set; }
        public string SenderName { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public decimal TruckWeight { get; set; }
        public int TotalNoOfBags { get; set; }
        public int NoOfBag_1 { get; set; }
        public decimal WeightPerBag_1_kg { get; set; }
        public decimal BagWeight_1_kg { get; set; }
        public int? NoOfBag_2 { get; set; }
        public decimal? WeightPerBag_2_kg { get; set; }
        public decimal? BagWeight_2_kg { get; set; }
        public decimal TotalBagWeight_kg { get; set; }
        public decimal NetWeight_kg { get; set; }
        public decimal MaterialWeight_kg { get; set; }
        public string VehicleNumber { get; set; }
        public string GatePassNumber { get; set; }
        public string ReceiverName { get; set; }
        public string WeightingWay { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}
