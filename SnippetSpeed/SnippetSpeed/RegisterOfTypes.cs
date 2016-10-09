using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace SnippetSpeed
{
    internal static class RegisterOfTypes
    {
        private static Dictionary<string, SnippetSpeedBase> dictoraryOfTypes = new Dictionary<string, SnippetSpeedBase>();

        static RegisterOfTypes()
        {
            var temporaryDictionaryOfTypes = CreateDictoinaryOfSnippetSpeedClassesFromCallingAssembly();

            AddProperlyNamedAndOrderedItemsToRegistryOfTypes(temporaryDictionaryOfTypes);
        }

        private static void AddProperlyNamedAndOrderedItemsToRegistryOfTypes(Dictionary<string, SnippetSpeedBase> temporaryDictionaryOfTypes)
        {
            var tempList = temporaryDictionaryOfTypes.OrderBy(x => x.Value.TypeOfTest).ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                var element = tempList.ElementAt(i);

                var newName = $"({i}) {element.Key}";
                dictoraryOfTypes.Add(newName, element.Value);
            }
        }

        private static Dictionary<string, SnippetSpeedBase> CreateDictoinaryOfSnippetSpeedClassesFromCallingAssembly()
        {
            var assembly = Assembly.GetEntryAssembly();

            var types = assembly.GetTypes();

            var temporaryDictionaryOfTypes = new Dictionary<string, SnippetSpeedBase>();

            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(SnippetSpeedBase)) && !type.IsAbstract)
                {
                    temporaryDictionaryOfTypes.Add(type.Name, (SnippetSpeedBase)Activator.CreateInstance(type));
                }
            }

            return temporaryDictionaryOfTypes;
        }

        public static Dictionary<string, SnippetSpeedBase> DictoraryOfTypes
        {
            get
            {
                return dictoraryOfTypes;
            }
        }
    }
}
