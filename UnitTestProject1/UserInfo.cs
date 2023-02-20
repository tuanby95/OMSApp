using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [Table("UserInfo")]
    internal class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string FullAddress { get; set; }
        public string UserStatus { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}