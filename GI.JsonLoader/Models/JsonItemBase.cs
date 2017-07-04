using System;

namespace GI.JsonLoader.Models
{
    public abstract class JsonItemBase
    {
        public Guid Id { get; private set; }

        protected JsonItemBase(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Print to console the contents of this item
        /// </summary>
        public virtual void Print()
        {
            Console.WriteLine($"Id: {Id}");
        }
    }
}
