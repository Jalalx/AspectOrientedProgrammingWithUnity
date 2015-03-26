using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AspectOrientedProgrammingWithUnity.Tests
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public class NotifyAttribute : HandlerAttribute
    {
        private readonly ICallHandler handler;
        public string PropertyName { get; set; }
        public NotifyAttribute()
        {
        }

        public NotifyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public NotifyAttribute(string propertyName, int order)
        {
            this.PropertyName = propertyName;
            this.Order = order;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            if (string.IsNullOrWhiteSpace(PropertyName))
                return new NotifyPropertyChangedCallHandler();
            
            return new NotifyPropertyChangedCallHandler(PropertyName, Order);
        }
    }
}
