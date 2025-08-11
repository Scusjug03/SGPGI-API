namespace UPSWCAPI.Model
{
    public class MasterDropdownDto
    {
        public int MasterId { get; set; }
        public string MasterName { get; set; }
    }

    public class MasterRequest
    {
        public int ProcId { get; set; }
    }

    public class MasterResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<MasterDropdownDto> data { get; set; }
    }
}
