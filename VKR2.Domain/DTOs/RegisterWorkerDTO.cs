namespace VKR2.Domain.DTOs
{
    public class RegisterWorkerDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public string phone { get; set; }
        public string passport { get; set; }
        public string address { get; set; }
        public DateOnly dateofbirth { get; set; }
    }
}
