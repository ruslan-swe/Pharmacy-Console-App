using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Abstract
{
    public interface IDrugRepos : IRepos<Drug>
    {
         List<Drug> GetAllByDrugStore(int id);
         List<Drug> GetDrugsByPrice(double price);
    }
}
