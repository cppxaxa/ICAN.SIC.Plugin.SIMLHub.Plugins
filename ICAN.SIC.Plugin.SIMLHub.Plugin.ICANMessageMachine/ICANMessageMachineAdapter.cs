using ICAN.SIC.Abstractions;
using ICAN.SIC.Abstractions.ConcreteClasses;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ICANMessageMachine
{
    public class ICANMessageMachineAdapter : AbstractPlugin, IAdapter
    {
        public ICANMessageMachineAdapter() : base("SIMLHubPlugin.ICANMessageMachineV1")
        {

        }

        public bool IsRecursive => false;

        public XName TagName 
		{
            get
            {
                XNamespace ns = "http://ican.sic/namespace#icanmessagemachinev1";
                return ns + "Parameter";
            }
        }

        public override void Dispose()
        {
            
        }

        public string Evaluate(Context simlContext)
        {
            string input = simlContext.Element.Value;

            MachineMessage request = new MachineMessage(input);
            hub.Publish<MachineMessage>(request);

            return string.Empty;
        }
    }
}
