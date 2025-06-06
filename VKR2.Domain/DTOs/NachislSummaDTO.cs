namespace VKR2.Domain.DTOs
{
    public class NachislSummaCreateDTO
    {
        public int Pupilcd { get; set; }
        public int Societycd { get; set; }
        public decimal Nachislsum { get; set; }
        public short Nachislmonth { get; set; }
        public short Nachislyear { get; set; }
    }

    public class NachislSummaDTO : NachislSummaCreateDTO
    {
        public int Nachislfactcd { get; set; }
    }
}
