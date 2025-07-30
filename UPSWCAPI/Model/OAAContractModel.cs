namespace UPSWCAPI.Model
{
    public class OAAContractModel
    {
        public int? ProcId { get; set; }
        public int? OaaId { get; set; }
        public string? OaaName { get; set; }
        public int? WareHouseId { get; set; }
        public int? Officeid { get; set; }
        public int? GodownId { get; set; }
        public decimal? ContractRate { get; set; }
        public DateTime? ContractStartDt { get; set; }
        public DateTime? ContractEndDt { get; set; }
        public string? Remarks { get; set; }
    }
}
