using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaTrack.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string LoginName { get; set; }
        public string Password_ { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public int? WhoCreateID { get; set; }
        public string Details { get; set; }
        public int? bEditDevice { get; set; }
        public string Domain { get; set; }
        public string WhoCreate { get; set; }
        public string RoleName { get; set; }
        public bool loginStatus { get; set; }
    }
}