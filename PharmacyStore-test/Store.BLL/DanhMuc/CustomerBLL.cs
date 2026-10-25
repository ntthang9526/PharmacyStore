using Store.DAL;
using Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL
{
    public class CustomerBLL
    {
        private readonly CustomerDAL customerDAL = new();
    }
}
