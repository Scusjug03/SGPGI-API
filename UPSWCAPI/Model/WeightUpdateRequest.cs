namespace UPSWCAPI.Model
{
    public class WeightUpdateRequest
    {
        public int ReceiveId { get; set; }
        public decimal TruckWeight { get; set; }
        public decimal NetWeight { get; set; }
    }
}
