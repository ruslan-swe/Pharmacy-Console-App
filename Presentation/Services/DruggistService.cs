using Data.Repos.Concrete;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Presentation.Services
{
    public class DruggistService
    {
        private readonly DruggistRepos _druggistRepos;
        public DruggistService()
        {
            _druggistRepos = new DruggistRepos();
        }

        public void Create(Admin admin)
        {
            ConsoleHelper.WriteWithColor("Enter Name :");
        }
    }
}
