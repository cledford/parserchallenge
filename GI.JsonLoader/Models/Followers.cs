using System;
using Newtonsoft.Json;

namespace GI.JsonLoader.Models
{
    public class Followers : JsonItemBase
    {
        [JsonConstructor]
        public Followers(Guid id, string firstName, string lastName, string email, int followers) 
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NumberOfFollowers = followers;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int NumberOfFollowers { get; }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"First Name: {FirstName}");
            Console.WriteLine($"Last Name: {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Followers: {NumberOfFollowers}");
        }
    }
}
