namespace UPSWCAPI.Model
{
    public class SalaryGenerateRequest
    {
        public int UserId { get; set; }
        public string IPAddress { get; set; }
        public int WtypeId { get; set; } = 1;
        public int DepartmentId { get; set; }
        public int ESubDeptId { get; set; }
        public string AllEmp { get; set; } = "Y"; // or "N"
        public string? EmpId { get; set; }
        public int PayYear { get; set; }
        public int PayMonth { get; set; }
        public int OfficeId { get; set; }
        public int TotalDays { get; set; }
        public int CircleId { get; set; }
    }

    public class SalaryRptRequest
    {
    
        public int ProcId { get; set; }
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public int EmpId { get; set; }
        public int PayYear { get; set; }
        public int PayMonth { get; set; }
        public int DepartmentId { get; set; }
        public int SubDeptId { get; set; }
        public int DesignationId { get; set; }
        public string Salarytype { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }

}
