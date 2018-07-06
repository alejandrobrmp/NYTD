using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Interfaces
{
    public interface ICacheService
    {
        void Save(string key, object value);
        T Get<T>(string key, bool delete = true) where T : class;
    }
}
