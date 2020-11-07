﻿using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class SetupData
    {
        public static int CurrentGameRound { get; set; }
        public static double CurrentWage { get; set; }
        public static double CurrentWorkers { get; set; }
        public static double PCDemandLastRound { get; set; }
        public static double Efficiency { get; set; }
        public static double PCMachinesAvailableThisRound { get; set; }
        public static double PCMachinesToReplaceThisRound { get; set; } 
        public static double PLTMachinesAvailableThisRound { get; set; }
        public static double PLTMachinesToReplaceThisRound { get; set; }
        public static double PPPPC { get; set; } 
        public static double PPPPLTProductionLastRound { get; set; } 
        public static double PPPPLTBuy { get; set; }
        public static double PPPChip1 { get; set; } 
        public static double PPPChip2 { get; set; }
        public static double PCsProducedLastRound { get; set; } 
        public static double PLTProducedLastRound { get; set; } 
        public static double Quality { get; set; } 
        public static double Chip1Storage { get; set; } 
        public static double Chip2Storage { get; set; } 
        public static double PLTStorage { get; set; } 
        public static double PCStorage { get; set; } 


        public SetupData()
        {
            // Always keep GameRound at 1 and change in XML Table
            CurrentGameRound = Convert.ToInt32(FetchTableDataController.ReadValueFromXML("generalData.xml", 1, 1, "CurrentGameRound"));
            Efficiency = FetchTableDataController.ReadValueFromXML("generalData.xml", CurrentGameRound - 1, 1, "Efficiency");
            PCDemandLastRound = FetchTableDataController.ReadValueFromXML("generalData.xml", CurrentGameRound - 1, 1, "PCDemandThisRound");
            CurrentWorkers = FetchTableDataController.ReadValueFromXML("generalData.xml", CurrentGameRound - 1, 1, "CurrentWorkers");
            CurrentWage = FetchTableDataController.ReadValueFromXML("generalData.xml", CurrentGameRound - 1, 1, "CurrentWage");

            PPPChip1 = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "Chip1Price");
            PPPChip2 = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "Chip2Price");
            PPPPLTBuy = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "PLTPrice");
            Quality = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "Quality");

            Chip1Storage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound - 1, 1, "Chip1Storage");
            Chip2Storage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound - 1, 1, "Chip2Storage");
            PLTStorage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound - 1, 1, "PLTStorage");
            PCStorage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound - 1, 1, "PCStorage");

            PCMachinesAvailableThisRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "PCMachinesAvailable");
            PCMachinesToReplaceThisRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "PCMachinesBreakingAfterThisRound");
            PLTMachinesAvailableThisRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "PLTMachinesAvailable");
            PLTMachinesToReplaceThisRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", CurrentGameRound, 1, "PLTMachinesBreakingAfterThisRound");

            PCsProducedLastRound = FetchTableDataController.ReadValueFromXML("marketData.xml", CurrentGameRound - 1, 1, "OutputPC");
            PLTProducedLastRound = FetchTableDataController.ReadValueFromXML("marketData.xml", CurrentGameRound - 1, 1, "OutputPLT");

            TemporaryData.TemporaryStorageChipOneLeftUnits = Chip1Storage;
            TemporaryData.TemporaryStorageChipTwoLeftUnits = Chip2Storage;
            TemporaryData.TemporaryStoragePLTLeftUnits = PLTStorage;

            var pcProductionCalculate = new CalculateProductionController();
            var pltProductionCalculate = new CalculateProductionController();
            var resetTempData = new TemporaryData();

            PPPPC = Convert.ToDouble(pcProductionCalculate.ShowCurrentProductionCostsPC(PCsProducedLastRound.ToString(),
                FetchTableDataController.ReadValueFromXML("marketData.xml", CurrentGameRound - 1, 1, "Marketing").ToString(),
                CurrentGameRound));

            resetTempData.ResetData();

            PPPPLTProductionLastRound = Convert.ToDouble(pltProductionCalculate.ShowCurrentProductionCostsPLT(PLTProducedLastRound.ToString(), CurrentGameRound-1));

            resetTempData.ResetData();            
        }    
    }
}