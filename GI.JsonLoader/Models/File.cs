using System;
using System.Collections.Generic;

namespace GI.JsonLoader.Models
{
    public class File
    {
        public File(string fileName, Type type, IEnumerable<object> contents)
        {
            FileName = fileName;
            Type = type;
            Contents = contents;
        }

        /// <summary>
        /// Name of file
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        ///  The type of data this file holds a collection of
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Contents of file.
        /// </summary>
        public IEnumerable<object> Contents { get; private set; }
    }
}
