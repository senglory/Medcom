using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IBLL
    {
        IEnumerable<BaseObj> GetFiltered(string filter = "", int pageSize = 10, int offset = 0);
        BaseObj GetItemById(int id);

        void Add(BaseObj obj);
        void Update(BaseObj obj);

        void DeleteById(int id);
        void Flush();
    }
}
