using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanspielWebApp.Models
{
    public class MachinePC : IMachine
    {
        public string MachineType { get; set; }
        public int Capacity { get; set; }
        public int InitialPrice { get; set; }
        public int CostPerRound { get; set; }
        public int StaffToRun { get; set; }
        public int EOLInRound { get; set; }

        public void ProduceOnePC()
        {

        }

    }

    
}
