namespace UPSWCAPI.Model
{
    public class region
    {
        public int ProcId { get; set; }
        public int RegionId { get; set; }
        public string? RegionName { get; set; }
        public int? DivisionId { get; set; } 
    }
    public class Office
    {
        internal readonly string? shortName;

        public int DivisionId { get; set; }
        public int OfficeId { get; set; }
        public int RegionId { get; set; }
        public string OfficeName { get; set; } = string.Empty;
        //public string Status { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string OfficeCode { get; set; } = string.Empty;
    }

}
