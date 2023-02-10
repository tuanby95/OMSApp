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

            var reader = SqlHelper.ExecuteReader(SqlHelper._connectionString, SqlQueryHelper.GetUserInformationByIdQuery(id), CommandType.Text);
            var result = new User();
            while (reader.Read())
            {
                result = new User
                {
                    Id = Int32.Parse(reader[0] + ""),
                    FullName = Convert.ToString(reader[1]),
                    ImageURL = Convert.ToString(reader[2]),
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
        /// <summary>
        /// Insert New user into table User
        /// </summary>
        /// <param name="user">User Info</param>
        /// <returns>Row effected</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static object InsertNewUser(User user)
        {
            //if (user.UserName is null)
            //{
            //    throw new ArgumentNullException(nameof(user.UserName));
            //}

            //if (user.UserPassword is null)
            //{
            //    throw new ArgumentNullException(nameof(user.UserPassword));
            //}
            var sql = string.Format(@"
                INSERT INTO [UserList]
                       ([FullName]
                       ,[ImageURL]
                       ,[PhoneNumber]
                       ,[UserRole]
                       ,[FullAddress]
                       ,[UserStatus]
                       ,[Facebook]
                       ,[Instagram]
                       ,[UserName]
                       ,[UserPassword]
                       ,[Email])
                 VALUES
                       ('{0}',
                       '{1}', 
                       '{2}',
                       '{3}',
                       '{4}',
                       '{5}',
                       '{6}',
                       '{7}',
                       '{8}',
                       '{9}',
                       '{10}');", user.FullName, user.ImageURL, user.PhoneNumber, user.UserRole, user.FullAddress, user.UserStatus, user.Facebook, user.Instagram, user.UserName, user.UserPassword, user.Email);

            return InsertIntoUserTable(sql);
        }
        internal static object InsertIntoUserTable(String sql)
        {
            var respond = SqlHelper.ExecuteNonQuery(SqlHelper._connectionString, sql, CommandType.Text);

            return respond;
        }
    }
}
