namespace UPSWCAPI.Model
{
    public class ItemDetailsModel
    {
        public int? Itemid { get; set; }

        public string? Rackno { get; set; }

        public string? ItemName { get; set; }

        public string? ManufctName { get; set; }

        public int? ModelId { get; set; }

        public int? SetID { get; set; }

        public decimal? SetQty { get; set; }

        public int? UnitId { get; set; }

        public string? Manufatureno { get; set; }

        public int? Itemgrpid { get; set; }

        public int? ItemTypeId { get; set; }

        public int? ItemCategoryId { get; set; }

        public int? UserId { get; set; }

        public int? ProcID { get; set; }  // Optional for internal use only
    }

    public class ItemGroupModel
    {
        public int Itemgrpid { get; set; }
        public string Itemgrpname { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Procid { get; set; }
    }

    public class MakeModel
    {
        public int Makeid { get; set; }
        public string Makename { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class SupplierCategoryModel
    {
        public int SuppCategId { get; set; }
        public string SuppCategNm { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; } // For both CreatedBy and UpdatedBy
    }

    public class SupplierDetailsModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string TinNo { get; set; }
        public string STDCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public int SuppCategId { get; set; }
        public string EmailId { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }
        public string Website { get; set; }
        public int UserId { get; set; }
    }

    public class UnitModel
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
    }
}
