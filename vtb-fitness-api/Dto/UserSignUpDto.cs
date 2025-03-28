namespace vtb_fitness_api.Dto
{
    public class UserSignUpDto
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Pfp { get; set; }
        public int RoleId { get; set; }
        public DateOnly? WorkingInVtbSince { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }

    }
}
