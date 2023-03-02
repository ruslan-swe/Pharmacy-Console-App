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
    public class OwnerRepos : IOwnerRepos
    {
        static int id;
        public List<Owner> GetAll()
        {
            return DbContext.Owners;
        }
        public Owner Get(int id)
        {
            return DbContext.Owners.FirstOrDefault(o => o.Id == id);
        }
        public void Add(Owner owner)
        {
            ++id;
            owner.Id = id;
            DbContext.Owners.Add(owner);
        }
        public void Update(Owner owner)
        {
            var dbOwner = DbContext.Owners.FirstOrDefault(o => o.Id == owner.Id);
            if (dbOwner is not null)
            {
                dbOwner.Name = owner.Name;
                dbOwner.Surname = owner.Surname;
            }
        }
        public void Delete(Owner owner)
        {
            DbContext.Owners.Remove(owner);
        }

    }
}