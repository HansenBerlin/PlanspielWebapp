using Plotly.Blazor.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class CompareDesiredProductionToStorageController
    {
        public static double AdditionalChipsType1 { get; set; }
        public static double AdditionalChipsType2 { get; set; }
        public static double AdditionalPLT { get; set; }
        public static double AdditionalCostChip1 { get; set; }
        public static double AdditionalCostChip2 { get; set; }
        public static double AdditionalCostPLT { get; set; }
        public static double AdditionalWorkers { get; set; }
        public static double AdditionalCostWorkers { get; set; }
        public static double AdditionalPCMachines { get; set; }
        public static double AdditionalPLTMachines { get; set; }


        public CompareDesiredProductionToStorageController(double inputPCs, double inputPLTs, string type, int calculateForGameRound)
        {
            ResetData();
            CalculateAdditionalUnitsAndCost(inputPCs, inputPLTs, type, calculateForGameRound);
        }

        public void ResetData()
        {
            AdditionalChipsType1 = 0;
            AdditionalChipsType2 = 0;
            AdditionalPLT = 0;
            AdditionalCostChip1 = 0;
            AdditionalCostChip2 = 0;
            AdditionalCostPLT = 0;
            AdditionalWorkers = 0;
            AdditionalCostWorkers = 0;
            AdditionalPCMachines = 0;
            AdditionalPLTMachines = 0;
        }

        public void CalculateAdditionalUnitsAndCost(double inputPCs, double inputPLTs, string type, int calculateForGameRound)
        {
            if (type == "PC")
            {
                AdditionalCostChip1 = CompareChipsTypeOne(inputPCs, type, calculateForGameRound);
                AdditionalCostChip2 = CompareChipsTypeTwo(inputPCs, type, calculateForGameRound);
                AdditionalCostPLT = ComparePLT(inputPCs, calculateForGameRound);
                AdditionalPCMachines = CompareMachines(inputPCs, type);
                AdditionalWorkers = CompareWorkers(inputPCs, type);
            }
            else if (type == "PLT")
            {
                AdditionalCostChip1 = CompareChipsTypeOne(inputPLTs, type, calculateForGameRound);
                AdditionalCostChip2 = CompareChipsTypeTwo(inputPLTs, type, calculateForGameRound);
                AdditionalPLTMachines = CompareMachines(inputPLTs, type);
                AdditionalWorkers = CompareWorkers(inputPLTs, type);
                AdditionalCostPLT = 0;
            }
            else if (type == "both")
            {
                AdditionalCostChip1 = CompareChipsTypeOne(inputPCs, "PC", calculateForGameRound);
                AdditionalCostChip2 = CompareChipsTypeTwo(inputPCs, "PC", calculateForGameRound);
                AdditionalCostChip1 = CompareChipsTypeOne(inputPLTs, "PLT", calculateForGameRound);
                AdditionalCostChip2 = CompareChipsTypeTwo(inputPLTs, "PLT", calculateForGameRound);
                AdditionalCostPLT = ComparePLT(inputPCs, calculateForGameRound);
                AdditionalPCMachines = CompareMachines(inputPCs, "PC");
                AdditionalPLTMachines = CompareMachines(inputPLTs, "PLT");
                AdditionalCostWorkers = CompareWorkers(inputPCs, "PC");
                AdditionalCostWorkers = CompareWorkers(inputPLTs, "PLT");
            }
        }

        private double CompareChipsTypeOne(double input, string type, int calculateForGameRound)
        {
            if (type == "PC") input *= 15;
            else if (type == "PLT") input *= 8;
            if (input > TemporaryData.TemporaryStorageChipOneLeftUnits)
            {
                AdditionalChipsType1 = input - TemporaryData.TemporaryStorageChipOneLeftUnits;
                double actualBasePrice = SetupData.PPPChip1 * (100 / SetupData.Quality);
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                TemporaryData.TemporaryStorageChipOneLeftUnits -= input;
                if (AdditionalChipsType1 >= 1500000) return priceWithDiscount * AdditionalChipsType1;
                else return actualBasePrice * AdditionalChipsType1;
            }
            else
            {
                TemporaryData.TemporaryStorageChipOneLeftUnits -= input;
                return 0;
            }
        }

        private double CompareChipsTypeTwo(double input, string type, int calculateForGameRound)
        {
            if (type == "PC") input *= 9;
            else if (type == "PLT") input *= 6;
            if (input > TemporaryData.TemporaryStorageChipTwoLeftUnits)
            {
                AdditionalChipsType2 = input - TemporaryData.TemporaryStorageChipTwoLeftUnits;
                double actualBasePrice = SetupData.PPPChip2 * (100 / SetupData.Quality);
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                TemporaryData.TemporaryStorageChipTwoLeftUnits -= input;
                if (AdditionalChipsType2 >= 1000000) return priceWithDiscount * AdditionalChipsType2;
                else return actualBasePrice * AdditionalChipsType2;
            }
            else
            {
                TemporaryData.TemporaryStorageChipTwoLeftUnits -= input;
                return 0;
            }
        }

        private double ComparePLT(double input, int calculateForGameRound)
        {
            if (input * 5 > TemporaryData.TemporaryStoragePLTLeftUnits)
            {
                AdditionalPLT = input * 5 - TemporaryData.TemporaryStoragePLTLeftUnits;
                double actualBasePrice = SetupData.PPPPLTBuy * (100 / SetupData.Quality);
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                TemporaryData.TemporaryStoragePLTLeftUnits -= input * 5;
                if (AdditionalPLT >= 250000) return priceWithDiscount * AdditionalPLT;
                else return actualBasePrice * AdditionalPLT;
            }
            else
            {
                TemporaryData.TemporaryStoragePLTLeftUnits -= input * 5;
                return 0;
            }
        }

        private double CompareWorkers(double input, string type)
        {
            if (type == "PC") input /= 120;
            else if (type == "PLT") input /= 600;
            if (input > TemporaryData.TemporaryStorageWorkersLeft)
            {
                AdditionalWorkers = input - TemporaryData.TemporaryStorageWorkersLeft;
                double wage = SetupData.CurrentWage;
                TemporaryData.TemporaryStorageWorkersLeft -= input;
                return wage * AdditionalWorkers;
            }
            else
            {
                TemporaryData.TemporaryStorageWorkersLeft -= input;
                return 0;
            }
        }

        private double CompareMachines(double input, string type)
        {
            double pcMachines = SetupData.PCMachinesAvailableThisRound;
            double pltMachines = SetupData.PLTMachinesAvailableThisRound;

            if (type == "PC" && input / 3000 > pcMachines)
            {
                return (input / 3000) - pcMachines;
            }
            else if (type == "PLT" && input / 7500 > pltMachines)
            {
                return (input / 7500) - pltMachines;
            }
            else return 0;
        }
    }
}
