using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.POS.FormBanHang
{
    public class RowData
    {
        public int No { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Unit { get; set; } = "";
        public int Quantity { get; set; }
        public bool IsPrescription {get;set;}
        public string HuongDan {get;set;} = "";
        private decimal price;
        public decimal Price { get => price; set => price = Store.Shared.TienTe.LamTron(value); }
        public decimal Total => Price * Quantity;
    }
}
