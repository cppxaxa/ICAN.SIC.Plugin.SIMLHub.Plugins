using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ICANSEE
{
    public class ICANSEEAdapter : IAdapter
    {
        public bool IsRecursive { get { return true; } }

        public XName TagName
        {
            get
            {
                XNamespace ns = "http://ican.sic/namespace#icansee";
                return ns + "Parameter";
            }
        }

        public string Evaluate(Context simlContext)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo("calc.exe")
            }.Start();

            return string.Empty;
        }
    }
}
