using System;
using Newtonsoft.Json;

namespace GI.JsonLoader.Models
{
    public class Rpg : JsonItemBase
    {
        [JsonConstructor]
        public Rpg(Guid id, string firstName, string lastName, CharacterRace characterRace, CharacterClass characterClass) 
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            CharacterRace = characterRace;
            CharacterClass = characterClass;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public CharacterRace CharacterRace { get; }

        public CharacterClass CharacterClass { get; }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"First Name: {FirstName}");
            Console.WriteLine($"Last Name: {LastName}");
            Console.WriteLine($"Character Class: {CharacterClass}");
            Console.WriteLine($"Character Race: {CharacterRace}");
        }
    }
}
