using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SnippetSpeed.Tests.TypesForRegistryOfTypesTest;
using System.Reflection;
using System;
using System.Linq;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class RegisterOfTypesTests
    {
        [ClassInitialize]
        public static void BeforeAll(TestContext ctx)
        {
            SetEntryAssembly(Assembly.GetExecutingAssembly());
        }

        [TestMethod]
        public void ShouldHaveAnInstanciatedObjectAssociatedWithSpeedBase1()
        {
            RegisterOfTypes.DictoraryOfTypes["(2) SpeedBase1"].Should().BeOfType<SpeedBase1>();
        }

        [TestMethod]
        public void ShouldNotContainKeyWithAbstractSpeedBaseInIt()
        {
            RegisterOfTypes.DictoraryOfTypes.Keys.FirstOrDefault(x => x.Contains("AbstractSpeedBase1")).Should().BeNull();
        }

        [TestMethod]
        public void ShouldHaveAnInstanciatedObjectAssociatedWithSpeedTest2()
        {
            RegisterOfTypes.DictoraryOfTypes["(0) SpeedTest2"].Should().BeOfType<SpeedTest2>();
        }

        [TestMethod]
        public void ShouldHaveAnInstanciatedObjectAssociatedWithASpeedTest3()
        {
            RegisterOfTypes.DictoraryOfTypes["(1) ASpeedTest3"].Should().BeOfType<ASpeedTest3>();
        }


        public static void SetEntryAssembly(Assembly assembly)
        {
            //http://stackoverflow.com/a/21888521/2740086
            var manager = new AppDomainManager();
            var entryAssemblyfield = manager.GetType().GetField("m_entryAssembly", BindingFlags.Instance | BindingFlags.NonPublic);
            entryAssemblyfield.SetValue(manager, assembly);

            var domain = AppDomain.CurrentDomain;
            var domainManagerField = domain.GetType().GetField("_domainManager", BindingFlags.Instance | BindingFlags.NonPublic);
            domainManagerField.SetValue(domain, manager);
        }
    }
}
