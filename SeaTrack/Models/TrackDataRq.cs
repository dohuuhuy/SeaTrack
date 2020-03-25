using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SeaTrack.Models
{
    public class TrackDataRq
    {
        public string imei { get; set; }
        public string monmsn { get; set; }
        public string transmit_time { get; set; }
        public decimal iridium_latitude { get; set; }
        public decimal iridium_longtitude { get; set; }
        public string  iridium_cep { get; set; }
        public string data { get; set; }
    }
}