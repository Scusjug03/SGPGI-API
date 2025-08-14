namespace UPSWCAPI.Model
{
    public class ReceiveStorageAtQc
    {
        public int ReceiveId { get; set; }
        public decimal Moisture { get; set; }
        public decimal OtherAgencyMoisture { get; set; }
        public int TotalWorker { get; set; }
        public string? WorkerNames { get; set; }
        public bool IsStockWeightTaken { get; set; }
        public decimal? Weight1 { get; set; }
        public int? NoOfBag1 { get; set; }
        public decimal? BagWeight1 { get; set; }
        public decimal? Weight2 { get; set; }
        public int? NoOfBag2 { get; set; }
        public decimal? BagWeight2 { get; set; }
        public string ActionType { get; set; } // 'Close', 'Reject', 'Forward'
        public int UpdatedBy { get; set; }
    }


    public class ReceiveStorageFinal
    {
        public int ReceiveId { get; set; }

        public double EmptyTruckWeightKg { get; set; }

        public int TotalNoOfBags { get; set; }

        public double TotalBagWeight_kg { get; set; }

        public double NetWeight { get; set; }

        public string? EnchargeName { get; set; }
        public int? UpdatedBy { get; set; }

    }


    // Issue Storage Models
    public class IssueStorageAtQc
    {
        public int ReceiveId { get; set; }
        public decimal Moisture { get; set; }
        public decimal OtherAgencyMoisture { get; set; }
        public int TotalWorker { get; set; }
        public string? WorkerNames { get; set; }
        public bool IsStockWeightTaken { get; set; }
        public decimal? Weight1 { get; set; }
        public int? NoOfBag1 { get; set; }
        public decimal? BagWeight1 { get; set; }
        public decimal? Weight2 { get; set; }
        public int? NoOfBag2 { get; set; }
        public decimal? BagWeight2 { get; set; }
        public string ActionType { get; set; } // 'Close', 'Reject', 'Forward'
        public int UpdatedBy { get; set; }
    }

    public class IssueStorageFinal
    {
        public int ReceiveId { get; set; }

        public double TruckWeightKg { get; set; }

        public int TotalNoOfBags { get; set; }

        public double TotalBagWeight_kg { get; set; }

        public double NetWeight { get; set; }

        public string? EnchargeName { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
