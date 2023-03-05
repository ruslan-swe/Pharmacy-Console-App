using Core.Entities;
using Data.Context;
using Data.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Concrete
{
    public class DrugStoreRepos : IDrugStoreRepos
    {
        static int id;
        public List<DrugStore> GetAll()
        {
            return DbContext.DrugStores;
        }
        public List<DrugStore> GetAllByOwner(int id)
        {
            return DbContext.DrugStores.Where(o => o.Owner.Id == id).ToList();
        }
        public List<Drug> GetDrugs()
        {
            return DbContext.Drugs.Where(s => s.Count > 0).ToList();
        }
        public DrugStore Get(int id)
        {
            return DbContext.DrugStores.FirstOrDefault(s => s.Id == id);
        }
        public void Add(DrugStore drugStore)
        {
            ++id;
            drugStore.Id = id;
            DbContext.DrugStores.Add(drugStore);
        }
        public void Update(DrugStore drugStore)
        {
            var dbDrugStore = DbContext.DrugStores.FirstOrDefault(s => s.Id == drugStore.Id);
            if (dbDrugStore is not null) 
            {
                dbDrugStore.Owner = drugStore.Owner;
                dbDrugStore.Name = drugStore.Name;
                dbDrugStore.Address = drugStore.Address;
                dbDrugStore.ContactNumber = drugStore.ContactNumber;
                dbDrugStore.Email = drugStore.Email;
            }
        }
        public void Delete(DrugStore drugStore)
        {
            DbContext.DrugStores.Remove(drugStore);
        }

    }
}
