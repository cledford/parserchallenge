namespace GI.JsonLoader.Core
{
    public static class Constants
    {
        public static readonly string[] CommandList = {
            $"{Commands.Exit } - exits the application",
            $"{Commands.Help} - displays these commands",
            $"{Commands.List} - lists the data",
            $"{Commands.SearchBy} <field> <query> - searches the field for the given query",
            $"{Commands.Select} <filename> - Selects the file you are viewing"
        };

        public static readonly string InvalidCommand = $"\nAn invalid command was entered.\n{TypeHelp}\n";
        public const string Prompt = ">";
        public const string TypeHelp = "Type 'help' to see a list of available commands.";
        public const string Title = "Generic Inc Json Parser";
        public static readonly string Welcome = $"Welcome to the Generic Inc Json Parsing application.\n{TypeHelp}\n";

        public static class Commands
        {
            public const string Exit = "exit";
            public const string Help = "help";
            public const string List = "list";
            public const string SearchBy = "searchby";
            public const string Select = "select";
        }

        internal static class AllowedFileNames
        {
            public const string RpgTypeFile = "RPG.JSON";
            public const string FollowersTypeFile = "FOLLOWERS.JSON";
        }
    }
}
