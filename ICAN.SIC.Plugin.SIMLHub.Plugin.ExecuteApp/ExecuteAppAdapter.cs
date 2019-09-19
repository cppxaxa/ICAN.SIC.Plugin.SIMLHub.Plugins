using ICAN.SIC.Abstractions;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ExecuteApp
{
    public class ExecuteAppAdapter : AbstractPlugin, IAdapter
    {
        public ExecuteAppAdapter(): base("SIMLHubPlugin.ExecuteAppAdapter")
        {

        }

        public XName TagName
        {
            get
            {
                XNamespace ns = "http://example.com/namespace#example";
                return ns + "Custom";
            }
        }
        public bool IsRecursive { get { return true; } }

        public override void Dispose()
        {
            
        }

        public string Evaluate(Context parameter)
        {
            var customElement = parameter.Element;
            var value = customElement.Value;

            new Process()
            {
                StartInfo = new ProcessStartInfo(value)
            }.Start();

            return String.Empty;
        }
    }
}
