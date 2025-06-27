namespace UPSWCAPI.Model
{
    public class ItemDetailsModel
    {
        public int? Itemid { get; set; }

        public string? Rackno { get; set; }

        public string? ItemName { get; set; }

        public int? ManufactureID { get; set; }

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

}
