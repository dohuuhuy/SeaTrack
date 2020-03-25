using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using SeaTrack.Lib.Database;
using SeaTrack.Lib.DTO;

namespace SeaTrack.Lib.Service
{
    public class TrackDataService
    {
        public static int AddTrackData(TrackData data)
        {
            var dateTime = DateTime.Now;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_AddTrackData",
                data.DeviceNo, data.Latitude, data.Longitude,
                data.DirectionSN, data.DirectionEW, data.Speed, data.TransmitTime, unixDateTime);
        }
        public static List<DeviceStatus> GetListDeviceStatus()
        {
            List<DeviceStatus> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetListDeviceStatus");
            if (reader.HasRows)
            {
                lst = new List<DeviceStatus>();

                while (reader.Read())
                {
                    var data = new DeviceStatus()
                    {
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        DeviceName = reader["DeviceName"].ToString(),
                        Latitude = Convert.ToDecimal(reader["Latitude"]),
                        Longitude = Convert.ToDecimal(reader["Longitude"]),
                        DirectionSN = reader["DirectionSN"].ToString(),
                        DirectionEW = reader["DirectionEW"].ToString(),
                        TransmitTime = Convert.ToDateTime(reader["TransmitTime"]),
                        Speed = Convert.ToInt16(reader["Speed"]),
                        
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }
        public static TrackData GetLastedLocation(int deviceID)
        {
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetLastedLocation",
                deviceID);
            while (reader.Read())
            {
                var data = new TrackData()
                {
                    Latitude = Convert.ToDecimal(reader["Latitude"]),
                    Longitude = Convert.ToDecimal(reader["Longitude"]),
                    Speed = Convert.ToInt16(reader["Speed"]),
                    State = reader["State"].ToString(),
                };
                return data;
            }
            return null;
        }
        public static List<TrackData> GetRoadmap(int deviceID)
        {
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetRoadmap",
                deviceID);
            List<TrackData> lst = null;
            if (reader.HasRows)
            {
                lst = new List<TrackData>();
                while (reader.Read())
                {
                    var data = new TrackData()
                    {
                        Latitude = Convert.ToDecimal(reader["Latitude"]),
                        Longitude = Convert.ToDecimal(reader["Longitude"])
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }
        public static List<TrackData> GetRoadmapByDateTime(int deviceID, DateTime from, DateTime to)
        {
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetRoadmapByDateTime",
                deviceID, from, to);
            List<TrackData> lst = null;
            if (reader.HasRows)
            {
                lst = new List<TrackData>();
                while (reader.Read())
                {
                    var data = new TrackData()
                    {
                        DeviceID = deviceID,
                        DirectionSN = Convert.ToString(reader["DirectionSN"]),
                        DirectionEW = Convert.ToString(reader["DirectionEW"]),
                        Latitude = Convert.ToDecimal(reader["Latitude"]),
                        Longitude = Convert.ToDecimal(reader["Longitude"]),
                        TransmitTime = Convert.ToDateTime(reader["TransmitTime"]),
                        Speed = Convert.ToInt16(reader["Speed"])

                    };
                    lst.Add(data);
                }
            }
            return lst;
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
        public static int AddMessage(string id, string Message)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_AddMessage",
                id, Message);
        }
        public static int AddConfigTime(string id, string OpCode, int Time)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_ConfigTime",
                id, OpCode, Time);
        }
    }
}
