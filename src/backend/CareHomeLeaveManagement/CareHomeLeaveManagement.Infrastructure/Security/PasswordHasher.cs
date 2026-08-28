using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CareHomeLeaveManagement.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<string> _hasher = new();
        public string HashPassword(string password)
        {
            return _hasher.HashPassword(string.Empty, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var result=_hasher.VerifyHashedPassword(
                string.Empty,
                hashedPassword, 
                password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
