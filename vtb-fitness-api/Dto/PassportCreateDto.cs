namespace vtb_fitness_api.Dto
{
    public class PassportCreateDto
    {
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateOnly IssuedDate { get; set; }
        public string UnitCode { get; set; }
        public DateOnly BirthDate { get; set; }
        public string BirthPlace { get; set; }
    }
}
