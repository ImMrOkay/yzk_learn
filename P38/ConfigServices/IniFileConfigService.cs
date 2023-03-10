using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ConfigServices
{
    public class IniFileConfigService : IConfigServices
    {
        public IniFileConfigService(string filePath)
        {
            FilePath = filePath;
        }

        private string FilePath  { get; set; }

        public string GetValue(string name)
        {
            var kv = File.ReadAllLines(FilePath)
                         .Select(s => s.Split('='))
                         .Select(strs => new { Name = strs[0], Value = strs[1] })
                         .SingleOrDefault(m => m.Name == name);

            return kv?.Value;
        }
    }
}
