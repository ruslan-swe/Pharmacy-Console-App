using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Owner : BaseEntity 
    {
        public string Surname { get; set; }
        public List<DrugStore> DrugStores { get; set; }
    }
}
