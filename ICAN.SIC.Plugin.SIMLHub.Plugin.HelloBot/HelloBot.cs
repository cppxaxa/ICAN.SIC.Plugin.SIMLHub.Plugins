using ICAN.SIC.Abstractions;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.HelloBot
{
    public class CustomAdapter : AbstractPlugin, IAdapter
    {
        public CustomAdapter(): base("SIMLHubPlugin.CustomAdapter")
        {

        }

        public XName TagName { get { return "Custom"; } }
        public bool IsRecursive { get { return true; } }

        public override void Dispose()
        {
            
        }

        public string Evaluate(Context parameter)
        {
            var customElement = parameter.Element;
            var value = customElement.Value;
            //Do some processing

            Console.WriteLine("CUSTOM PROCESSING");

            return "ABC" + value;
        }
    }

    //public class HelloBot : ISIMLHubPlugin.ISIMLHubPlugin
    //{
    //    public List<Type> GetAllAdapterTypes()
    //    {
    //        List<Type> adapterTypes = new List<Type>();

    //        Type t = this.GetType();
    //        foreach(var item in t.Assembly.GetTypes())
    //            Console.WriteLine(item);

    //        return adapterTypes;
    //    }
    //}
}
