using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleHealthKitExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            AppleXMLData xmldata = new AppleXMLData("c:\\data\\export.xml");
            xmldata.DistinationDirectory = "c:\\data\\csv";
            xmldata.ExportIntoCSVFiles();
        }
    }
}
