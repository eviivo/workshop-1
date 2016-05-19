using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Dao
{
    /// <summary>
    /// Hi, I'm PaymentRepository. If you have user Id, I can give you some data!
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        public List<Payment> GetUserPayments(int userId)
        {
            return _payments.Where(x => x.UserId == userId).ToList();
        }

        #region Data (assume it comes from database)

        private readonly List<Payment> _payments = new List<Payment>
        {
            new Payment {Id =  0, UserId = 2, Date = DateTime.Today.AddDays(-9), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id =  1, UserId = 2, Date = DateTime.Today.AddDays(-8), PaymentStatus = PaymentStatus.Rejected},
            new Payment {Id =  3, UserId = 2, Date = DateTime.Today.AddDays(-7), PaymentStatus = PaymentStatus.Rejected},

            new Payment {Id =  4, UserId = 3, Date = DateTime.Today.AddDays(-2), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id =  5, UserId = 3, Date = DateTime.Today.AddDays(-2), PaymentStatus = PaymentStatus.Approved},

            new Payment {Id =  6, UserId = 4, Date = DateTime.Today.AddDays(-9), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id =  7, UserId = 4, Date = DateTime.Today.AddDays(-8), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id =  8, UserId = 4, Date = DateTime.Today.AddDays(-7), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id =  9, UserId = 4, Date = DateTime.Today.AddDays(-2), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 10, UserId = 4, Date = DateTime.Today,             PaymentStatus = PaymentStatus.Pending},

            new Payment {Id = 11, UserId = 5, Date = DateTime.Today.AddDays(-5), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 12, UserId = 5, Date = DateTime.Today.AddDays(-4), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 13, UserId = 5, Date = DateTime.Today.AddDays(-4), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 14, UserId = 5, Date = DateTime.Today.AddDays(-3), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 15, UserId = 5, Date = DateTime.Today.AddDays(-3), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 16, UserId = 5, Date = DateTime.Today.AddDays(-2), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 17, UserId = 5, Date = DateTime.Today.AddDays(-2), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 18, UserId = 5, Date = DateTime.Today.AddDays(-1), PaymentStatus = PaymentStatus.Approved},
            new Payment {Id = 19, UserId = 5, Date = DateTime.Today,             PaymentStatus = PaymentStatus.Pending}
        };

        #endregion
    }
}
