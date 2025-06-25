namespace UPSWCAPI.Model
{
    public class ModuleMasterModel
    {
        public int ModuleId { get; set; } = 0;
        public int ProjectId { get; set; } = 0;
        public string ModuleName { get; set; } = string.Empty;
        public string ModuleStatus { get; set; } = string.Empty;
        public int SNo { get; set; } = 0;
        public int UserTypeId { get; set; } = 0;
        public int RoleTypeId { get; set; } = 0;
        public int ProcId { get; set; } = 0;
    }
}
