namespace Domain.BusinessRules
{
    using System.Collections.Generic;

    public interface ILevelUpPaymentRule
    {
        bool CanLevelUp(List<Payment> payments);
    }
}