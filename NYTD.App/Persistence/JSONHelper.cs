using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Persistence
{
    public class JSONHelper
    {
        private static readonly string DBPath = AppDomain.CurrentDomain.BaseDirectory + "/data";
        
        public T Deserialize<T>(string fileName)
        {
            string json = File.ReadAllText($"{DBPath}/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Serialize(string fileName, object o)
        {
            string json = JsonConvert.SerializeObject(o, Formatting.Indented);
            FileInfo file = new FileInfo($"{DBPath}/{fileName}.json");
            Directory.CreateDirectory(file.Directory.FullName);
            File.WriteAllText(file.FullName, json, Encoding.UTF8);
        }

    }
}
