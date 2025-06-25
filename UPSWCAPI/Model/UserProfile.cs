namespace UPSWCAPI.Model
{
    namespace UPSWCAPI.Model
    {
        public class UserProfile
        {
            public int empId { get; set; }
            public string empName { get; set; } = string.Empty;
            public string fatherName { get; set; } = string.Empty;
            public int categoryId { get; set; }
            public string gender { get; set; } = string.Empty;
            public string dOB { get; set; } = string.Empty;
            public string mobileNo { get; set; } = string.Empty;
            public string isBoardEmployee { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
            public int departmentId { get; set; }
            public int designationId { get; set; }
            public string address { get; set; } = string.Empty;
            public int stateId { get; set; }
            public int cityId { get; set; }
            public string pinCode { get; set; } = string.Empty;
            public string photo { get; set; } = string.Empty;
            public int procId { get; set; }
            public string msg { get; set; }
            public string JSONApproval { get; set; } = string.Empty;
        }
    }

}
