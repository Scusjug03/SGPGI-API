namespace UPSWCAPI.Model
{
    public class ProjectMaster
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int Procid { get; set; }
    }

    public class IApiRersponse
    {
        public string message { get; set; }
        public Boolean result { get; set; }
        public List<ProjectMaster>? data { get; set; }
        
    }
}
