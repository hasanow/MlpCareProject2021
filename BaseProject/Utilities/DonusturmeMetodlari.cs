using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities
{
    public static class DonusturmeMetodlari
    {
        public static string ObjectToJsonString(this object data)
        {
            if (data == null) return null;

            string jsonData = JsonConvert.SerializeObject(data);
            return jsonData;
        }
        public static T JsonStringToObject<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return default;

            T data = JsonConvert.DeserializeObject<T>(jsonString);
            return data;
        }
        public static object JsonStringToObject(this string jsonString, Type type)
        {
            if (string.IsNullOrEmpty(jsonString)) return default;

             object data = JsonConvert.DeserializeObject(jsonString,type);
            return data;
        }       
    }
}
