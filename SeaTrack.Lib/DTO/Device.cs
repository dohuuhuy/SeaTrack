using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.DTO
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceNo { get; set; }
        public DateTime DateExpired { get; set; }
        public string DeviceNote { get; set; }
        public int CreateBy { get; set; }
        public string Status { get; set; }
        public int LastUpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
