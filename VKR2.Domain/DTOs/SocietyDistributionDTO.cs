namespace VKR2.Domain.DTOs
{
    public class SocietyDistributionCreateDTO
    {
        public int Pupilcd { get; set; }
        public int Societycd { get; set; }
    }

    public class SocietyDistributionDTO : SocietyDistributionCreateDTO
    {
        public int Distributioncd { get; set; }
    }
}
