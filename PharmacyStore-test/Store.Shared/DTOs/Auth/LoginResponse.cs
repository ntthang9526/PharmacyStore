using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.DTOs.Auth
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? EmployeeID { get; set; }
        public string? FullName { get; set; }
        /// <summary>true nếu tài khoản có quyền Quản trị viên (quản lý nhân viên)</summary>
        public bool IsAdmin { get; set; }
    }
}
