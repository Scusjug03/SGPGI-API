namespace UPSWCAPI.Model
{
    public class FinalizeSalaryRequest
    {
        public List<int> EmpIds { get; set; }
        public int FinalMonth { get; set; }
        public int FinalYear { get; set; }
        public string SalaryType { get; set; }
        public string Status { get; set; }
    }
}
