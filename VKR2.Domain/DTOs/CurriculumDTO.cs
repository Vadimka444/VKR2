namespace VKR2.Domain.DTOs
{
    public class CurriculumCreateDTO
    {
        public string Title { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public short Quantity { get; set; }
    }

    public class CurriculumDTO : CurriculumCreateDTO
    {
        public int Lessoncd { get; set; }
    }
}
