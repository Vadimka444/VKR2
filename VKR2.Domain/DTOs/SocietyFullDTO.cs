namespace VKR2.Domain.DTOs
{
    public class SocietyFullDTO
    {
        public int Societycd { get; set; }
        public string Title { get; set; }
        public int Minage { get; set; }
        public int Maxage { get; set; }
        public decimal Price { get; set; }
        public int Numberofseats { get; set; }
        public int CurrentCount { get; set; }
        public int FreeSeats => Numberofseats - CurrentCount;
    }
}
