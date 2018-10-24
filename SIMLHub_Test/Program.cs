﻿using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.Plugin.SIMLHub;
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
            //Console.WriteLine("Press any key to continue ...");
            //Console.Read();
            //Console.Read();
            SIMLHub simlHub = new SIMLHub();
            simlHub.Hub.Publish<IUserResponse>(new UserResponse("Open notepad"));
            simlHub.Hub.Publish<IUserResponse>(new UserResponse("Connect camera 0"));

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
