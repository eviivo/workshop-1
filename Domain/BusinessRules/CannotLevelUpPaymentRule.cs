namespace Domain.BusinessRules
{
    using System.Collections.Generic;

    public class CannotLevelUpPaymentRule : ILevelUpPaymentRule
    {
        public bool CanLevelUp(List<Payment> payments)
        {
            return false;
        } 
    }
}