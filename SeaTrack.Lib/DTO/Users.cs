using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.DTO
{
    public class Users
    {
        public int UserID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public int Status { get; set; }
        public String CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int RoleID { get; set; }



    }
}
