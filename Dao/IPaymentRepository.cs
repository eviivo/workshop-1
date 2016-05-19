using System.Collections.Generic;
using Domain;

namespace Dao
{
    /// <summary>
    /// Hi, I'm PaymentsRepository interface. Say hello to my best friend UserRepository interface.
    /// </summary>
    public interface IPaymentRepository
    {
        List<Payment> GetUserPayments(int userId);
    }
}
