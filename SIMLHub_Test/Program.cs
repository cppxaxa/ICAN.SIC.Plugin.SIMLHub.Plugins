using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.Plugin.SIMLHub;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIMLHub_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SIMLHub simlHub = new SIMLHub();

            Console.WriteLine("Adapter count: " + simlHub.AdapterCount);


            simlHub.Hub.Publish<IUserResponse>(new UserResponse("Connect camera 0"));
            //simlHub.Hub.Publish<IUserResponse>(new UserResponse("Open notepad"));

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
