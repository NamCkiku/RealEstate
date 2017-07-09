using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class ProvinceEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
