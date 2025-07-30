namespace UPSWCAPI.Model
{
    public class PayRegister
    {
        public int EmpId { get; set; }
        public string DptEmpCode { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public string CPfAccountNo { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; } 
        public string PanNo { get; set; } = string.Empty;
        public string LwpOrderNo { get; set; } = string.Empty;
        public string PayslipDt { get; set; } = string.Empty;
        public string Absence { get; set; } = string.Empty;
        public int PayYear { get; set; }
        public int PayMonth { get; set; }
        public decimal WorkDays { get; set; }
        public decimal SusDays { get; set; }
        public decimal SusPer { get; set; }
        public int GradePayId { get; set; }
        public int CircleId { get; set; }
        public decimal BasicSal { get; set; }
        public decimal GradePay { get; set; }
        public decimal DAPer { get; set; }
        public decimal DA { get; set; }
        public decimal MA { get; set; }
        public decimal WA { get; set; }
        public decimal IR { get; set; }
        public decimal CCA { get; set; }
        public decimal HRA { get; set; }
        public decimal PerPay { get; set; }
        public decimal SpecPay { get; set; }
        public decimal PenContribution { get; set; }
        public decimal PPFContribution { get; set; }
        public decimal BilangAllow { get; set; }
        public decimal NPSEmployer { get; set; }
        public decimal NPSEmployee { get; set; }
        public decimal DisAllow { get; set; }
        public decimal DepAllow { get; set; }
        public decimal VehicleAllow { get; set; }
        public decimal OtherAllow { get; set; }
        public decimal MiscAllowAmt1 { get; set; }
        public string MiscAllowDesc1 { get; set; } = string.Empty;
        public decimal MiscAllowAmt2 { get; set; }
        public string MiscAllowDesc2 { get; set; } = string.Empty;
        public decimal MiscAllowAmt3 { get; set; }
        public string MiscAllowDesc3 { get; set; } = string.Empty;
        public decimal MiscAllowAmt4 { get; set; }
        public string MiscAllowDesc4 { get; set; } = string.Empty;
        public decimal MiscDedAmt1 { get; set; }
        public string MiscDedDesc1 { get; set; } = string.Empty;
        public decimal MiscDedAmt2 { get; set; }
        public string MiscDedDesc2 { get; set; } = string.Empty;
        public decimal MiscDedAmt3 { get; set; }
        public string MiscDedDesc3 { get; set; } = string.Empty;
        public decimal MiscDedAmt4 { get; set; }
        public string MiscDedDesc4 { get; set; } = string.Empty;
        public decimal GrossPay { get; set; }
        public int GPFTypeId { get; set; }
        public decimal GPF { get; set; }
        public decimal PPF { get; set; }
        public decimal LIC { get; set; }
        public decimal GIS { get; set; }
        public decimal GI { get; set; }
        public decimal VehicleCharges { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal CourtReco { get; set; }
        public decimal QtrRent { get; set; }
        public decimal Society { get; set; }
        public decimal GPFAdv { get; set; }
        public decimal CPFAdv { get; set; }
        public decimal SCCar { get; set; }
        public decimal SCCarInt { get; set; }
        public decimal BidAdv { get; set; }
        public decimal BidInt { get; set; }
        public decimal HDFC { get; set; }
        public decimal NameAC { get; set; }
        public decimal FestivalAdv { get; set; }
        public decimal Genloan { get; set; }
        public decimal GenLoanInt { get; set; }
        public decimal CALoan { get; set; }
        public decimal CALoanInt { get; set; }
        public decimal BLA { get; set; }
        public decimal BLAInt { get; set; }
        public decimal MiscReco { get; set; }
        public decimal OtherReco { get; set; }
        public decimal StaffAdv { get; set; }
        public decimal CoOperative { get; set; }
        public decimal Penalty { get; set; }
        public decimal Rd { get; set; }
        public decimal HatkarghaNigamAdv { get; set; }
        public decimal NSF { get; set; }
        public decimal NetPay { get; set; }
        public decimal EleCharges { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime entrydate { get; set; }
        public string salarytype { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public int DepartmentId { get; set; }
        public int SubDeptId { get; set; }
        public int DesigId { get; set; }
        public int CommssionId { get; set; }
        public int LevelId { get; set; }
        public int IncrementId { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public decimal BasicPay { get; set; }
        public decimal CPF { get; set; }
        public string AccountNo { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string GPFCode { get; set; } = string.Empty;
        public string CPFCode { get; set; } = string.Empty;
        public string PFMSCode { get; set; } = string.Empty; 
        public int ScaleId { get; set; }
        public int HeadId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string E_HRMSId { get; set; } = string.Empty;
        public decimal CPFVol { get; set; }
        public decimal LWP { get; set; }
        public decimal NSC { get; set; }
        public decimal CoUnionInsurance { get; set; }
        public int LevelC { get; set; }
        public int Arrear { get; set; }
        public int PayFixation { get; set; }
        public int Bonus { get; set; }
        public int HeavyDuty { get; set; }
    }
}
