using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.DigitaPlatform.Models
{
    public class MenuModel
    {
        public bool IsSelected { get; set; }

        public string MenuHeader { get; set; }

        public string TargetView { get; set; }

        public string MenuIcon { get; set; }
    }
}
