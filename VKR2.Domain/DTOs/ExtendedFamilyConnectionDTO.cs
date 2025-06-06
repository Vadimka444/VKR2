namespace VKR2.Domain.DTOs
{
    public class ExtendedFamilyConnectionDTO
    {
        public int Familyconnectioncd { get; set; }
        public int Pupilcd { get; set; }
        public string PupilFio { get; set; }
        public int Parentcd { get; set; }
        public string ParentFio { get; set; }
        public string KinshipTitle { get; set; }
        public string ParentPhone { get; set; }
        public string GroupTitle { get; set; }
    }
}
