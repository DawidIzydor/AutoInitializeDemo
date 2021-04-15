using System;
using Abstractions;

namespace SomeLibrary
{
    [AutoInitialize("Init")]
    public class SomeClass
    {
        public virtual void Init()
        {
            Console.WriteLine("This message is inside the 'SomeClass' initialization method");
        }
    }
}