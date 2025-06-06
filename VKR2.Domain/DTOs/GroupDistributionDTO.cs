namespace VKR2.Domain.DTOs
{
    public class GroupDistributionCreateDTO
    {
        public int Pupilcd { get; set; }
        public int Groupcd { get; set; }
    }

    public class GroupDistributionDTO : GroupDistributionCreateDTO
    {
        public int Distributioncd { get; set; }
    }
}
