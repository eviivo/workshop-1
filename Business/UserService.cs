using System;
using System.Linq;
using Dao;
using Domain;
using Infrastructure;

namespace Services
{
    /// <summary>
    /// Hi, I'm UserService. I'm very-very important. So many responsibilities!
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICache _cache;

        public UserService()
            : this(new UserRepository(), new PaymentRepository(), new Cache())
        {
        }

        public UserService(
            IUserRepository userRepository,
            IPaymentRepository paymentRepository,
            ICache cache)
        {
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _cache = cache;
        }

        /// <summary>
        /// Activates the user.
        /// </summary>
        /// <returns><c>true</c>, if user was activated, <c>false</c> otherwise.</returns>
        /// <param name="id">User Id.</param>
        public bool Activate(int id)
        {
            bool result;
            var user = _userRepository.FindById(id);

            if (user == null)
            {
                result = false; // not found
            }
            else if (user.UserType == UserType.Admin || user.UserType == UserType.Support)
            {
                result = false; // cannot (de-)activate admin/support
            }
            else if (user.UserType == UserType.Customer ||
                     user.UserType == UserType.SilverCustomer ||
                     user.UserType == UserType.GoldCustomer)
            {
                user.UserStatus = UserStatus.Active;
                _userRepository.Modify(user);
                result = true;
            }
            else
            {
                throw new ArgumentException("Undefined user type");
            }

            if (result)
            {
                _cache.Update(user);
            }

            return result;
        }

        /// <summary>
        /// Deactivates the user.
        /// </summary>
        /// <returns><c>true</c>, if user was deactivated, <c>false</c> otherwise.</returns>
        /// <param name="id">User Id.</param>
        public bool Deactivate(int id)
        {
            bool result;
            var user = _userRepository.FindById(id);

            if (user == null)
            {
                result = false; // not found
            }
            else if (user.UserType == UserType.Admin || user.UserType == UserType.Support)
            {
                result = false; // cannot (de-)activate admin/support
            }
            else if (user.UserType == UserType.Customer)
            {
                var payments = _paymentRepository.GetUserPayments(user.Id);

                if (payments == null)
                {
                    result = true; // no payments - deactivate
                }
                else if (payments.Any(x => x.PaymentStatus == PaymentStatus.Pending))
                {
                    result = false; // cannot deactivate customer with pending payments
                }
                else
                {
                    user.UserStatus = UserStatus.Disabled;
                    _userRepository.Modify(user);
                    result = true;
                }
            }
            else if (user.UserType == UserType.SilverCustomer)
            {
                var payments = _paymentRepository.GetUserPayments(user.Id);

                if (payments == null)
                {
                    result = true; // no payments - deactivate
                }
                else if (payments.Any(x => x.PaymentStatus == PaymentStatus.Pending) ||
                    payments.Any(x => x.Date == DateTime.Today))
                {
                    // cannot deactivate silver customer with pending payments OR
                    // who's made a payment today.
                    result = false;
                }
                else
                {
                    user.UserStatus = UserStatus.Disabled;
                    _userRepository.Modify(user);
                    result = true;
                }
            }
            else if (user.UserType == UserType.GoldCustomer)
            {
                // cannot be done online, only relationship manager can deactivate gold customer
                result = false;
            }
            else
            {
                throw new ArgumentException("Undefined user type");
            }

            if (result)
            {
                _cache.Update(user);
            }

            return result;
        }

        /// <summary>
        /// Promotes (levels up) the customer if possible.
        /// </summary>
        /// <returns><c>true</c>, if user was promoted, <c>false</c> otherwise.</returns>
        /// <param name="id">User Id.</param>
        public bool LevelUp(int id)
        {
            bool result;
            var user = _userRepository.FindById(id);

            if (user == null)
            {
                result = false; // not found
            }
            else if (user.UserType == UserType.Admin || user.UserType == UserType.Support)
            {
                result = false; // technical users cannot be promoted
            }
            else if (user.UserType == UserType.Customer) // to Silver
            {
                if (user.UserStatus == UserStatus.Active)
                {
                    var payments = _paymentRepository.GetUserPayments(user.Id);
                        
                    if (payments == null ||
                        payments.Count < 5 ||
                        payments.Count(x => x.PaymentStatus == PaymentStatus.Approved) < 3 ||
                        payments.Any(x => x.PaymentStatus == PaymentStatus.Rejected) ||
                        payments.Any(x => x.PaymentStatus == PaymentStatus.Pending))
                    {
                        // no pending or rejected payments allowed.
                        // customer must have at least 5 payments,
                        // at least 3 of them must be approved.
                        result = false;
                    }
                    else
                    {
                        user.UserType = UserType.SilverCustomer;
                        _userRepository.Modify(user);
                        result = true;
                    }
                }
                else
                {
                    result = false; // cannot promote inactive user
                }
            }
            else if (user.UserType == UserType.SilverCustomer) // to Gold
            {
                if (user.UserStatus == UserStatus.Active)
                {
                    var payments = _paymentRepository.GetUserPayments(user.Id);

                    if (payments == null ||
                        payments.Count < 7 ||
                        payments.Count(x => x.PaymentStatus == PaymentStatus.Approved) < 5 ||
                        payments.Any(x => x.PaymentStatus == PaymentStatus.Rejected) ||
                        payments.Any(x => x.PaymentStatus == PaymentStatus.Pending))
                    {
                        // no pending or rejected payments allowed.
                        // customer must have at least 7 payments,
                        // at least 5 of them must be approved.
                        result = false;
                    }
                    else
                    {
                        user.UserType = UserType.GoldCustomer;
                        _userRepository.Modify(user);
                        result = true;
                    }
                }
                else
                {
                    result = false; // cannot promote inactive user
                }
            }
            else if (user.UserType == UserType.GoldCustomer)
            {
                result = false; // i'm at the top of the food chain! (forever?)
            }
            else
            {
                throw new ArgumentException("Undefined user type");
            }

            if (result)
            {
                _cache.Update(user);
            }

            return result;
        }
    }
}
