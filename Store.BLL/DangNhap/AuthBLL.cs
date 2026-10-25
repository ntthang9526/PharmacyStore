using Store.DAL;
using Store.Shared.DTOs.Auth;
using Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL
{
    public class AuthBLL
    {
        private readonly EmployeeDAL employeeDAL = new();
        
        public LoginResponse Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Vui lòng nhập tài khoản và mật khẩu"
                };
            }
            string username = request.Username.Trim();
            string password = request.Password;

            if (username.Length > 50)
            {
                return LoginFailed();
            }

            Employee? employee = employeeDAL.GetEmployeeByUsername(username);
            if (employee == null || employee.IsActive == false)
            {
                return LoginFailed();
            }
            if (!MatKhauNhanVien.KiemTra(password, employee.PasswordHash))
            {
                return LoginFailed();
            }

            return new LoginResponse
            {
                Success = true,
                Message = "Đăng nhập thành công! Xin chào " + employee.FullName + ".",
                FullName = employee.FullName,
                EmployeeID = employee.ID,
                Role = employee.Role
            };


        }
        private static LoginResponse LoginFailed()
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Tên tài khoản hoặc mật khẩu không đúng"
            };

        }
    }
}
