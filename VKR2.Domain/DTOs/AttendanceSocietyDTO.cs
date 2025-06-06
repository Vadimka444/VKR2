namespace VKR2.Domain.DTOs
{
    public class AttendanceSocietyCreateDTO
    {
        public int Pupilcd { get; set; }
        public int Societycd { get; set; }
        public int? Reasoncd { get; set; }
        public DateOnly Visitdate { get; set; }
        public bool Availability { get; set; }
    }

    public class AttendanceSocietyDTO : AttendanceSocietyCreateDTO
    {
        public int Visitingcd { get; set; }
    }
}
