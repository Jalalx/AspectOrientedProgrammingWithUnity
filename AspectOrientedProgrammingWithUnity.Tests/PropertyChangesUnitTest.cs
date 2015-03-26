using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspectOrientedProgrammingWithUnity.Tests
{
    [TestClass]
    public class PropertyChangesUnitTest
    {
        [TestMethod]
        public void CheckPropertyChangedGetsCalledForPropertiesInAttributeWay()
        {
            using (var container = new UnityContainer())
            {
                container.AddNewExtension<Interception>();
                container.RegisterType<PersonViewModel>(
                    new Interceptor<TransparentProxyInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>());

                var propertyChanged = false;
                var propertyNames = new List<string>();
                var viewModel = container.Resolve<PersonViewModel>();

                viewModel.PropertyChanged += (sender, e) =>
                {
                    propertyChanged = true;
                    propertyNames.Add(e.PropertyName);
                };

                viewModel.FullName = "new value!";
                Assert.IsTrue(propertyChanged, "PropertyChanged not called!");
                Assert.IsTrue(propertyNames.Contains("FullName"));
                propertyNames.Clear();

                viewModel.Birthday = new DateTime(1990, 1, 1);
                Assert.IsTrue(propertyChanged, "PropertyChanged not called!");
                Assert.IsTrue(propertyNames.Contains("Birthday"));
                Assert.IsTrue(propertyNames.Contains("Age"));
            }
        }
    }
}
