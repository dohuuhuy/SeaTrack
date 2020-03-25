using Microsoft.ApplicationBlocks.Data;
using SeaTrack.Lib.Database;
using SeaTrack.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.Service
{
    class SOSService
    {
        public static int UpdateStatusSOSbyID(SOSDTO sos)
        {
            return SqlHelper.ExecuteNonQuery(ConnectData.ConnectionString, "sp_UpdateSOSStatus",
                sos.SOSID, sos.Status);
        }
        public static List<SOSDTO> GetSOSPending()
        {
            List<SOSDTO> lst = null;
            var reader = SqlHelper.ExecuteReader(ConnectData.ConnectionString, "sp_GetSOSPending");
            if (reader.HasRows)
            {
                lst = new List<SOSDTO>();

                while (reader.Read())
                {
                    var data = new SOSDTO()
                    {
                        SOSID = Convert.ToInt32(reader["SOSID"]),
                        DeviceID = Convert.ToInt32(reader["DeviceID"]),
                        Latitude = Convert.ToDecimal(reader["Latitude"]),
                        Longitude = Convert.ToDecimal(reader["Longitude"]),
                        DirectionSN = reader["DirectionSN"].ToString(),
                        DirectionEW = reader["DirectionEW"].ToString(),
                        DateRequest = Convert.ToDateTime(reader["DateRequest"]),
                        GMT = reader["GMT"].ToString(),
                    };
                    lst.Add(data);
                }
            }
            return lst;
        }
    }
}
