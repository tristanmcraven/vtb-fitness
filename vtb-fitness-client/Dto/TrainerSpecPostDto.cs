using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vtb_fitness_client.Dto
{
    public class TrainerSpecPostDto
    {
        public int TrainerId { get; set; }
        public List<int> SpecIds { get; set; }
    }
}
