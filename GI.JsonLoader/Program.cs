using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GI.JsonLoader.Core;
using static GI.JsonLoader.Core.CommandHandler;

namespace GI.JsonLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Sets console title and application message
            Console.Title = Constants.Title;
            Console.WriteLine(Constants.Welcome);
            
            //  Starts app
            UserCommand userCommand;
            do
            {
                userCommand = CommandHandlerInstance.ReadCommand();
            }
            while (!userCommand.IsExit);
        }
    }
}
