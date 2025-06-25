using System.ComponentModel.DataAnnotations;

namespace MectoiApis.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        public char Sex { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string  DOB { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AadhaarNo { get; set; }
        [Required]
        public int  DeptId { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public string Photopath { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string  Pincode { get; set; }
        [Required]
        public int CasteId { get; set; }

        public string EmpCode { get; set; }

    }
    public class Permission
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public int ModuleId { get; set; }
        public int MenuId { get; set; }
        public int ProcId { get; set; }
    }
   
}
