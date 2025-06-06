namespace VKR2.Domain.DTOs
{
    public class SocietyCreateDTO
    {
        public string Title { get; set; } = null!;
        public int Maxage { get; set; }
        public int Minage { get; set; }
        public int Numberofseats { get; set; }
        public decimal Price { get; set; }
    }

    public class SocietyDTO : SocietyCreateDTO
    {
        public int Societycd { get; set; }
    }
}
