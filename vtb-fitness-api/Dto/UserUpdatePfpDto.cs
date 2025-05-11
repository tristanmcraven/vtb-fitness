namespace vtb_fitness_api.Dto
{
    public class UserUpdatePfpDto
    {
        public int UserId { get; set; }
        public byte[] Pfp { get; set; }
    }
}
