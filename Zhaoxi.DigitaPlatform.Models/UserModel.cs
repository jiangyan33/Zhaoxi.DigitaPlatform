﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.DigitaPlatform.Models
{
    public class UserModel
    {
        public string UserName { get; set; } = "admin";
        public string Password { get; set; } = "123456";

        public string RealName { get; set; }
        public string EmployNum { get; set; }
        public string PhoneNumber { get; set; }

        public int UserType { get; set; }

        public int Gender { get; set; }

        public string Department { get; set; }
    }
}
