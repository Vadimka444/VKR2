namespace VKR2.Domain.DTOs
{
    public class PupilCreateDTO
    {
        public string Fio { get; set; } = null!;
        public string Birthcertificatenumber { get; set; } = null!;
        public string Gender { get; set; }
        public DateOnly Dateofbirth { get; set; }
    }

    public class PupilDTO : PupilCreateDTO
    {
        public int Pupilcd { get; set; }
    }
}
