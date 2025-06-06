namespace VKR2.Domain.DTOs
{
    public class GroupScheduleResult
    {
        public DateOnly Schedule_Date { get; set; }
        public TimeOnly Start_Time { get; set; }
        public TimeOnly End_Time { get; set; }
        public string Lesson_Title { get; set; }
        public int? Cabinet_No { get; set; }
        public string Location { get; set; }
        public string Worker_Fio { get; set; }
    }
}
