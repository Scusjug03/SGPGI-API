namespace UPSWCAPI.Model
{
    public class ReceiveInsertDto
    {
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public string PurchaseNo { get; set; } = string.Empty;
        public bool IsHoOrRm { get; set; }
        public bool IsPurchase { get; set; }
        public List<ReceiveEntry> Entries { get; set; } = new();
    }

    public class OpeningModel
    {
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public int MakeId { get; set; }
        public decimal ReceiptQty { get; set; }

    }

    public class ReceiveEntry
    {
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public int MakeId { get; set; }
        public decimal ReceiptQty { get; set; }
    }

    // Entry-level data: one per row
    public class PurchaseEntryDto
    {
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public int MakeId { get; set; }
        public decimal ReceiptQty { get; set; }
        public decimal BasicRate { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal IGSTPer { get; set; }
    }

    // Header-level data with list of entry rows
    public class PurchaseInsertDto
    {
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public int SupplierId { get; set; }
        public string PurchaseNo { get; set; }  // Optional if auto-generated
        public string ChallanNo { get; set; }
        public DateTime? ChallanDt { get; set; }

        public List<PurchaseEntryDto> Entries { get; set; }
    }

}
