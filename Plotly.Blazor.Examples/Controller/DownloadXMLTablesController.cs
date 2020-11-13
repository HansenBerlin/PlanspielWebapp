using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Plotly.Blazor.Examples.Controller
{
    public class DownloadXMLTablesController
    {        
        public DownloadXMLTablesController()
        {
            try
            {
                File.Delete(Path.Combine(@"Tables\\", "companyProductionData.xml"));
                File.Delete(Path.Combine(@"Tables\\", "marketData.xml"));
                File.Delete(Path.Combine(@"Tables\\", "generalData.xml"));
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://bonoweb.de/companyProductionData.xml", @"Tables\\companyProductionData.xml");
                webClient.DownloadFile("https://bonoweb.de/marketData.xml", @"Tables\\marketData.xml");
                webClient.DownloadFile("https://bonoweb.de/generalData.xml", @"Tables\\generalData.xml");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
