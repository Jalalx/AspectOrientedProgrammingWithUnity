
using System;
namespace AspectOrientedProgrammingWithUnity.Tests
{
    public class PersonViewModel : BaseViewModel
    {
        public string FullName { get; [Notify]set; }
        public DateTime Birthday { get; [Notify(Order = 1)][Notify(PropertyName = "Age", Order = 2)]set; }
        public int Age { get { return DateTime.Now.Year - Birthday.Year; } }
    }
}
