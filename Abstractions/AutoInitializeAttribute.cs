using System;

namespace Abstractions
{
    [AttributeUsage(validOn: AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AutoInitializeAttribute : Attribute
    {
        public string InitMethodName { get; }

        public AutoInitializeAttribute(string initMethodName)
        {
            InitMethodName = initMethodName;
        }
    }
}