using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TL11
{

    public static class ServiceProviderLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
}
