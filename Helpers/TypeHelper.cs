using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Helpers
{
    public static class TypeHelper
    {
        public static List<TInterface> CreateObjects<TInterface>()
        {
            return Assembly.GetExecutingAssembly().ExportedTypes.Where(c =>
                typeof(TInterface).IsAssignableFrom(c) && c.IsClass)
                .Select(Activator.CreateInstance).Cast<TInterface>().ToList();
        }

        public static List<Type> GetAssignableTypes(Type type)
        {
            var assignableTypes = new List<Type>();
            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList().ForEach(assemblyName =>
                assignableTypes.AddRange(Assembly.Load(assemblyName).GetTypes().Where(c => type.IsAssignableFrom(c) && c.IsClass && !c.IsAbstract).ToList())
            );
            assignableTypes.AddRange(Assembly.GetEntryAssembly().GetTypes().Where(c => type.IsAssignableFrom(c) && c.IsClass).ToList());
            return assignableTypes;
        }
    }
}
