using System.Reflection;

using Furion;

namespace FunionBlazor.Web.Entry
{
    public class SingleFilePublish : ISingleFilePublish
    {
        public Assembly[] IncludeAssemblies()
        {
            return Array.Empty<Assembly>();
        }

        public string[] IncludeAssemblyNames()
        {
            return new[]
            {
            "FunionBlazor.Application",
            "FunionBlazor.Core",
            "FunionBlazor.EntityFramework.Core",
            "FunionBlazor.Web.Core"
        };
        }
    }
}