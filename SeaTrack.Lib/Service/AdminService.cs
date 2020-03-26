using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNuke.UI.UserControls;
using Microsoft.ApplicationBlocks.Data;
using SeaTrack.Lib.Database;
using SeaTrack.Lib.DTO;
using SeaTrack.Lib.DTO.Admin;

namespace SeaTrack.Lib.Service
{
    public class AdminService
    {
        public static int CreateUser(UserInfoDTO user, int RoleID)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_CreateUser",
                 user.Username, user.Password, user.Fullname, user.Phone, user.Address, user.CreateBy, RoleID);
        }
        public static int UpdateUser(UserInfoDTO user)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateUser",
                user.UserID, user.Status);

        }
        public static List<UserInfoDTO> GetListUser(int RoleID)
        {
            List<UserInfoDTO> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListUser", RoleID);
            if (reader.HasRows)
            {
                lst = new List<UserInfoDTO>();

                while (reader.Read())
                {
                    var data = new UserInfoDTO()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Status = Convert.ToInt32(reader["Status"]),
                        CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }

        public static UserInfoDTO GetUserByID(int UserID)
        {
            UserInfoDTO user = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetUserByID", UserID);
            if (reader.HasRows)
            {
                user = new UserInfoDTO();
                while (reader.Read())
                {
                    var data = new UserInfoDTO()
                    {
                        UserID = Convert.ToInt16(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["FullName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Status = Convert.ToInt16(reader["Status"]),
                        CreateBy = reader["CreateBy"].ToString(),
                        CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                        UpdateBy = reader["UpdateBy"].ToString(),
                        LastUpdateDate = Convert.ToDateTime(reader["LastUpdateDate"]),
                        RoleID = Convert.ToInt16(reader["RoleID"]),
                    };
                    user = data;
                }
                return user;

            }
            return null;
        }

        public static bool EditUser(UserInfoDTO user)
        {
            try
            {
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_EditUser", user.UserID, user.Username, user.Password, user.Fullname, user.Phone, user.Address, user.Status, user.UpdateBy, user.LastUpdateDate, user.RoleID);
                if (res == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteUser(int UserID)
        {
            try
            {
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_DeleteUser", UserID);
                if (res == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #region
        public static int CreateDevice(Device device)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_CreateDevice", device.DeviceNo, device.DeviceName, device.DateExpired, device.DeviceNote,
                device.CreateBy);
        }
        public static int UpdateDevice(Device device)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateDevice", device.DeviceID, device.DeviceNo, device.DeviceName,
                device.DateExpired, device.DeviceNote, device.Status, device.LastUpdateBy);
        }
        public static List<Device> GetListDevice()
        {
            List<Device> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDevice");
            if (reader.HasRows)
            {
                lst = new List<Device>();

                while (reader.Read())
                {
                    var data = new Device()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceName = reader["DeviceName"].ToString(),
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }
        #endregion
    }
}
