using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GI.JsonLoader.Core
{
    public class UserCommand
    {
        public UserCommand(string command, string[] parameters)
        {
            Command = command;
            Parameters = parameters;
        }

        public string Command { get; }

        public string[] Parameters { get; private set; }

        public bool IsExit => (Command != null)
                      && Command.Equals(Constants.Commands.Exit, StringComparison.OrdinalIgnoreCase);
        
        /// <summary>
        /// Gets commands from the console and returns a UserCommand
        /// </summary>
        /// <returns>A UserCommand with the user's command and its parameters</returns>
        public static UserCommand GetCommands()
        {
            Console.Write($"{Constants.Prompt} ");
            var input = Console.ReadLine();
            var tokenizedInput = Regex.Split(input, @"\s+");

            return Parse(tokenizedInput);
        }

        /// <summary>
        /// Parses a command array into a UserCommand object
        /// </summary>
        /// <param name="commands">An array of strings representing commands from the user</param>
        /// <returns>A UserCommand with a Command and its Parameters</returns>
        private static UserCommand Parse(params string[] commands)
        {
            return new UserCommand(
                commands.FirstOrDefault() ?? "", 
                commands.Skip(1).ToArray());
        }
    }
}
