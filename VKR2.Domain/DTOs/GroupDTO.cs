namespace VKR2.Domain.DTOs
{
    public class GroupCreateDTO
    {
        public string Title { get; set; } = null!;
        public int Maxage { get; set; }
        public int Minage { get; set; }
        public int Numberofseats { get; set; }
    }

    public class GroupDTO : GroupCreateDTO
    {
        public int Groupcd { get; set; }
    }
}
