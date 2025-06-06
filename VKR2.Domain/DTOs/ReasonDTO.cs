namespace VKR2.Domain.DTOs
{
    public class ReasonCreateDTO
    {
        public string Title { get; set; } = null!;
    }

    public class ReasonDTO : ReasonCreateDTO
    {
        public int Reasoncd { get; set; }
    }
}
