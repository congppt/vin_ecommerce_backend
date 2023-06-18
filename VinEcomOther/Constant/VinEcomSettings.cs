using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VinEcomOther.Constant
{
    public static class VinEcomSettings
    {
        public static Dictionary<string , object> Settings { get; set; }
        static VinEcomSettings()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "VinEcomSettings.json");
            string dict = File.ReadAllText(path);
            Settings = JsonSerializer.Deserialize<Dictionary<string, object>>(dict);
        }
    }
}
