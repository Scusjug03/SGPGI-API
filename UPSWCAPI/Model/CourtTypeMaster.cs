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
}
