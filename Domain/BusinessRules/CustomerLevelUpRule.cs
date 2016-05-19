namespace Domain.BusinessRules
{
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerLevelUpRule : ILevelUpPaymentRule
    {
        public bool CanLevelUp(List<Payment> payments)
        {
            return payments == null ||
                   payments.Count < 5 ||
                   payments.Count(x => x.PaymentStatus == PaymentStatus.Approved) < 3 ||
                   payments.Any(x => x.PaymentStatus == PaymentStatus.Rejected) ||
                   payments.Any(x => x.PaymentStatus == PaymentStatus.Pending);
        }
    }
}