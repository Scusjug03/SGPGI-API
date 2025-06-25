namespace UPSWCAPI.Model
{
    public class RolePermission
    {
        public int WTypeId { get; set; }
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string CpfNo { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string UserPwd { get; set; } = string.Empty;
        public string DecPassword { get; set; } = string.Empty;
        public string HrmsId { get; set; } = string.Empty;
        public int RoleTypeId { get; set; }
        public int DesignationId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string RefreshTokenExpiryTime { get; set; } = string.Empty;
        public int Circle { get; set; }
        public int UsertypeId { get; set; }
    }
}
