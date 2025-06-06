namespace VKR2.Domain.DTOs
{
    public class AttendanceCreateDTO
    {
        public int? Reasoncd { get; set; }

        public int Pupilcd { get; set; }

        public DateOnly Visitdate { get; set; }

        public bool Availability { get; set; }
    }

    public class AttendanceDTO : AttendanceCreateDTO
    {
        public int Visitingcd { get; set; }
    }
}
