namespace UPSWCAPI.Model
{
    public class WarehouseDetail
    {
        public int id { get; set; }
        public int warehouseId { get; set; }
        public int? goDownId { get; set; }
        public int? stackId { get; set; }
        public int? serialNoId { get; set; }
        public int? lotNoId { get; set; }
        public int? itemCategoryId { get; set; }
        public int? itemNameId { get; set; }
        public int? agencyTypeId { get; set; }
        public int? agencyNameId { get; set; }
        public string senderName { get; set; }
        public decimal grossWeight { get; set; }
        public decimal truckWeight { get; set; }
        public int noOfBags { get; set; }
        public decimal avgPerBagWeight { get; set; }
        public decimal netWeight { get; set; }
        public decimal materialWeight { get; set; }
        public int vehicleNoId { get; set; }
        public string gatePassNo { get; set; }
        public string receiverName { get; set; }
        public string receiverDate { get; set; }
        public int weightingWayId { get; set; }
        public int procId { get; set; }
        public string? msg { get; set; }
    }
}
