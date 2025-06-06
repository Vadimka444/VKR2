namespace VKR2.Domain.DTOs
{
    public class ParentCreateDTO
    {
        public string Fio { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public class ParentDTO : ParentCreateDTO
    {
        public int Parentcd { get; set; }
    }
}
