namespace UPSWCAPI.Model
{
    public class CourtMaster
    {
        public int CourtTypeID { get; set; }
        public string CourtType { get; set; } = string.Empty;   
        public string ShortName { get; set; }  = string.Empty ;
        public string Description { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CourtTypeMaster
    {
        public int CourtId { get; set; }
        public int CourtTypeId { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class CaseTypeModel
    {
        public int CaseTypeId { get; set; }
        public int CourtTypeID { get; set; }
        public string CaseType { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;  
    }
    public class SubjectMaster
    {
        public int SubjectMatterID { get; set; }
        public string SubjectCode { get; set; } = string.Empty;
        public string SubjectMatters { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class SectionMaster
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int ProcId { get; set; }
    }

    public class DocumentMaster
    {
        public int DocumentId { get; set; }
        public string DocumentCode { get; set; } = string.Empty;
        public string DocumentDetails { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class EvidenceMaster
    {
        public int EvidenceId { get; set; }
        public string EvidenceCode { get; set; } = string.Empty;
        public string EvidenceDetails { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class GovDepartment
    {
        public int GovDeptID { get; set; }
        public string GovDepart { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class IntrimMaster
    {
        public int InterimOrderId { get; set; }
        public string InterimOrder { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        
    }
    public class JudgementTypeModel
    {
        public int JudgementTypeID { get; set; }
        public string JudgementType { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CounselDetail
    {
        public int CounselId { get; set; }
        public int CourtTypeId { get; set; }
        public int CourtId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool IsSenior { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

}
