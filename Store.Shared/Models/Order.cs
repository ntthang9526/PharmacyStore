using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.Models
{
    public class Order
    {
        public string Id { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public int VoucherId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Channel { get; set; } = string.Empty;
    }
}
