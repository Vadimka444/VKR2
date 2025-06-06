namespace VKR2.Domain.DTOs
{
    public class WorkerPostCreateDTO
    {
        public int Workercd { get; set; }
        public int Postcd { get; set; }
    }

    public class WorkerPostDTO : PostCreateDTO
    {
        public int WorkerPostcd { get; set; }
        public int Workercd { get; set; }
        public int Postcd { get; set; }
    }
}
