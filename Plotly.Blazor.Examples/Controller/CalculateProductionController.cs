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
    public class CalculateProductionController
    {
        public string ShowCurrentProductionCostsPLT(string unitsToProduce, int calculateForGameRound)
        {
            var matchInputProducedUnits = Regex.Match(unitsToProduce, "[0-9]+");

            if (matchInputProducedUnits.Success)
            {
                return Convert.ToDouble((533333 + (10 * SetupData.CurrentWage)) / 7500 
                    + PricePerPieceChip1(Convert.ToInt32(unitsToProduce), calculateForGameRound, 8)
                    + PricePerPieceChip2(Convert.ToInt32(unitsToProduce), calculateForGameRound, 6)).ToString("N2");
            }
            else return "ungültige Eingabe";
        }

        public string ShowCurrentProductionCostsPC(string unitsToProduce, string marketingCost, int calculateForGameRound)
        {
            var matchInputProducedUnits = Regex.Match(unitsToProduce, "[0-9]+");
            if (marketingCost == "") marketingCost = "0";
            var matchInputMarketing = Regex.Match(marketingCost, "[0-9]+");
            if (matchInputProducedUnits.Success && matchInputMarketing.Success)
            {
                return Convert.ToDouble(((2250000 + 20 * SetupData.CurrentWage + Convert.ToDouble(marketingCost)) / 3000)
                    + PricePerPieceChip1(Convert.ToInt32(unitsToProduce), calculateForGameRound, 15)
                    + PricePerPieceChip2(Convert.ToInt32(unitsToProduce), calculateForGameRound, 9)
                    + PricePerPiecePLT(Convert.ToInt32(unitsToProduce), calculateForGameRound)).ToString("N2");
            }
            else
            {
                return "ungültige Eingabe";
            }
        }       

        public double PricePerPieceChip1(int unitsToProduce, int calculateForGameRound, int multiplier)
        {
            var chipOne = new ChipTypeOne(calculateForGameRound);
            double returnValue;

            double additionalChipsTypeOne = unitsToProduce * multiplier - TemporaryData.TemporaryStorageChipOneLeftUnits;

            if(additionalChipsTypeOne > 0 && additionalChipsTypeOne < 1500000)
            {
                returnValue =  ((TemporaryData.TemporaryStorageChipOneLeftUnits * chipOne.PricePerUnit) 
                    + (additionalChipsTypeOne * (SetupData.PPPChip1*(100/SetupData.Quality)))) / unitsToProduce;
                TemporaryData.TemporaryStorageChipOneLeftUnits = 0;
            }
            else if (additionalChipsTypeOne > 1500000)
            {
                returnValue = ((TemporaryData.TemporaryStorageChipOneLeftUnits * chipOne.PricePerUnit) 
                    + (additionalChipsTypeOne * (SetupData.PPPChip1*(100/SetupData.Quality)*0.9))) / unitsToProduce;
                TemporaryData.TemporaryStorageChipOneLeftUnits = 0;
            }
            else
            {
                TemporaryData.TemporaryStorageChipOneLeftUnits -= unitsToProduce * multiplier; 
                returnValue = chipOne.PricePerUnit * multiplier;
            }
            return returnValue;
        }

        public double PricePerPieceChip2(int unitsToProduce, int calculateForGameRound, int multiplier)
        {
            var chipTwo = new ChipTypeTwo(calculateForGameRound);
            double returnValue;

            double additionalChipsTypeTwo = unitsToProduce * multiplier - TemporaryData.TemporaryStorageChipTwoLeftUnits;

            if (additionalChipsTypeTwo > 0 && additionalChipsTypeTwo < 1000000)
            {
                returnValue = ((TemporaryData.TemporaryStorageChipTwoLeftUnits * chipTwo.PricePerUnit)
                    + (additionalChipsTypeTwo * (SetupData.PPPChip2 * (100 / SetupData.Quality)))) / unitsToProduce;
                TemporaryData.TemporaryStorageChipTwoLeftUnits = 0;
            }
            else if (additionalChipsTypeTwo > 1000000)
            {
                returnValue = ((TemporaryData.TemporaryStorageChipTwoLeftUnits * chipTwo.PricePerUnit)
                    + (additionalChipsTypeTwo * (SetupData.PPPChip2 * (100 / SetupData.Quality) * 0.9))) / unitsToProduce;
                TemporaryData.TemporaryStorageChipTwoLeftUnits = 0;
            }
            else
            {
                TemporaryData.TemporaryStorageChipTwoLeftUnits -= unitsToProduce * multiplier;
                returnValue = chipTwo.PricePerUnit * multiplier;
            }
            return returnValue;
        }

        public double PricePerPiecePLT(int unitsToProduce, int calculateForGameRound)
        {
            var plt = new PLT(calculateForGameRound);
            double returnValue;

            double additionalPLT = unitsToProduce * 5 - TemporaryData.TemporaryStoragePLTLeftUnits;

            if (additionalPLT > 0 && additionalPLT < 250000)
            {
                returnValue = ((TemporaryData.TemporaryStoragePLTLeftUnits * plt.PricePerUnit)
                    + (additionalPLT * (SetupData.PPPPLTBuy * (100 / SetupData.Quality)))) / unitsToProduce;
                TemporaryData.TemporaryStoragePLTLeftUnits = 0;
            }
            else if (additionalPLT > 250000)
            {
                returnValue = ((TemporaryData.TemporaryStoragePLTLeftUnits * plt.PricePerUnit)
                    + (additionalPLT * (SetupData.PPPPLTBuy * (100 / SetupData.Quality) * 0.9))) / unitsToProduce;
                TemporaryData.TemporaryStoragePLTLeftUnits = 0;
            }
            else
            {
                TemporaryData.TemporaryStoragePLTLeftUnits -= unitsToProduce * 5;
                returnValue = plt.PricePerUnit * 5;
            }
            return returnValue;
        }

        public double LastPLTPrice(int calculateForGameRound)
        {
            var chipOne = new ChipTypeOne(calculateForGameRound);
            var chipTwo = new ChipTypeTwo(calculateForGameRound);
            return (533333 + (10 * SetupData.CurrentWage)) / 7500 + (8 * chipOne.PricePerUnit) + (6 * chipTwo.PricePerUnit);
        }
    }
}
