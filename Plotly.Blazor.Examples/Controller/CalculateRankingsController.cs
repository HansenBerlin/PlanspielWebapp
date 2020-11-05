using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class CalculateRankingsController
    {
        public double RankingIncome { get; set; }
        public double RankingSaldo { get; set; }
        public double ProductionCapacity { get; set; }
        public double Marketshare { get; set; }
        

        public static double CheckPosition(string type)
        {
            double currentPosition = 1;
            if (type == "production")
            {
                type = "OutputPC";
                var listPCs = MergedDataController.GetDataSetFromCompanyTable(new bool[] { true, true, true, true, true, true }, new bool[] { true, true, true, true, true, true }, type);
                type = "OutputPLT";
                var listPLT = MergedDataController.GetDataSetFromCompanyTable(new bool[] { true, true, true, true, true, true }, new bool[] { true, true, true, true, true, true }, type);

                for (int i = 1; i < 5; i++)
                {
                    if ((Convert.ToDouble(listPCs[i]) + Convert.ToDouble(listPLT[i])) > (Convert.ToDouble(listPCs[0]) + Convert.ToDouble(listPLT[0])))
                    {
                        currentPosition++;
                    }
                }
            }
            else
            {
                var listAllValues = MergedDataController.GetDataSetFromCompanyTable(new bool[] { true, true, true, true, true, true }, new bool[] { true, true, true, true, true, true }, type);
                for (int i = 1; i < 5; i++)
                {
                    if (Convert.ToDouble(listAllValues[i]) > Convert.ToDouble(listAllValues[0]))
                    {
                        currentPosition++;
                    }
                }
            }
            
            return currentPosition;
        }
    }
}
