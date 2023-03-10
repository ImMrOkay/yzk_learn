using ConfigServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IniFileConfigServiceExtensions
    {
        public static void AddIniFileConfigService(this IServiceCollection services, string name)
        {
            services.AddScoped(typeof(IConfigServices), s => new IniFileConfigService("mail.ini"));
        }
    }
}
