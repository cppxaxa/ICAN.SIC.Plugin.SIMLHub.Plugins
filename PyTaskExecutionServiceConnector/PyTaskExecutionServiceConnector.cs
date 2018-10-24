using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.PyTaskExecutionServiceConnector
{
    public class PyTaskExecutionServiceConnectorAdapter : IAdapter
    {
        public bool IsRecursive { get { return true; } }

        public XName TagName
        {
            get
            {
                XNamespace ns = "http://ican.sic/namespace#ptesc";
                return ns + "Parameter";
            }
        }

        public string Evaluate(Context simlContext)
        {
            Console.WriteLine("Hello console");

            new Process
            {
                StartInfo = new ProcessStartInfo("calc.exe")
            }.Start();

            return "Result Hello";
        }
    }
}
