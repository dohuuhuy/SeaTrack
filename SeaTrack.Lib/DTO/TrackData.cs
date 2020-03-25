using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.DTO
{
    public class TrackData
    {
        public Int64 ID { get; set; }
        public int DeviceID { get; set; }
        public string DeviceNo { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Speed { get; set; }
        public string DirectionSN { get; set; }
        public string DirectionEW { get; set; }
        public DateTime TransmitTime { get; set; }
        public string State { get; set; }
    }
}
