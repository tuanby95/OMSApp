using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
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
        internal static UserInfo GetUserInformationById(int id)
        {

            var reader = SqlHelper.ExecuteReader(SqlHelper._connectionString, SqlQueryHelper.GetUserInformationByIdQuery(id), CommandType.Text);
            var result = new UserInfo();
            while (reader.Read())
            {
                result = new UserInfo
                {
                    Id = Int32.Parse(reader[0] + ""),
                    FullName = Convert.ToString(reader[1]),
                    PhoneNumber = Convert.ToString(reader[3]),
                    UserRole = Convert.ToString(reader[4]),
                    FullAddress = Convert.ToString(reader[5]),
                    UserStatus = Convert.ToString(reader[6]),
                    Facebook = Convert.ToString(reader[7]),
                    Instagram = Convert.ToString(reader[8]),
                    UserName = Convert.ToString(reader[9]),
                    UserPassword = Convert.ToString(reader[10]),
                };
            }

            return result;
        }

        internal static object InsertNewUser(UserInfo user)
        {
            return InsertIntoUserInfoTable(user);
        }

        internal static long InsertIntoUserInfoTable(UserInfo user)
        {
            using (var conn = new SqlConnection(SqlHelper._connectionString))
            {
                return conn.Insert(user);
            }
        }

        internal static bool DeleteFromuUserInfoTable(int id)
        {
            using (var conn = new SqlConnection(SqlHelper._connectionString))
            {
                return conn.Delete(new UserInfo() { Id = id });
            }
        }

        internal static bool UpdateFromuUserInfoTable(UserInfo user)
        {
            using (var conn = new SqlConnection(SqlHelper._connectionString))
            {
                return conn.Update(user);
            }
        }
    }
}
