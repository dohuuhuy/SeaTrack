﻿using System;
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
using SeaTrack.Areas.Admin.Model;

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
            }
            return lst;
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
        }

        public static bool EditUser(UserInfoDTO user)
        {
            try
            {
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_EditUser", user.UserID, user.Username, user.Password, user.Fullname, user.Phone, user.Address, user.Status, user.UpdateBy, user.LastUpdateDate, user.RoleID, user.ManageBy);
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
        #region
        public static int CreateDevice(Device device)
        {
            return SqlHelper.ExecuteNonQuery
                (
                ConnectData.ConnectionString,
                "View_CreateDevice",

                device.DeviceNo,
                device.DeviceName,
                device.DeviceVersion,
                device.DeviceImei,
                device.DeviceGroup,
                device.DateExpired,
                device.DeviceNote

                );
        }

        public static int UpdateDevice(Device device)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateDevice", device.DeviceID, device.DeviceNo, device.DeviceName,
                device.DateExpired, device.DeviceNote, device.Status, device.LastUpdateBy);
        }

        public static List<Device> GetListDevice()
        {
            List<Device> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "View_GetListDevice");
            if (reader.HasRows)
            {
                lst = new List<Device>();

                while (reader.Read())
                {
                    var data = new Device()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DeviceImei = reader["DeviceImei"].ToString(),
                        DeviceVersion = reader["DeviceVersion"].ToString(),
                        DeviceGroup = reader["DeviceGroup"].ToString(),
                        DeviceNote = reader["DeviceNote"].ToString(),
                        DateExpired = Convert.ToDateTime(reader["DateExpired"].ToString())

                    };
                    lst.Add(data);
                }
            }
            return lst;
        }//Lấy danh sách tất cả device

        public static Device GetDeviceByID(int deviceID)
        {
            Device device = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "View_GetDeviceByID", deviceID);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var data = new Device()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceNo = reader["DeviceNo"].ToString(),
                        DeviceName = reader["DeviceName"].ToString(),
                        DeviceImei = reader["DeviceImei"].ToString(),
                        DeviceVersion = reader["DeviceVersion"].ToString(),
                        DeviceGroup = reader["DeviceGroup"].ToString(),
                        DeviceNote = reader["DeviceNote"].ToString(),
                        DateExpired = Convert.ToDateTime(reader["DateExpired"].ToString())
                    };
                    device = data;
                }
            }
            return device;
        }//Lấy device theo ID

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
                        DateExpired = reader["DateExpired"].ToString()
                    };
                    lst.Add(data);
                }
            }
            return lst;
        } //Lấy danh sách device theo UserID

        public static List<DeviceViewModel> GetListDeviceNotUsedByUser(int UserID)
        {
            List<DeviceViewModel> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDeviceNotUsedByUser", UserID);
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
                int res = SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "View_DeleteDevice", DeviceID);
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
        #endregion
    }
}
