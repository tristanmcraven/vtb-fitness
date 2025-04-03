namespace vtb_fitness_api.Dto
{
    public class TariffCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public TimeSpan Period { get; set; }
        public List<string> Pros { get; set; }
    }
}
