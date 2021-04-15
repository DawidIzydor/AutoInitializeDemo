using System;
using Abstractions;

namespace SomeLibrary
{
    [AutoInitialize("Init2")]
    public class SomeOtherClass : SomeClass
    {
        public override void Init()
        {
            Console.WriteLine("This message is inside the 'SomeOtherClass' inherited initialization method, the next message will be from parent initialization for this object.");
            base.Init();
        }

        public void Init2()
        {
            Console.WriteLine("This message is inside the 'SomeOtherClass' second initialization method");
        }
    }
}