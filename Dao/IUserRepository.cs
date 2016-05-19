using System.Collections.Generic;
using Domain;

namespace Dao
{
    /// <summary>
    /// Hi, I'm UserRepository interface.
    /// If you open Dao assembly in Solution Explorer you'll find a class which implements me.
    /// Some people say that I live in my parents house. What do they mean?
    /// </summary>
    public interface IUserRepository
    {
        void Create(User user);
        
        void Modify(User user);
        
        void Delete(User user);
        
        List<User> FindAll();
        
        User FindById(int id);
    }
}
