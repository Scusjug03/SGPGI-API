namespace UPSWCAPI.Model
{
    public class SupplierCategoryModel
    {
        public int SuppCategId { get; set; }
        public string SuppCategNm { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; } // For both CreatedBy and UpdatedBy
    }
}
