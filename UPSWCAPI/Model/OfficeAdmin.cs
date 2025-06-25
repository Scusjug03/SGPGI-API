namespace UPSWCAPI.Model
{
    public class OfficeAdmin
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public int RoleTypeId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserMobileno { get; set; } = string.Empty;
        public int CircleId { get; set; }
        public int UsertypeId { get; set; }
        public int OfficeId { get; set; }
        public int DivisionId { get; set; }
        public int RegionId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string RefreshTokenExpiryTime { get; set; } = string.Empty;

    }

}
