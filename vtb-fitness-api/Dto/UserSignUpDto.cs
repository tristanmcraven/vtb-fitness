﻿namespace vtb_fitness_api.Dto
{
    public class UserSignUpDto
    {
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[]? Pfp { get; set; }
        public int RoleId { get; set; }
        public DateOnly? WorkingInVtbSince { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }

        public PassportCreateDto PassportCreateDto { get; set; }
        public BankingDetailsCreateDto? BankingDetailsDto { get; set; }
    }
}
