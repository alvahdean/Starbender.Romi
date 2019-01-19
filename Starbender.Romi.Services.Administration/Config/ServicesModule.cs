using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Module = Autofac.Module;

namespace Starbender.Romi.Services.Administration
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && (x.Name.EndsWith("Provider") || x.Name.EndsWith("Service") || x.Name.EndsWith("Factory")))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
