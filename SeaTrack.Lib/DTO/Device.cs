using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.DTO
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceImei { get; set; }
        public DateTime DateCreate { get; set; }
        public string DeviceGroup { get; set; }
        public DateTime DateExpired { get; set; }
        public string DeviceNote { get; set; }

        public string CreateBy { get; set; }
        public int StatusDevice { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
