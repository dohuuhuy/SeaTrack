using DotNetNuke.UI.UserControls;
using Microsoft.ApplicationBlocks.Data;
using SeaTrack.Lib.Database;
using SeaTrack.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.Service
{
    public class UsersService
    {
        public static Users CheckUsers(String name, String pass)
        {
            if (name == null || pass == null) return null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetDataUser", name, pass);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var data = new Users()
                    {
                        UserID = Convert.ToInt16(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Status = Convert.ToInt16(reader["Status"]),
                        CreateBy = reader["CreateBy"].ToString(),
                        CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                        UpdateBy = Convert.ToDateTime(reader["UpdateBy"]),
                        LastUpdateDate = Convert.ToDateTime(reader["LastUpdateDate"]),
                        RoleID = Convert.ToInt16(reader["RoleID"]),

                    };
                    return data;
                }
            }
            return null;
        }


    }
}
