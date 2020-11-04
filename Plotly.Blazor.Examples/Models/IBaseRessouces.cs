using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    interface IBaseRessouces
    {
        public double PricePerUnit { get; set; }
        public double CurrentStorage { get; set; }
        public double StorageCosts { get; set; }


        public double CalculateCurrentCost();

    }
}
