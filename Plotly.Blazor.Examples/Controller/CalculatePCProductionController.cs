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
        public string ShowCurrentProductionCosts(string unitsToProduce, string marketingCost, int calculateForGameRound)
        {
            var matchInputProducedUnits =  Regex.Match(unitsToProduce, "[0-9]+");
            if (marketingCost == "") marketingCost = "0";
            var matchInputMarketing = Regex.Match(marketingCost, "[0-9]+");
            if (matchInputProducedUnits.Success && matchInputMarketing.Success)
            {
                return CurrentPCPrice(Double.Parse(marketingCost), calculateForGameRound).ToString("N2");
            }
            else
            {
                return "invalid input";
            }
        }


        private double CurrentPCPrice(double marketingCost, int calculateForGameRound)
        {            
            var chipOne = new ChipTypeOne(calculateForGameRound);
            var chipTwo = new ChipTypeTwo(calculateForGameRound);
            var platine = new PLT(calculateForGameRound);
            return (2250000 + (20 * SetupData.CurrentWage) + marketingCost) / 3000 + (15 * chipOne.PricePerUnit) + (9 * chipTwo.PricePerUnit) + (5 * platine.PricePerUnit);
        }
    }
}
