using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UPSWCAPI.Model
{
    public class RoleType
    {
        public int ProcId { get; set; }
        public int RoleTypeId { get; set; }
        public string? RoleTypeName { get; set; }
        public string? ShortName { get; set; }
        public string? DashboardPage { get; set; }
        public string? msg { get; set; }
    }
    public class LoanTypeMaster
    {
        public int LoanTypeId { get; set; }
        public string? LoanType { get; set; }
        public string? LoanDesc { get; set; }
        public string? msg { get; set; }

    }
    public class ComissionMaster
    {
        public int ComissionId { get; set; }
        public string? ComName { get; set; }
        public string? msg { get; set; }
    }
    public class ScaleMaster
    {
        public int SCALECODE { get; set; }
        public decimal L_LIMIT { get; set; }
        public decimal INC1 { get; set; }
        public decimal L_LIMIT2 { get; set; }
        public decimal INC2 { get; set; }
        public decimal L_LIMIT3 { get; set; }
        public decimal INC3 { get; set; }
        public decimal U_LIMIT { get; set; }
        public string? PAYSCALE { get; set; }
        public string? msg { get; set; }
    }
    public class GradePayMaster
    {
        public int GradePayId { get; set; }
        public decimal GradePay { get; set; }
        public string? msg { get; set; }
    }
    public class PayCommisionMaster
    {
        public int PayCommisionId { get; set; }
        public string? PayCommision { get; set; }
        public int GradePay { get; set; }
        public string? PayCommLevel { get; set; }
        public int Increment { get; set; }
        public int Basic { get; set; }
        public int Levelid { get; set; }
        public string? msg { get; set; }
    }
    public class CircleMaster
    {
        public int CircleId { get; set; }
        public string? CircleName { get; set; }
        public string? msg { get; set; }
    }

    public class AgencyMaster
    {
        public int AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public string? msg { get; set; }
    }

    public class EmploymentModel
    {
        public int employementid { get; set; }
        public string? employement { get; set; }
        public int wtypeid { get; set; }
        public string? componentcode { get; set; }
        public string? workingtype { get; set; }
    }
    public class Subdepartment
    {
        public int subdepartmentId { get; set; }
        public string? subdepartment { get; set; }
        public int departmentId { get; set; }
        public string? componentcode { get; set; }
        public string? departmentname { get; set; }
    }

    public class FinancialYearMaster
    {
        public int financialyearcode { get; set; }
        public string? financialYear { get; set; }
    }

    public class M_ApplicationTypeMaster
    {
        public int Appid { get; set; }
        public string? AppType { get; set; }
        public string? msg { get; set; }
    }
    public class M_CounterTypeMaster
    {
        public int Appid { get; set; }
        public string? AppType { get; set; }
        public string? msg { get; set; }
    }

}
