namespace RulesDemo.Core.Data
{
    public class EpiCase
    {
        public string CdIcd9 { get; set; }
        public string CdStatus { get; set; }
        public string CdDxStatus { get; set; }
        public string IdAdded { get; set; }
        public short AmAge { get; set; }
        public DateTime? DtEvent { get; set; }
    }
}
