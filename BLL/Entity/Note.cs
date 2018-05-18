using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Note: BaseObj
    {
        public string Content { get; set; }

        public override bool Fit(string filter)
        {
            var r = base.Fit(filter);
            return r || Content.Contains(filter);
        }
    }
}
