using System.Collections.Generic;
using Domain.BusinessRules;

namespace Domain
{
    /// <summary>
    /// Hi, I'm user! I have so much data, but I don't know what to do with it :(
    /// People say I'm anemic and that I should care more about Encapsulation, Inheritance, Polymorphism, Abstraction.
    /// </summary>
    public class User
    {
        private readonly ILevelUpPaymentRule levelUpRule;

        public User(int id, string name, UserType userType, UserStatus userStatus, List<Payment> payments, ILevelUpPaymentRule levelUpRule)
        {
            Id = id;
            Name = name;
            UserType = userType;
            UserStatus = userStatus;
            Payments = payments;

            this.levelUpRule = levelUpRule;
        }

        public int Id { get; }

        public string Name { get; }

        private UserType UserType { get; }

        public UserStatus UserStatus { get; }

        public List<Payment> Payments { get; }

        public bool LevelUp(List<Payment> payments)
        {
            return levelUpRule.CanLevelUp(payments);
        }
    }
}
