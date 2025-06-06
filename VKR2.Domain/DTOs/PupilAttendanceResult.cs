namespace VKR2.Domain.DTOs
{
    public class PupilAttendanceResult
    {
        public DateOnly Visit_Date { get; set; }
        public string Group_Title { get; set; }
        public bool Availability { get; set; }
        public string? Reason_Title { get; set; }
    }
}
