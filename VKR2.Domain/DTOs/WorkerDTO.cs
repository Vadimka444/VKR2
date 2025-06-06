namespace VKR2.Domain.DTOs
{
    public class WorkerCreateDTO
    {
        public string Fio { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateOnly Dateofbirth { get; set; }
    }

    public class WorkerDTO : WorkerCreateDTO
    {
        public int Workercd { get; set; }
    }
}
