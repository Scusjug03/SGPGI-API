namespace UPSWCAPI.Model
{
    public class ReceiveStockAtWbIn
    {
        public decimal TruckWeight { get; set; }
        public int TotalNoOfBags { get; set; }
        public int GodownId { get; set; }
        public int StackId { get; set; }
        public int WbInBy { get; set; }
        public string? BagType { get; set; }
        public decimal? BagTypeWeight { get; set; }
    }

    public class IssueStockAtWbIn
    {
        public decimal TruckWeight { get; set; }
        public int? ReceiveId { get; set; }
        public int TotalNoOfBags { get; set; }
        public int GodownId { get; set; }
        public int StackId { get; set; }
        public int WbInBy { get; set; }
        public string? BagType { get; set; }
        public decimal? BagTypeWeight { get; set; }
    }
}
