using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Dao
{
    /// <summary>
    /// Hi, I'm UserRepository! I don't know much about users, but I can save/return them to/from db.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public void Create(User user)
        {
            var u = FindById(user.Id);
            if (u != null)
                throw new ArgumentException();
            _users.Add(user);
        }

        public void Modify(User user)
        {
            var u = FindById(user.Id);
            if (u == null)
                throw new ArgumentException();
            u.Name = user.Name;
            u.UserType = user.UserType;
        }

        public void Delete(User user)
        {
            var u = FindById(user.Id);
            if (u == null)
                throw new ArgumentException();
            _users.Remove(u);
        }

        public List<User> FindAll()
        {
            return _users;
        }

        public User FindById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        #region Data (assume it comes from database)

        private readonly List<User> _users = new List<User>
        {
            new User { Id = 0, Name = "Admin", UserType = UserType.Admin, UserStatus = UserStatus.Active },
            new User { Id = 1, Name = "Wonder Woman", UserType = UserType.Support, UserStatus = UserStatus.Active },
            new User { Id = 2, Name = "Captain America", UserType = UserType.Customer, UserStatus = UserStatus.Disabled },
            new User { Id = 3, Name = "Flash Gordon", UserType = UserType.Customer, UserStatus = UserStatus.Active },
            new User { Id = 4, Name = "Hulk", UserType = UserType.SilverCustomer, UserStatus = UserStatus.Active },
            new User { Id = 5, Name = "Thor", UserType = UserType.GoldCustomer, UserStatus = UserStatus.Active }
        };

        #endregion
    }
}
