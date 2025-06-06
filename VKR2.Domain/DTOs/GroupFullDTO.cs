namespace VKR2.Domain.DTOs
{
    public class GroupFullDTO
    {
        public int Groupcd { get; set; }
        public string Title { get; set; }
        public int Minage { get; set; }
        public int Maxage { get; set; }
        public int Numberofseats { get; set; }
        public int CurrentCount { get; set; }
        public int FreeSeats => Numberofseats - CurrentCount;
    }
}
