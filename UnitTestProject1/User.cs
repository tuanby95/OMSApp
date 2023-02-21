using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UnitTestProject1
{
    [Table("UserInfo")]
    internal class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string GENDER { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string UserStatus { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public byte[] Avatar { get; set; }
    }
}