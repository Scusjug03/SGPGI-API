namespace UPSWCAPI.Model
{
    public class MenuMasterModel
    {
        public int ModuleId { get; set; }
        public int ProjectId { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string MenuName { get; set; } = string.Empty;
        public string RouterLink { get; set; } = string.Empty;
        public int MenuId { get; set; }
        public string ModuleStatus { get; set; } = string.Empty;
        public int Sno { get; set; }
        public int UserTypeId { get; set; }
        public int RoleTypeId { get; set; }
        public int EmpId { get; set; }
        public int ProcId { get; set; }

      
    }
}
