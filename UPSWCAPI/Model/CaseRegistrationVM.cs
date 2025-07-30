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
        public List<PetitionerVM> Petitioners { get; set; } = new();
        public List<RespondentVM> Respondents { get; set; } = new();
        public List<StandingCounselVM> StandingCounsels { get; set; } = new();
    }

    public class PetitionerVM
    {
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
    }

    public class CaseHearingFormModel
    {
        public int RegistrationId { get; set; }
        public string? PreviousHearing { get; set; }
        public string? HearingDate { get; set; }
        public string? NextHearingRemark { get; set; }
        public IFormFile? HearingFile { get; set; }
        public int UserId { get; set; }
    }
}
