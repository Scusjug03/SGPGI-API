using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPSWCAPI.Model
{
    public class EnquiryDbContext:DbContext
    {
     public EnquiryDbContext(DbContextOptions<EnquiryDbContext>options) : base(options) { 
      
        
        }
        public DbSet<EnquiryStatus> EnquiryStatus { get; set; }
        public DbSet<EnquiryType> EnquiryType { get; set; }
        public DbSet<EnquiryModel> EnquiryModel { get; set; }
        public DbSet<Employee> Employes { get; set; }
        public DbSet<EmployeeFamily> EmployeeFamilies { get; set; }


        [Table("EmployeeMaster")]

        public class Employee
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int EmployeeId { get; set; }
            [Required]
            public string name { get; set; } = string.Empty;
            public string contactNo { get; set; } = string.Empty;
            public string emailid { get; set; } = string.Empty;
            public string projectName { get; set; } = string.Empty;
            public string city { get; set; } = string.Empty;
            public string address { get; set; } = string.Empty;
             
        }

        public class EmployeeViewModel
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int EmployeeId { get; set; }
            [Required]
            public string name { get; set; } = string.Empty;
            public string contactNo { get; set; } = string.Empty;
            public string emailid { get; set; } = string.Empty;
            public string projectName { get; set; } = string.Empty;
            public string city { get; set; } = string.Empty;
            public string address { get; set; } = string.Empty;
            public List<EmployeeFamily> employeeFamilies { get; set; }


        }

        [Table("EmployeeFamily")]

        public class EmployeeFamily
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int familyid { get; set; }
            public int employeeid { get; set; }
            public string name { get; set; }
            public string relation { get; set; }
            public int age { get; set; }

        }

    }


    }
