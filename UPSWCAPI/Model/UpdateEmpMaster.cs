namespace UPSWCAPI.Model
{
    public class UpdateEmpMaster
    {
        public int EmpId { get; set; }
        public int userId { get; set; }
        public int employementId { get; set; }
        public int officeId { get; set; }
        public int wTypeId { get; set; }
        public string empName { get; set; } = string.Empty;
        public string fatherName { get; set; } = string.Empty;
        public string photo { get; set; } = string.Empty;
        public string sex { get; set; } = string.Empty;
        public string empQualification { get; set; } = string.Empty;
        public int? religionId { get; set; }
        public int? bloodgroupId { get; set; }
        public int stateId { get; set; }
        public int districtId { get; set; }
        public string married { get; set; } = string.Empty;
        public string dOB { get; set; } = string.Empty;
        public int? casteId { get; set; }
        public string emailId { get; set; } = string.Empty;
        public string permAddress { get; set; } = string.Empty;
        public string mobileNo { get; set; } = string.Empty;
        public string emergencyNo { get; set; } = string.Empty;
        public string postAddress { get; set; } = string.Empty;
        public string adharNo { get; set; } = string.Empty;
        public string dptEmpCode { get; set; } = string.Empty;
        public string pfmsCode { get; set; } = string.Empty;
        public string doj { get; set; } = string.Empty;
        public string officeDOJ { get; set; } = string.Empty;
        public int categoryId { get; set; }
        public int departmentID { get; set; }
        public int? subDeptID { get; set; }
        public int? headId { get; set; }
        public int? recruitmentMode { get; set; }
        public int? serviceQuota { get; set; }
        public int designationId { get; set; }
        public int incDt { get; set; }
        public int incrementCode { get; set; }
        public string ifscCode { get; set; } = string.Empty;
        public int bankId { get; set; }
        public string accountNo { get; set; } = string.Empty;
        public string panNo { get; set; } = string.Empty;
        public string salaryStatus { get; set; } = string.Empty;
        //public int casteid { get; set; } 
        public int? payCommissionId { get; set; }
        public int? payScaleID { get; set; }
        public int gradePayId { get; set; }
        public int? levelID { get; set; }
        public int? incrementId { get; set; }
        public decimal basicSalary { get; set; }
        public string ma { get; set; } = "N";
        public string wa { get; set; } = "N";
        public string hra { get; set; } = "N";
        public string isNPSCon { get; set; } = "N";
        public int? isPPF { get; set; }
        public int? isPenCon { get; set; }
        public string npsNo { get; set; } = string.Empty;
        public string nominee { get; set; } = string.Empty;
        public int? relationId { get; set; }
        public string gisCode { get; set; } = string.Empty;
        public string orderNo { get; set; } = string.Empty;
        public string orderDt { get; set; } = string.Empty;
        public string gpfAc { get; set; } = string.Empty;
        public string cpfAc { get; set; } = string.Empty;
        public string authSignatory { get; set; } = string.Empty;
        public string dor { get; set; } = string.Empty;
        public string dod { get; set; } = string.Empty;
        public string empCode { get; set; } = string.Empty;
        public int branchId { get; set; }
        public string remarks { get; set; } = string.Empty;
        public string uanno { get; set; } = string.Empty;
        public string ppoNo { get; set; } = string.Empty;
        public int? gpfTypeId { get; set; }
        public string gpfCode { get; set; } = string.Empty;
        public string epfCode { get; set; } = string.Empty;
        public string esicCode { get; set; } = string.Empty;
        public string cca { get; set; } = "N";
        public string isEPF { get; set; } = "N";
        public string isESIC { get; set; } = "N";
        public string employeeStatus { get; set; } = string.Empty;
        public string isLock { get; set; } = string.Empty;
        public string entryDate { get; set; } = string.Empty;
        public string updateDate { get; set; } = string.Empty;
        public string ipAddress { get; set; } = string.Empty;
        public string contractvalidity { get; set; } = string.Empty;
        public int? sourceId { get; set; }
        public string orderRemarks { get; set; } = string.Empty;
        public int? preEmpId { get; set; }
        public string lastIncDate { get; set; } = string.Empty;
    }
}
