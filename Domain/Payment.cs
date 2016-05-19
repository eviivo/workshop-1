using System;

namespace Domain
{
    /// <summary>
    /// Hi, I'm Payment. I belong to User. Thanks for opening me.
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
