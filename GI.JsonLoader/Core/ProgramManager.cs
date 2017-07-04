using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GI.JsonLoader.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using File = System.IO.File;
using GIFile = GI.JsonLoader.Models.File;
using static GI.JsonLoader.Core.Constants.AllowedFileNames;

namespace GI.JsonLoader.Core
{
    public class ProgramManager
    {
        // Let's keep this guy in the scope of this class.
        private const string FileDataPath = @".\Data\";
        
        static ProgramManager()
        {
            ProgramManagerInstance = new ProgramManager();
        }

        internal ProgramManager() { }

        public GIFile SelectedFile { get; private set; }

        public bool TrySelectFile(string fileName)
        {
            string filePath = $"{FileDataPath}{fileName}";
            if (!File.Exists(filePath))
                return false;

            try
            {
                var fileContents = JArray.Parse(File.ReadAllText(filePath));
                var fileType = GetType(fileName);
                var items = fileContents.Select(f => JsonConvert.DeserializeObject(f.ToString(), fileType));

                SelectedFile = new GIFile(fileName, fileType, items);
                return true;
            }
            catch (JsonReaderException)
            {
                Console.WriteLine(
                    $"Invalid json for file path: {filePath} \r\n Please double check your data file to ensure it is valid");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception has occurred attempting to load file: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<object> GetFileContents(string fieldToSearch, string searchString)
        {
            // XXX: Instead of doing heavy reflection magic, we'll get more performance at the cost of maintainability here. 
            switch (SelectedFile.Type.Name)
            {
                case nameof(Rpg):
                    return SearchForRpgContents(fieldToSearch, searchString);
                case nameof(Followers):
                    return SearchForFollowersContents(fieldToSearch, searchString);

                default: // XXX: By design, this shouldn't happen, but we're exposing this method publicly 
                         // XXX: and we need to do something so the compiler doesn't whine about not all code paths returning a value
                    throw new TypeLoadException($"The selected file's type of {SelectedFile.Type.Name} is invalid.");
            }    
        } 

        public IEnumerable<object> GetFileContents()
        {
            return SelectedFile.Contents;
        } 

        public IEnumerable<string> GetFileNames()
        {
            return Directory.GetFiles(FileDataPath);
        }

        private IEnumerable<object> SearchForRpgContents(string fieldToSearch, string searchString)
        {
            switch (fieldToSearch.ToUpper())
            {
                case "FIRSTNAME":
                    return SelectedFile.Contents.Where(a => ((Rpg)a).FirstName.ToUpper().Contains(searchString.ToUpper()));
                case "LASTNAME":
                    return SelectedFile.Contents.Where(a => ((Rpg)a).LastName.ToUpper().Contains(searchString.ToUpper()));
                case "CHARACTERCLASS":
                    return SelectedFile.Contents.Where(a => ((Rpg)a).CharacterClass.ToString().ToUpper().Contains(searchString.ToUpper()));
                case "CHARACTERRACE":
                    return SelectedFile.Contents.Where(a => ((Rpg)a).CharacterRace.ToString().ToUpper().Contains(searchString.ToUpper()));
                default:
                    // XXX: Error here? The user may want to know why they're getting all results back instead of specific ones.
                    return SelectedFile.Contents;
            }
        }

        private IEnumerable<object> SearchForFollowersContents(string fieldToSearch, string searchString)
        {
            switch (fieldToSearch.ToUpper())
            {
                case "FIRSTNAME":
                    return SelectedFile.Contents.Where(a => ((Followers)a).FirstName.ToUpper().Contains(searchString.ToUpper()));
                case "LASTNAME":
                    return SelectedFile.Contents.Where(a => ((Followers)a).LastName.ToUpper().Contains(searchString.ToUpper()));
                case "EMAIL":
                    return SelectedFile.Contents.Where(a => ((Followers)a).Email.ToUpper().Contains(searchString.ToUpper()));
                case "FOLLOWERS":
                    return SelectedFile.Contents.Where(a => ((Followers)a).NumberOfFollowers.ToString().ToUpper().Contains(searchString.ToUpper()));
                default:
                    // XXX: Error here? The user may want to know why they're getting all results back instead of specific ones.
                    return SelectedFile.Contents;
            }
        }

        private Type GetType(string fileName)
        {
            switch (fileName.ToUpper())
            {
                case RpgTypeFile:
                    return typeof (Rpg);
                case FollowersTypeFile:
                    return typeof (Followers);
                default:
                    throw new TypeLoadException("File does not match any defined type.");
            }
        }

        public static ProgramManager ProgramManagerInstance { get; internal set; }
    }
}
