using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Abstract
{
    public interface IDruggistRepos : IRepos<Druggist>
    {
        List<Druggist> GetAllByDrugStore(int id);
    }
}
