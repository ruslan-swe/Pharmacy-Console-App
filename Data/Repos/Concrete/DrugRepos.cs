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
    public class DrugRepos : IDrugRepos
    {
        static int id;
        public List<Drug> GetAll()
        {
            return DbContext.Drugs;
        }
        public List<Drug> GetAllByDrugStore(int id)
        {
            return DbContext.Drugs.Where(s => s.DrugStore.Id == id).ToList();
        }
        public Drug Get(int id)
        {
            return DbContext.Drugs.FirstOrDefault(d => d.Id == id);
        }
        public void Update(Drug drug)
        {
            var dbDrug = DbContext.Drugs.FirstOrDefault(d => d.Id == drug.Id);
            if (dbDrug is not null)
            {
                dbDrug.Name = drug.Name;
                dbDrug.Price = drug.Price;
                dbDrug.Count = drug.Count;
            }
        }
        public void Add(Drug drug)
        {
            ++id;
            drug.Id = id;
            DbContext.Drugs.Add(drug);
        }
        public void Delete(Drug drug)
        {
            DbContext.Drugs.Remove(drug);
        }
    }
}