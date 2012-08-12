using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;

namespace Epilogger.Web.Core.Stats
{
    public class EpiloggerServices
    {


        public List<ServiceController> GetEpiloggerServiceStatus()
        {
            var services = ServiceController.GetServices();

            return services.Where(e => e.DisplayName.Contains("Epilogger") || e.DisplayName.Contains("SQL Server (MS")).ToList();

        }
        


    }
}