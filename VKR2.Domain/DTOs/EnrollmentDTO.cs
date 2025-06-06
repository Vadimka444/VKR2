namespace VKR2.Domain.DTOs
{
    public class EnrollmentDTO
    {
        public int EnrollmentId { get; set; }
        public string PupilName { get; set; }
        public string SocietyTitle { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public decimal Price { get; set; }
    }
}
