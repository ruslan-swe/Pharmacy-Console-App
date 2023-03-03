using Core.Entities;
using Data.Context;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DbInitializer
    {
        static int Id;        
        public static void SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Id = ++Id,
                    Username = "1",
                    Password = PasswordHasher.Encrypt("1")
                },
                new Admin
                {
                    Id = ++Id,
                    Username = "Manager",
                    Password = PasswordHasher.Encrypt("tryme"),
                },
                new Admin
                {
                    Id = ++ Id,
                    Username = "Accountant",
                    Password = PasswordHasher.Encrypt("letssee")
                },              
            };
            DbContext.Admins.AddRange(admins);
        }
    }
}