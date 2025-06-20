using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UPSWCAPI.Model
{
    public class ModuleMaster
    {
        public int moduleId { get; set; }
        public int menuId { get; set; }
        public int projectId { get; set; } 
        public string moduleName { get; set; } = string.Empty;
        public string menuName { get; set; } = string.Empty;
        public string routerLink { get; set; } = string.Empty;
        public string moduleStatus { get; set; } = string.Empty;
        public  int sno { get; set; } 
        public int procid { get; set; } 
        public int UsertypeId { get; set; } 
        public int RoleTypeId { get; set; } 
        public int EmpId { get; set; } 
        public string message { get; set; } = string.Empty;


    }
}
