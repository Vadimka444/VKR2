namespace VKR2.Domain.DTOs
{
    public class PupilWithGenderDTO
    {
        public int Pupilcd { get; set; }
        public string Fio { get; set; }
        public DateOnly Dateofbirth { get; set; }
        public string Birthcertificatenumber { get; set; }
        public string Gender { get; set; }
        public string GroupTitle { get; set; }
    }
}
