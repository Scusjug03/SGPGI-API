namespace UPSWCAPI.Model
{
    public class EmpAllowanceModel
    {
        public int EmpId { get; set; }
        public decimal? GradePay { get; set; }
        public decimal? DAPer { get; set; }
        public decimal? IR { get; set; }
        public decimal? PerPay { get; set; }
        public decimal? Bla { get; set; }
        public decimal? Other { get; set; }
        public decimal? LWP { get; set; }
        public decimal? LevelC { get; set; }
        public decimal? Arrear { get; set; }
        public decimal? PayFixation { get; set; }
        public decimal? HeavyDuty { get; set; }
        public decimal? MiscAmt1 { get; set; }
        public string? MiscDesc1 { get; set; }
        public decimal? MiscAmt2 { get; set; }
        public string? MiscDesc2 { get; set; }
        public decimal? MiscAmt3 { get; set; }
        public string? MiscDesc3 { get; set; }
        public decimal? MiscAmt4 { get; set; }
        public string? MiscDesc4 { get; set; }
        public int? DAStatus { get; set; }
        public int ProcId { get; set; }

    }

    public class EmpDeductionModel
    {
        public int ProcId { get; set; }
        public int EmpId { get; set; }

        public decimal? CPF { get; set; }
        public decimal? Vol { get; set; }
        //  public decimal? CPFAdv { get; set; }
        public decimal? NSC { get; set; }
        public decimal? LIC { get; set; }
        public decimal? GI { get; set; }
        public decimal? ITax { get; set; }
        //public decimal? SCCar { get; set; }
        //public decimal? SCCarInt { get; set; }
        //public decimal? BidAdv { get; set; }
        //public decimal? BidInt { get; set; }
        //public decimal? HDFC { get; set; }
        public decimal? RD { get; set; }
        //public decimal? NameAC { get; set; }
        public decimal? CoUnionInsurance { get; set; }
        public decimal? NSF { get; set; }
        public decimal? EleCharges { get; set; }
        public decimal? HatkarghaNigamAdv { get; set; }
        //public decimal? FestivalAdv { get; set; }
        //public decimal? Genloan { get; set; }
        //public decimal? GenLoanInt { get; set; }
        //public decimal? CALoan { get; set; }
        //public decimal? CALoanInt { get; set; }

        public decimal? MiscAmt1 { get; set; }
        public string MiscDesc1 { get; set; }
        public decimal? MiscAmt2 { get; set; }
        public string MiscDesc2 { get; set; }
        public decimal? MiscAmt3 { get; set; }
        public string MiscDesc3 { get; set; }
        public decimal? MiscAmt4 { get; set; }
        public string MiscDesc4 { get; set; }
    }

}
