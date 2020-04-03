using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using SeaTrack.Lib.Database;
using SeaTrack.Lib.DTO;
using SeaTrack.Lib.DTO.Admin;
using System.Globalization;

namespace SeaTrack.Lib.Service
{
    public class AdminService
    {
        public static int CreateUser(UserInfoDTO user, int RoleID)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_CreateUser",
                 user.Username, user.Password, user.Fullname, user.Phone, user.Address, user.CreateBy, user.CreateDate, RoleID, user.ManageBy, user.Status);
        }
        public static int UpdateUser(UserInfoDTO user)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateUser",
                user.UserID, user.Status);

        }
        public static List<UserViewModel> GetListUser(int RoleID)
        {
            List<UserViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListUser", RoleID);
            if (reader.HasRows)
            {
                lst = new List<UserViewModel>();

                while (reader.Read())
                {
                    var data = new UserViewModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Status = Convert.ToInt32(reader["Status"]),
                        CreateDate = reader["CreateDate"].ToString(),
                        ManageBy = reader["ManageBy"].ToString()
                    };
                    lst.Add(data);
                }
                return lst;
            }
            return null;
        }
        public static UserViewModel GetUserByID(int UserID)
        {
            UserViewModel user = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetUserByID", UserID);
            if (reader.HasRows)
            {
                user = new UserViewModel();
                while (reader.Read())
                {
                    var data = new UserViewModel()
                    {
                        UserID = Convert.ToInt16(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["FullName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Status = Convert.ToInt16(reader["Status"]),
                        CreateBy = reader["CreateBy"].ToString(),
                        CreateDate = reader["CreateDate"].ToString(),
                        UpdateBy = reader["UpdateBy"].ToString(),
                        LastUpdateDate = reader["LastUpdateDate"].ToString(),
                        RoleID = Convert.ToInt16(reader["RoleID"]),
                        ManageBy = reader["ManageBy"].ToString()
                    };
                    user = data;
                }
                return user;

            }
            return null;
        } //Lấy đối tượng user theo UserID
        public static List<UserViewModel> GetListUserByUserID(string Username, int RoleID) //Lấy danh sách user được quản lý bởi Username
        {
            List<UserViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListUserByUserID", Username, RoleID);
            if (reader.HasRows)
            {
                lst = new List<UserViewModel>();

                while (reader.Read())
                {
                    var data = new UserViewModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Status = Convert.ToInt32(reader["Status"]),
                        CreateDate = reader["CreateDate"].ToString(),
                        ManageBy = reader["ManageBy"].ToString()
                    };
                    lst.Add(data);
                }
                return lst;
            }
            return null;
        }
        public static List<UserViewModel> GetListUserOfAgency(string Username)
        {
            List<UserViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListUserOfAgency", Username);
            if (reader.HasRows)
            {
                lst = new List<UserViewModel>();

                while (reader.Read())
                {
                    var data = new UserViewModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Status = Convert.ToInt32(reader["Status"]),
                        CreateDate = reader["CreateDate"].ToString(),
                        ManageBy = reader["ManageBy"].ToString()
                    };
                    lst.Add(data);
                }
                return lst;
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
        public static bool UpdateStatusUser(int UserID, int Status)
        {
            try
            {
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateUser", UserID, Status);
                if (res == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static bool CheckUserManage(int UserID, string Username)
        {
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_CheckUserManage", UserID, Username);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (Convert.ToInt16(reader["result"]) == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #region
        public static int CreateDevice(Device device)
        {
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "[sp_CreateDevice]", device.DeviceNo, device.DeviceName, device.DeviceVersion,
                device.DeviceImei, device.DeviceGroup, device.DateExpired, device.DeviceNote, device.StatusDevice, device.CreateBy, device.DateCreate);
            if (reader.HasRows)
            {
                int ID = 0;
                while (reader.Read())
                {
                    ID = Convert.ToInt32(reader["DeviceID"]);
                }
                return ID;

            }
            return 0;
        }

        public static int UpdateDevice(Device device)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateDevice", device.DeviceID, device.DeviceNo, device.DeviceName,
                device.DeviceVersion, device.DeviceImei, device.DeviceGroup, device.DateExpired, device.DeviceNote);
        }

        public static List<DeviceViewModel> GetListDevice()
        {
            List<DeviceViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "View_GetListDevice");
            if (reader.HasRows)
            {
                lst = new List<DeviceViewModel>();

                while (reader.Read())
                {
                    var data = new DeviceViewModel()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DeviceImei = reader["DeviceImei"].ToString(),
                        DeviceVersion = reader["DeviceVersion"].ToString(),
                        DeviceGroup = reader["DeviceGroup"].ToString(),
                        DeviceNote = reader["DeviceNote"].ToString(),
                        DateExpired = (reader["DateExpired"].ToString().Substring(0, 10)),
                        StatusDevice = Convert.ToInt32(reader["StatusDevice"]),
                        ExpireStatus = DateTime.Compare(Convert.ToDateTime(reader["DateExpired"]), DateTime.Now) > 0 ? 1 : -1,
                        ExpireDate = Convert.ToDateTime(reader["DateExpired"].ToString())



                    };
                    lst.Add(data);
                }
            }
            return lst;
        }//Lấy danh sách tất cả device

        public static DeviceViewModel GetDeviceByID(int deviceID)
        {
            DeviceViewModel device = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetDeviceByID", deviceID);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var data = new DeviceViewModel()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DeviceImei = reader["DeviceImei"].ToString(),
                        DeviceVersion = reader["DeviceVersion"].ToString(),
                        DeviceGroup = reader["DeviceGroup"].ToString(),
                        DeviceNote = reader["DeviceNote"].ToString(),
                        DateExpired = (reader["DateExpired"].ToString()),
                        ExpireDate = Convert.ToDateTime(reader["DateExpired"].ToString())
                    };
                    device = data;
                }
            }
            return device;
        }//Lấy device theo ID

        //Lấy danh sách device theo UserID
        public static List<DeviceViewModel> GetListDeviceByUserID(int UserID)
        {
            List<DeviceViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDeviceByUserID", UserID);
            if (reader.HasRows)
            {
                lst = new List<DeviceViewModel>();

                while (reader.Read())
                {
                    var data = new DeviceViewModel()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DeviceImei = reader["DeviceImei"].ToString(),
                        DeviceVersion = reader["DeviceVersion"].ToString(),
                        DeviceGroup = reader["DeviceGroup"].ToString(),
                        DeviceNote = reader["DeviceNote"].ToString(),
                        DateExpired = (reader["DateExpired"].ToString()),
                        StatusDevice = Convert.ToInt32(reader["StatusDevice"]),
                        ExpireStatus = DateTime.Compare(Convert.ToDateTime(reader["DateExpired"]), DateTime.Now) > 0 ? 1 : -1
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }

        //UserID != null, Lấy danh sách thiết bị thuộc về UserID nhưng chưa được gán cho người dùng khác
        //UserID == null, lấy danh sách thiết bị chưa được gán cho bất kỳ người dùng
        public static List<DeviceViewModel> GetListDeviceNotUsedByUser(string Username)
        {
            List<DeviceViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDeviceNotUsedByUser", Username);
            if (reader.HasRows)
            {
                lst = new List<DeviceViewModel>();

                while (reader.Read())
                {
                    var data = new DeviceViewModel()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DateExpired = reader["DateExpired"].ToString()
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }

        public static List<DeviceViewModel> GetListDeviceBelongToAgencyNotUsedByUser(int AgencyID, string Username)
        {
            List<DeviceViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDeviceBelongToAgencyNotUsedByUser", AgencyID, Username);
            if (reader.HasRows)
            {
                lst = new List<DeviceViewModel>();
                while (reader.Read())
                {
                    var data = new DeviceViewModel()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DateExpired = reader["DateExpired"].ToString()
                    };
                    lst.Add(data);
                }
                return lst;

            }
            return null;
        }

        public static List<DeviceViewModel> GetListDeviceOfCustomer(string Username, int UserID)
        {
            List<DeviceViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDeviceOfCustomer", Username, UserID);
            if (reader.HasRows)
            {
                lst = new List<DeviceViewModel>();
                while (reader.Read())
                {
                    var data = new DeviceViewModel()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DateExpired = reader["DateExpired"].ToString()
                    };
                    lst.Add(data);
                }
                return lst;

            }
            return null;

        }
        public static int RemoveDeviceFromUser(int UserID, int DeviceID)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_RemoveDeviceFromUser", UserID, DeviceID);
        }

        public static int AddDeviceToUser(int UserID, int DeviceID, string CreateBy)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_AddDeviceToUser", UserID, DeviceID, CreateBy);
        }

        public static bool DeleteDevice(int DeviceID)
        {
            try
            {
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_DeleteDevice", DeviceID);
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

        public static bool UnlockDevice(int DeviceID)
        {
            try
            {
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UnlockDevice", DeviceID);
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

        public static string CheckDeviceExist(string DeviceNo, string DeviceImei)
        {
            string No = null;
            string Imei = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_CheckDeviceExist", DeviceNo, DeviceImei);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (DeviceNo != null)
                        No = reader["DeviceNo"].ToString();
                    else
                        Imei = reader["DeviceImei"].ToString();
                }
                if (No == null)
                    return Imei;
                return No;
            }
            return null;
        }

        public static bool CheckUserDevice (int UserID, int DeviceID)
        {
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_CheckUserDevice", UserID, DeviceID);
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }


        #endregion
    }
}
