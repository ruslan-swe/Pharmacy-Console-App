using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Abstract
{
    public interface IAdminRepos
    {
        Admin GetByUsernameAndPassword(string username, string password);
        public void LogOut();
    }
}
