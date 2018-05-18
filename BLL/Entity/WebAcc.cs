using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WebAcc: BaseObj
    {
        public string Url { get; set; }
        public override bool Fit(string filter)
        {
            var r = base.Fit(filter);
            return r || Url.Contains(filter);
        }
    }
}
