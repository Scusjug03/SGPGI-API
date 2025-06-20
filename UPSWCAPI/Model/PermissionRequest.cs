namespace UPSWCAPI.Model
{
    public class PermissionItem
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public int ModuleId { get; set; }
        public int MenuId { get; set; }
    }

    public class PermissionRequest
    {
        public int Id { get; set; }           
        public int ProjectId { get; set; }
        public int ModuleId { get; set; }
        public int MenuId { get; set; }
        public int ProcId { get; set; }
        public List<PermissionItem> Items { get; set; }
    }



}
