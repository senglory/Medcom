using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CreditCard: BaseObj
    {
        public string Number { get; set; }
        public DateTime? ExpDate { get; set; }
        public override bool Fit(string filter)
        {
            var r = base.Fit(filter);
            return r || Number.Contains(filter) ||  (ExpDate.HasValue ? ExpDate.ToString().Contains(filter) : false );
        }
    }
}
