using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace UPSWCAPI.Model
{
    public class CommodityModel
    {
        public int ProcId { get; set; }
        public int? CommodityId { get; set; }
        public string? CommodityName { get; set; }
    }

    public class DesignationModel
    {
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public short? CategoryId { get; set; }
        public string? Status { get; set; }      
        public int? OrderBy { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }        
        public string? DesigUsage { get; set; }  
        public int? CompId { get; set; }
        public int? EmployeId { get; set; }
        public int? EmployementMapId { get; set; }
        public int? ClassMapId { get; set; }
    }

    public class DepartmentModel
    {
        public int? DepartmentId { get; set; }
        public string? DepartmentHead { get; set; }
       // public string? Status { get; set; }
        public int? ProcId { get; set; }
    }

    public class RecruitmentModentModel
    {
        public int? RecruitId { get; set; }
        public string? RecruitMode { get; set; }
        public int? ProcId { get; set; }
    }

    public class PaymentHeadModel
    {
        public int? HeadId { get; set; }
        public int? ProcId { get; set; }
        public string? HeadCode { get; set; }
        public string? HeadName { get; set; }
        public string? Status { get; set; }
        public int? OfficeId { get; set; }

    }

    public class ServiceQuotaModel
    {
        public int ProcId { get; set; }
        public int? QuotaId { get; set; }
        public string? QuotaName { get; set; }
    }

    public class MSPModel
    {
        public int ProcId { get; set; }
        public int? JeansRateid { get; set; }
        public int? CommodityId { get; set; }
        public decimal? PerKuntelRate { get; set; }
        public decimal? PerKgRate { get; set; }
        public DateTime? EffectiveDate { get; set; }
    }
}
