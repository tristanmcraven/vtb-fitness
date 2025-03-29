using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vtb_fitness_client.Dto
{
    public class BankingDetailsCreateDto
    {
        public string CorrespondentAccount { get; set; }
        public string Bik { get; set; }
        public string BankName { get; set; }
        public string DebitCardNumber { get; set; }
    }
}
