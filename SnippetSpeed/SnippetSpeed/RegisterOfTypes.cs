using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace SnippetSpeed
{
    internal static class RegisterOfTypes
    {
        private static Dictionary<string, SpeedTestBase> dictoraryOfTypes = new Dictionary<string, SpeedTestBase>();

        static RegisterOfTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if(type.IsSubclassOf(typeof(SpeedTestBase)) && !type.IsAbstract)
                {
                    var countOfDictionary = dictoraryOfTypes.Count();
                    var typeName = string.Format("({0}) {1}", countOfDictionary, type.Name);

                    dictoraryOfTypes.Add(typeName, (SpeedTestBase)Activator.CreateInstance(type));
                }
            }

            dictoraryOfTypes.OrderBy(x => x.Value.TypeOfTest);
        }

        public static Dictionary<string, SpeedTestBase> DictoraryOfTypes
        {
            get
            {
                return dictoraryOfTypes;
            }
        }


    }
}
