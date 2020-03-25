using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.DTO
{
    public class InfoDTO
    {
        public string MREF { get; set; }
        public string ID { get; set; }
        public string Message { get; set; }
        public string Seqno { get; set; }
        public string SecretCode { get; set; }
        public string OpCode { get; set; }
        public int Time { get; set; }
    }
}
