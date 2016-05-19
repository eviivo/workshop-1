using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// Hi, I'm user! I have so much data, but I don't know what to do with it :(
    /// People say I'm anemic and that I should care more about Encapsulation, Inheritance, Polymorphism, Abstraction.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserType UserType { get; set; }

        public UserStatus UserStatus { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
