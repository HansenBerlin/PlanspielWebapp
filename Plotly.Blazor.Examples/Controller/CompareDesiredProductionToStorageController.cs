using Plotly.Blazor.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class CompareDesiredProductionToStorageController
    {
        public double AdditionalChipsType1 { get; set; }
        public double AdditionalChipsType2 { get; set; }
        public double AdditionalPLT { get; set; }
        public double AdditionalCostChip1 { get; set; }
        public double AdditionalCostChip2 { get; set; }
        public double AdditionalCostPLT { get; set; }
        public double AdditionalWorkers { get; set; }
        public double AdditionalPCMachines { get; set; }
        public double AdditionalPLTMachines { get; set; }


        public CompareDesiredProductionToStorageController(int input, string type, int calculateForGameRound)
        {
            AdditionalCostChip1 = CompareChipsTypeOne(input, type, calculateForGameRound);
            AdditionalCostChip2 = CompareChipsTypeTwo(input, type, calculateForGameRound);
            if (type == "PC")
            {
                AdditionalCostPLT = ComparePLT(input, calculateForGameRound);
                AdditionalPCMachines = CompareMachines(input, type);
            }
            else
            {
                AdditionalPCMachines = CompareMachines(input, type);
                AdditionalCostPLT = 0;
            }
            AdditionalWorkers = CompareWorkers(input, type);
        }

        private double CompareChipsTypeOne(int input, string type, int calculateForGameRound)
        {
            var chipOne = new ChipTypeOne(calculateForGameRound);
            if (type == "PC") input *= 15;
            else if (type == "PLT") input *= 8;
            if (input > chipOne.CurrentStorage)
            {
                AdditionalChipsType1 = input - chipOne.CurrentStorage;
                double actualBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound-1, 1, "Chip1Price") *
                    (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                if (AdditionalChipsType1 >= 1500000) return (priceWithDiscount + 0.1)* AdditionalChipsType1;
                else return (actualBasePrice + 0.1)* AdditionalChipsType1;
            }
            else return 0;
        }

        private double CompareChipsTypeTwo(int input, string type, int calculateForGameRound)
        {
            var chipTwo = new ChipTypeTwo(calculateForGameRound);
            if (type == "PC") input *= 9;
            else if (type == "PLT") input *= 6;
            if (input > chipTwo.CurrentStorage)
            {
                AdditionalChipsType2 = input - chipTwo.CurrentStorage;
                double actualBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound-1, 1, "Chip2Price") *
                    (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                if (AdditionalChipsType2 >= 1000000) return (priceWithDiscount + 0.5) * AdditionalChipsType2;
                else return (actualBasePrice + 0.5) * AdditionalChipsType2;
            }
            else return 0;
        }

        private double ComparePLT(int input, int calculateForGameRound)
        {
            var plt = new PLT(calculateForGameRound);
            if (input*5 > plt.CurrentStorage)
            {
                AdditionalPLT = input*5 - plt.CurrentStorage;
                double actualBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound-1, 1, "PLTPrice") *
                    (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                if (AdditionalPLT >= 250000) return (priceWithDiscount + 10) * AdditionalPLT;
                else return (actualBasePrice + 10) * AdditionalPLT;
            }
            else return 0;
        }

        private double CompareWorkers(int input, string type)
        {
            if (type == "PC") input /= 120;
            else if (type == "PLT") input /= 600;
            if (input > SetupData.CurrentWorkers) return input - SetupData.CurrentWorkers;
            else return 0;
        }

        private double CompareMachines(int input, string type)
        {
            double pcMachines = FetchTableDataController.ReadValueFromXML
                    ("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "PCMachines");
            double pltMachines = FetchTableDataController.ReadValueFromXML
                    ("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "PLTMachines");

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
