namespace UPSWCAPI.Model
{
    public class EmpLoanDetailModel
    {
        public int EmpId { get; set; }
        public int LoanTypeId { get; set; }
        public string? LoanType { get; set; }
        public string? EmpName { get; set; }
        public string LoanNo { get; set; } = string.Empty;
        public decimal LoanAmount { get; set; }
        public int TotalInst { get; set; }
        public decimal InstAmount { get; set; }
        public decimal TotPre { get; set; }
        public decimal CurrInst { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public int RecTotInst { get; set; }
        public string Status { get; set; } = string.Empty;
        public int SNo { get; set; }
    }
}
