namespace vtb_fitness_api.Dto
{
    public class TrackerCreateDto
    {
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public int? Sits { get; set; }
        public int? Reps { get; set; }
        public int? Meters { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
