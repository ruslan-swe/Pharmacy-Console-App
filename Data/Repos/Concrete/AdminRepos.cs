using Data.Context;
using Data.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;

namespace Data.Repos.Concrete
{
    public class AdminRepos : IAdminRepos
    {
        public Admin GetByUsernameAndPassword(string username, string password)
        {
            return DbContext.Admins.FirstOrDefault(a => a.Username.ToLower() == username.ToLower() && PasswordHasher.Decrypt(a.Password) == password);
        }
        public void LogOut()
        {
            
        }
    }
}