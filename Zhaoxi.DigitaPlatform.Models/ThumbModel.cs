using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.DigitaPlatform.Models
{
    public class ThumbModel
    {
        public string Header { get; set; }

        public List<ThumbItemModel> Children { get; set; } = new List<ThumbItemModel>();
    }
}
