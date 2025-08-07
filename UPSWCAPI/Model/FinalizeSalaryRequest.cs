namespace UPSWCAPI.Model
{
    public class FinalizeSalaryRequest
    {
        public List<int> EmpIds { get; set; }
        public int FinalMonth { get; set; }
        public int FinalYear { get; set; }
        public string SalaryType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class IncrementDto
    {
        public int UserId { get; set; }
        public int Empid { get; set; }
        public int LevelId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int PreIncrementId { get; set; }
        public int PoIncrementId { get; set; }
        public decimal PreBasic { get; set; }
        public decimal PostBasic { get; set; }
        public int GradePay { get; set; }
        public string DepartmentHead { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
    }

    public class UserDto
    {
        public int UserId { get; set; }
    }

}
