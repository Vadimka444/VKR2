namespace VKR2.Domain.DTOs
{
    public class PostCreateDTO
    {
        public string Title { get; set; } = null!;
    }

    public class PostDTO : PostCreateDTO
    {
        public int Postcd { get; set; }
    }
}
