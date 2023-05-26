using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.DigitaPlatform.Models
{
    public class UserModel
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public string RealName { get; set; }
        public string EmployNum { get; set; }
        public string PhoneNumber { get; set; }
    }
}
