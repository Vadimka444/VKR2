namespace VKR2.Domain.DTOs
{
    public class FamilyConnectionCreateDTO
    {
        public int Parentcd { get; set; }
        public int Pupilcd { get; set; }
        public string KinshipDegree { get; set; }   
    }

    public class FamilyConnectionDTO : FamilyConnectionCreateDTO
    {
        public int Familyconnectioncd { get; set; }
    }
}
