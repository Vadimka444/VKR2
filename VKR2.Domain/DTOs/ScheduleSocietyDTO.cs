namespace VKR2.Domain.DTOs
{
    public class ScheduleSocietyCreateDTO
    {
        public int? Cabinetcd { get; set; }
        public int Societycd { get; set; }
        public int? Workercd { get; set; }
        public DateOnly Scheduledate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }

    public class ScheduleSocietyDTO : ScheduleSocietyCreateDTO
    {
        public int Schedulecd { get; set; }
    }
}
