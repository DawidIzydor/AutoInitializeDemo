using System;
using Abstractions;

namespace SomeReferencedLibrary
{
    [AutoInitialize("Init")]
    public class SomeReferencedClass
    {
        public void Init()
        {
            Console.WriteLine("SomeReferencedClass was initialized successfully (this message is inside the Init method)");
        }
    }
}
