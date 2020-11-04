using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class MergedDataController
    {
        public static List<object> GetDataSetFromCompanyTable(bool[] gameRounds, bool[] companys, string key)
        {
            var returnList = new List<object>();
          
            for (int i = 0; i < gameRounds.Length; i++)
            {
                for (int j = 0; j < companys.Length; j++)
                {
                    if (gameRounds[i] == true && companys[j] == true)
                    {
                        returnList.Add(FetchTableDataController.ReadValueFromXML("marketData.xml", i + 1, j + 1, key));
                    }
                }
            }

            return returnList;
        }
    }
}
