using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.DTOs
{
    public class MedicineDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
