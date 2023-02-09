using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMSTest;

namespace UnitTestProject1
{
    public static class UserService
    {
        /// <summary>
        /// Get user information by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Object contain user information</returns>
        internal static User GetUserInformationById(int id)
        {
            
            var reader = SqlHelper.ExecuteReader(SqlQueryHelper.GetUserInformationByIdQuery(id), CommandType.Text);
            var result = new User();
            while (reader.Read())
            {
                result = new User
                {
                    Id = Int32.Parse(reader[0] + ""),
                    FullName = Convert.ToString(reader[1]),
                    ImageURL = Convert.ToString(reader[2]),
                    PhoneNumber = Int32.Parse(reader[3] + ""),
                    Role = Convert.ToString(reader[4]),
                    Address = Convert.ToString(reader[5]),
                    UserStatus = Convert.ToString(reader[6]),
                    Facebook = Convert.ToString(reader[7]),
                    Instagram = Convert.ToString(reader[8]),
                    UserName = Convert.ToString(reader[9]),
                    Password = Convert.ToString(reader[10]),
                };
            }

            return result;
        }

        internal static int UpdatePassword(User user, string oldPassword, string newPassword)
        {
            if (oldPassword == "" || oldPassword.Length > 16) { return 0; }

            var sql = SqlQueryHelper.UpdatePasswordQuery(user, oldPassword, newPassword);

            var result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text);
            return result;
        }
    }
}
