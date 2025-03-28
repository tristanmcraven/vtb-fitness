namespace vtb_fitness_api.Dto
{
    public class BankingDetailsCreateDto
    {
        public string CorrespondentAccount { get; set; }
        public string Bik {  get; set; }
        public string BankName { get; set; }
        public string DebitCardNumber { get; set; }
    }
}
