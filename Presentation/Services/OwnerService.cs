using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repos.Concrete;
using Core.Helpers;
using Core.Extentions;

namespace Presentation.Services
{
    internal class OwnerService
    {
        private readonly OwnerRepos _ownerRepos;
        public OwnerService()
        {
            _ownerRepos = new OwnerRepos();
        }
        public void Create()
        {
        NameCheck:
            Console.Clear();
            ConsoleHelper.WriteWithColor("Enter owner name", ConsoleColor.Yellow);
            string name = Console.ReadLine();
            if(String.IsNullOrEmpty(name) && name.IsDetails()==false) 
            {
                ConsoleHelper.WriteWithColor("Wrong input format!", ConsoleColor.Red);
                Console.ReadKey();
            }
        }
    }
}
