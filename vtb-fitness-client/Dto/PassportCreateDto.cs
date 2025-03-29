using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vtb_fitness_client.Dto
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
