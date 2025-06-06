namespace VKR2.Domain.DTOs
{
    public class CabinetCreateDTO
    {
        public int? Cabinetno { get; set; }
        public string Location { get; set; } = null!;
    }

    public class CabinetDTO : CabinetCreateDTO
    {
        public int Cabinetcd { get; set; }
    }
}
