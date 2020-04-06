using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaTrack.Lib.DTO.Admin
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        [RegularExpression(@"^[a-z0-9 ]{5,30}$",
                    ErrorMessage = "Password không được chứa ký tự unicode")]
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public string LastUpdateDate { get; set; }
        public int RoleID { get; set; }
        public string ManageBy { get; set; }

    }
}
