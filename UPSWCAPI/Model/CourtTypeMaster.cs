namespace UPSWCAPI.Model
{
    public class CourtTypeMaster
    {
        public int CourtTypeID { get; set; }
        public string CourtType { get; set; } = string.Empty;   
        public string ShortName { get; set; }  = string.Empty ;
        public string Description { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
