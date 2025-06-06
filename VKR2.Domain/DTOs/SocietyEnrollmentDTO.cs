namespace VKR2.Domain.DTOs
{
    public class SocietyEnrollmentDTO
    {
        public int DistributionId { get; set; }
        public string PupilFio { get; set; }
        public string SocietyTitle { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public decimal Price { get; set; }
    }
}
