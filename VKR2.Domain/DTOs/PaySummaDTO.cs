namespace VKR2.Domain.DTOs
{
    public class PaySummaCreateDTO
    {
        public int Pupilcd { get; set; }
        public int Societycd { get; set; }
        public decimal Paysum { get; set; }
        public DateOnly Paydate { get; set; }
        public short Paymonth { get; set; }
        public short Payyear { get; set; }
    }

    public class PaySummaDTO : PaySummaCreateDTO
    {
        public int Payfactcd { get; set; }
    }
}
