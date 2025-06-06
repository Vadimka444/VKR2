namespace VKR2.Domain.DTOs
{
    public class PupilsPaymentsReportResult
    {
        public int? pupil_cd { get; set; }
        public string fio { get; set; }
        public decimal dance { get; set; }
        public decimal english { get; set; }
        public decimal drawing { get; set; }
        public decimal music { get; set; }
        public decimal total { get; set; }
    }
}
