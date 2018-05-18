using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLL
{
    public class BLL : IBLL
    {
        string _filePath;
        List<BaseObj> _storage = new List<BaseObj>();
        int _lastId = 0;

        public BLL(string filePath)
        {
            _filePath = filePath;
            if (File.Exists(_filePath))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                _storage = JsonConvert.DeserializeObject<List<BaseObj>>(File.ReadAllText(filePath), settings);
                _lastId = _storage.LastOrDefault().Id + 1;
            }
        }

        public void Add(BaseObj obj)
        {
            if (obj.Id == 0) {
                _lastId = _lastId + 1;
                obj.Id = _lastId;
            }
            _storage.Add (obj);
        }

        public void DeleteById(int id)
        {
            var idx = _storage.FindIndex(o => o.Id == id);
            _storage.RemoveAt(idx);
        }

        public IEnumerable<BaseObj> GetFiltered(string filter = "", int pageSize = 10, int offset = 0)
        {
            return _storage.Where ( o => o.Fit(filter)).Skip (offset ).Take(pageSize) ;
        }

        public BaseObj GetItemById(int id)
        {
            return _storage.FirstOrDefault(o => o.Id==id );
        }

        public void Update(BaseObj obj)
        {
            var obj2 = _storage.FirstOrDefault(o => o.Id == obj.Id);
        }

        public void Flush()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string str = JsonConvert.SerializeObject(_storage, settings);
            File.WriteAllText(_filePath, str);
        }
    }
}
