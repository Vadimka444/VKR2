namespace VKR2.Domain.DTOs
{
    public class ScheduleCreateDTO
    {
        public int? Cabinetcd { get; set; }
        public int Groupcd { get; set; }
        public int Lessoncd { get; set; }
        public int? Workercd { get; set; }
        public DateOnly Scheduledate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }

    public class ScheduleDTO : ScheduleCreateDTO
    {
        public int Schedulecd { get; set; }
    }
}
