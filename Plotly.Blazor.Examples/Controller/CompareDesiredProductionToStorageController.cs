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
        public double AdditionalOrAvailableWorkers { get; set; }
        public double AdditionalOrAvailablePCMachines { get; set; }
        public double AdditionalOrAvailablePLTMachines { get; set; }


        public CompareDesiredProductionToStorageController(int input, string type)
        {
            AdditionalCostChip1 = CompareChipsTypeOne(input, type);
            AdditionalCostChip2 = CompareChipsTypeTwo(input, type);
            if (type == "PC") AdditionalCostPLT = ComparePLT(input); else AdditionalCostPLT = 0;
            CompareWorkers(input, type);
            CompareMachines(input, type);
        }

        private double CompareChipsTypeOne(int input, string type)
        {
            var chipOne = new ChipTypeOne();
            if (type == "PC") input *= 15;
            else if (type == "PLT") input *= 8;
            if (input > chipOne.CurrentStorage)
            {
                AdditionalChipsType1 = input - chipOne.CurrentStorage;
                double actualBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "Chip1Price") *
                    (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                if (AdditionalChipsType1 >= 1500000) return (priceWithDiscount + 0.1)* AdditionalChipsType1;
                else return (actualBasePrice + 0.1)* AdditionalChipsType1;
            }
            else return 0;
        }

        private double CompareChipsTypeTwo(int input, string type)
        {
            var chipTwo = new ChipTypeTwo();
            if (type == "PC") input *= 9;
            else if (type == "PLT") input *= 6;
            if (input > chipTwo.CurrentStorage)
            {
                AdditionalChipsType2 = input - chipTwo.CurrentStorage;
                double actualBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "Chip2Price") *
                    (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                if (AdditionalChipsType2 >= 1000000) return (priceWithDiscount + 0.5) * AdditionalChipsType2;
                else return (actualBasePrice + 0.5) * AdditionalChipsType2;
            }
            else return 0;
        }

        private double ComparePLT(int input)
        {
            var plt = new PLT();
            if (input*5 > plt.CurrentStorage)
            {
                AdditionalPLT = input*5 - plt.CurrentStorage;
                double actualBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "PLTPrice") *
                    (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
                double priceWithDiscount = actualBasePrice - (actualBasePrice / 10);
                if (AdditionalPLT >= 250000) return (priceWithDiscount + 10) * AdditionalPLT;
                else return (actualBasePrice + 10) * AdditionalPLT;
            }
            else return 0;
        }

        private void CompareWorkers(int input, string type)
        {
            if (type == "PC") input /= 120;
            else if (type == "PLT") input /= 600;
            AdditionalOrAvailableWorkers = input - SetupData.CurrentWorkers;            
        }

        private void CompareMachines(int input, string type)
        {
            if (type == "PC")
            {
                input /= 3000;
                AdditionalOrAvailablePCMachines = input - FetchTableDataController.ReadValueFromXML
                    ("companyProductionData.xml", SetupData.CurrentGameRound, 1, "PCMachines");
            }
            else if (type == "PLT")
            {
                input /= 7500;
                AdditionalOrAvailablePLTMachines = input - FetchTableDataController.ReadValueFromXML
                    ("companyProductionData.xml", SetupData.CurrentGameRound, 1, "PLTMachines");
            }
        }
    }
}
