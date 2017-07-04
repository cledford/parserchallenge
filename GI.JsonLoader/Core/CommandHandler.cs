using System;
using System.Collections.Generic;
using System.Linq;
using GI.JsonLoader.Models;
using static GI.JsonLoader.Core.ProgramManager;

namespace GI.JsonLoader.Core
{
    internal class CommandHandler
    {
        private Dictionary<string, Action<string[]>> CommandMap { get; }
        
        static CommandHandler()
        {
            CommandHandlerInstance = new CommandHandler();
        }

        private CommandHandler()
        {
            CommandMap = new Dictionary<string, Action<string[]>>
            {
                { Constants.Commands.Exit, x => { } },
                { Constants.Commands.Help, HelpCommandHandler },
                { Constants.Commands.List, ListCommandHandler },
                { Constants.Commands.SearchBy, SearchByCommandHandler },
                { Constants.Commands.Select, SelectFileCommandHandler }
            };
        }

        /// <summary>
        /// Starts handling commands
        /// </summary>
        /// <returns>The user's command object</returns>
        public UserCommand ReadCommand()
        {
            try
            {
                var userCommand = UserCommand.GetCommands();

                if (!string.IsNullOrWhiteSpace(userCommand.Command))
                    CommandMap[userCommand.Command](userCommand.Parameters);

                return userCommand;
            }
            catch (NotImplementedException notImplementedError)
            {
                Console.WriteLine(notImplementedError.Message);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(Constants.InvalidCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
        
        /// <summary>
        /// Displays the available methods
        /// </summary>
        /// <param name="parameters">n/a</param>
        private void HelpCommandHandler(params string[] parameters)
        {
            DisplayHelp();
        }

        /// <summary>
        /// Lists all available commands
        /// </summary>
        /// <param name="parameters">n/a</param>
        private void ListCommandHandler(params string[] parameters)
        {
            if (!IsFileSelected())
                return;

            DisplayArrayData(ProgramManagerInstance.GetFileContents());
        }

        /// <summary>
        /// Searches the data on the given field for the given query
        /// </summary>
        /// <param name="parameters">
        ///     field - field name to search on
        ///     searchString - string to search on in the field
        /// 
        /// Passing no parameters results in a list of all data to be displayed.
        /// </param>
        private void SearchByCommandHandler(params string[] parameters)
        {
            if (!IsFileSelected())
                return;

            string field = parameters[0];
            string searchString = parameters[1];

            if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(searchString))
            {
                // XXX: invalid parameters in some way. However, due to the nature of the search, 
                // XXX: fall into the search as if they passed in no parameters.    
                DisplayArrayData(ProgramManagerInstance.GetFileContents());
                return;
            }

            var requestItems = ProgramManagerInstance.GetFileContents(field, searchString);
            DisplayArrayData(requestItems);
        }

        private bool IsFileSelected()
        {
            if (ProgramManagerInstance.SelectedFile == null)
            {
                Console.WriteLine("No file seleced. Please select a file from the list by using the 'select' command.");
                DisplayFiles();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Selects file to read from.
        /// </summary>
        /// <param name="parameters">fileName - select specified file. If no fileName is provided, a list of available files will be displayed</param>
        private void SelectFileCommandHandler(params string[] parameters)
        {
            string fileName = parameters.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                DisplayFiles();
            }
            else if (ProgramManagerInstance.TrySelectFile(fileName))
            {
                Console.WriteLine($"Switched to file: {fileName}");
            }
        }

        private void DisplayFiles()
        {
            foreach (var fileName in ProgramManagerInstance.GetFileNames())
            {
                Console.WriteLine(fileName);
            }
        }

        private static void DisplayArrayData(IEnumerable<object> items)
        {
            foreach (var fileContent in items)
            {
                ((JsonItemBase)fileContent).Print();
                Console.WriteLine();
            }
        }

        private void DisplayHelp()
        {
            foreach (var command in Constants.CommandList)
            {
                Console.WriteLine(command);
            }
        }

        public static CommandHandler CommandHandlerInstance { get; }
    }
}
