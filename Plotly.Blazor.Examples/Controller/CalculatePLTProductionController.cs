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
    public class CalculatePLTProductionController
    {
        public string ShowCurrentProductionCosts(string unitsToProduce, int calculateForGameRound)
        {
            var matchInputProducedUnits = Regex.Match(unitsToProduce, "[0-9]+");

            if (matchInputProducedUnits.Success)
            {
                return Convert.ToDouble(CurrentPLTPrice(calculateForGameRound)).ToString("N2");
            }
            else
            {
                return "invalid input";
            }
        }


        public double CurrentPLTPrice(int calculateForGameRound)
        {
            var chipOne = new ChipTypeOne(calculateForGameRound);
            var chipTwo = new ChipTypeTwo(calculateForGameRound);
            return (533333 + (10 * SetupData.CurrentWage)) / 7500 + (8 * chipOne.PricePerUnit) + (6 * chipTwo.PricePerUnit);
        }
    }
}
