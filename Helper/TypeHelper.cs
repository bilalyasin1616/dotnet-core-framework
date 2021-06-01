using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Helper
{
    public static class TypeHelper
    {
        public static List<TInterface> CreateObjects<TInterface>()
        {
            return Assembly.GetExecutingAssembly().ExportedTypes.Where(c =>
                typeof(TInterface).IsAssignableFrom(c) && !c.IsInterface && !c.IsAbstract)
                .Select(Activator.CreateInstance).Cast<TInterface>().ToList();
        }

        public static List<TInterface> CreateObjects<TAssembly, TInterface>()
        {
            return typeof(TAssembly).Assembly.ExportedTypes.Where(c =>
                typeof(TInterface).IsAssignableFrom(c) && !c.IsInterface && !c.IsAbstract)
                .Select(Activator.CreateInstance).Cast<TInterface>().ToList();
        }

        public static List<Type> GetAssignableTypesOfInterface(Type typeOfInterface)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).Where(t =>
                typeOfInterface.IsAssignableFrom(t) && t.IsClass).ToList();
        }

        public static List<Type> GetInterfaceTypesFromNamespace(string assembly, string ns)
        {
            return Assembly.Load(assembly).GetTypes().Where(t => t.Namespace == ns).ToList();
        }
    }
}
