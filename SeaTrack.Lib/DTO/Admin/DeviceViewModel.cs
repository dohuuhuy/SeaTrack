using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaTrack.Areas.Admin.Model
{
    public class DeviceViewModel
    {
        public int DeviceID { get; set; }
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceImei { get; set; }
        public string DateCreate { get; set; }
        public string DeviceGroup { get; set; }
        public string DateExpired { get; set; }
        public string DeviceNote { get; set; }

        public int CreateBy { get; set; }
        public int StatusDevice { get; set; }
        public int LastUpdateBy { get; set; }
        public string LastUpdateDate { get; set; }
        public int ExpireStatus { get; set; }
    }
}