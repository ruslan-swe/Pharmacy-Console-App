using Core.Helpers;
using Data.Repos.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class DrugStoreService
    {
        private readonly DrugStoreRepos _drugStoreRepos;
        private readonly OwnerRepos _ownerRepos;
        public DrugStoreService()
        {
            _drugStoreRepos = new DrugStoreRepos();
            _ownerRepos = new OwnerRepos();
        }

        public void Create()
        {
            if (_ownerRepos.GetAll().Count == 0)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("There is no Owners to assign new to!  Please first create Teacher", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        }
    }
}
