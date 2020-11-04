using NUnit.Framework.Internal;
using Plotly.Blazor.Examples.Components;
using Plotly.Blazor.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class CalculatePCProductionController
    {        
        public string ShowCurrentProductionCosts(string unitsToProduce, string marketingCost)
        {
            var matchInputProducedUnits =  Regex.Match(unitsToProduce, "[0-9]+");
            if (marketingCost == "") marketingCost = "0";
            var matchInputMarketing = Regex.Match(marketingCost, "[0-9]+");
            if (matchInputProducedUnits.Success && matchInputMarketing.Success)
            {
                return Convert.ToDouble(CurrentPCPrice(Double.Parse(marketingCost))).ToString("N2");
            }
            else
            {
                return "invalid input";
            }
        }


        private double CurrentPCPrice(double marketingCost)
        {            
            var chipOne = new ChipTypeOne();
            var chipTwo = new ChipTypeTwo();
            var platine = new PLT();
            return (2250000 + (20 * SetupData.CurrentWage) + marketingCost) / 3000 + (15 * chipOne.PricePerUnit) + (9 * chipTwo.PricePerUnit) + (5 * platine.PricePerUnit);
        }
    }
}
