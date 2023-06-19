using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VinEcomDomain.Constant
{
    public static class VinEcomSettings
    {
        public static Dictionary<string, object> Settings { get; set; }
        static VinEcomSettings()
        {
            string parentPath = Path.GetDirectoryName(typeof(VinEcomSettings).Assembly.Location);
            string path = Path.Combine(parentPath, "Constant", "VinEcomSettings.json");
            string dict = File.ReadAllText(path);
            Settings = JsonSerializer.Deserialize<Dictionary<string, object>>(dict);
        }
    }
}
