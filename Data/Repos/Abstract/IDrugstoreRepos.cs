﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Abstract
{
    public interface IDrugStoreRepos : IRepos<DrugStore>
    {
        List<DrugStore> GetAllByOwner(int id);
        List<Drug> GetAvailableDrugs();
        Drug BuyAvailableDrug(int id);
    }
}