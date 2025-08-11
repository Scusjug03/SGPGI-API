using Dapper;

namespace UPSWCAPI.Model
{
    public class CaseRegistrationVM
    {
        public int RegistrationId { get; set; }
        public int courtTypeId { get; set; }
        public int CourtId { get; set; }
        public int CaseTypeId { get; set; }
        public string CaseNo { get; set; } = string.Empty;
        public string CaseRegistrationDate { get; set; } = string.Empty;
        public string FileNo { get; set; } = string.Empty;
        public int SectionId { get; set; }
        public string PreCaseNo { get; set; } = string.Empty;
        public string CaseRecDate { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Prayer { get; set; } = string.Empty;
        public string CaseFile { get; set; } = string.Empty;
        public int IsDecided { get; set; }
        public string LCCodes { get; set; } = string.Empty;
        public string NarrativeDoc { get; set; } = string.Empty;
        public string VakalatnamaDoc { get; set; } = string.Empty;
        public List<PetitionerVM> Petitioners { get; set; } = new();
        public List<RespondentVM> Respondents { get; set; } = new();
        public List<StandingCounselVM> StandingCounsels { get; set; } = new();
    }

    public class PetitionerVM
    {
        public int GovDeptId { get; set; }
        public int EmpId { get; set; }
        public string Another { get; set; } = string.Empty;
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class RespondentVM : PetitionerVM { }

    public class StandingCounselVM
    {
        public int StandingCounselId { get; set; }
        public string AssignOn { get; set; } = string.Empty;
        public string VakalatnamaDate { get; set; } = string.Empty;
        public string ReplyDate { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CaseReportRequest
    {
        public string LCNo { get; set; } = string.Empty;
        public string FileNo { get; set; } = string.Empty;
        public int IsDecided { get; set; }  // 0 = All, 1 = Decided, 2 = Pending
    }

    public class CaseDetailRequest
    {
        public int ProcId { get; set; }
        public int RegistrationId { get; set; }
        public string? LCCodes { get; set; }
    }
    public class CaseHearingRequest
    {
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int ProcId { get; set; }
        public int RegistrationId { get; set; }
    }

    public class CaseHearingFormModel
    {
        public int RegistrationId { get; set; }
        public string? PreviousHearing { get; set; }
        public string? HearingDate { get; set; }
        public string? NextHearingRemark { get; set; }
        public IFormFile? HearingFile { get; set; }
        public int UserId { get; set; }
        public int CaseStatusId { get; set; }
        public int OrdertypeId { get; set; }
    }
    public class FillCounterModel
    {
        public int RegistrationId { get; set; }
        public int CounterTypeId { get; set; }
        public string? CounterDate { get; set; }
        public string? CounterNo { get; set; }
        public string? CounterRemark { get; set; }
        public IFormFile? CounterFile { get; set; }
        public int UserId { get; set; }
    }

    public class FillApplicationModel
    {
        public int RegistrationId { get; set; }
        public int ApplicationTypeId { get; set; }
        public string? ApplicationDate { get; set; }
        public string? ApplicationNo { get; set; }
        public string? ApplicationRemark { get; set; }
        public int AppSubmittedBy { get; set; }
        public string? AppReplyRemark { get; set; }
        public IFormFile? ApplicationFile { get; set; }
        public IFormFile? AppReplyFile { get; set; }
        public int UserId { get; set; }
    }

    public class MergeCaseModel
    {
        public int RegistrationId { get; set; } // ParentId
        public string MergeTitle { get; set; } = string.Empty;
        public string MergeDate { get; set; } = string.Empty;
        public int NoofCase { get; set; }
        public int ProcId { get; set; }
        public string XML { get; set; } = string.Empty;
        public IFormFile? MergeFile { get; set; }
    }

    public class LegalDashboardRequest
    {
        public int Id { get; set; }
        public int ProcId { get; set; }
    }

    public class LegalMisReport
    {
        
            public int ProcId { get; set; }
            public string? LCNo { get; set; }
            public string? FromDate { get; set; }
            public string? ToDate { get; set; }
        

    }

    public class GetAllCase
    {
        public int RegistrationId { get; set; }
        public string LCNo { get; set; } = string.Empty;
        public string CaseType { get; set; } = string.Empty;
        public string CourtName { get; set; } = string.Empty;
        public string CaseNo { get; set; } = string.Empty;
        public string CourtType { get; set; } = string.Empty;
    }

    public class LegalStandingCounsilReport
    {
        public int ProcId { get; set; }
        public int CourtTypeId { get; set; }
        public int CourtId { get; set; }
        public int UserId { get; set; }
    }
}
