using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    public class LayeredConfigReader : IConfigReader
    {
        private readonly IEnumerable<IConfigServices> services;

        public LayeredConfigReader(IEnumerable<IConfigServices> services)
        {
            this.services = services;
        }

        public string GetValue(string name)
        {
            string value = null;
            foreach (var service in services)
            {
                string newValue = service.GetValue(name);
                if (newValue != null)
                {
                    value = newValue;//最后一个部位null的值，就是最终值
                }
            }
            return value;
        }
    }
}
