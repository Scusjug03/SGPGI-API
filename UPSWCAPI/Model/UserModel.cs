namespace MectoiApis.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int UsertypeId { get; set; }
        public int OfficeId { get; set; }
        public int CircleId { get; set; }
        public int ProcId { get; set; }
        public int EmpId { get; set; }
        public string IsFirstLogin { get; set; } = string.Empty;
        public string DashboardPage { get; set; } = string.Empty;
        //public string EmailAddress { get; set; }
        //public bool isAuth { get; set; }
        //public DateTime DateOfJoing { get; set; }
        public string message { get; set; } =string.Empty;

        public int RoleTypeId { get; set; }

        public string UserRole { get; set; } = string.Empty;

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public string? RefreshTokenExpiryTime { get; set; }
    }

    public class DepartmentLogin
    {
        public int UserId { get; set; }
        public int UserType { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;
        public int ProcId { get; set; }
    }
}
