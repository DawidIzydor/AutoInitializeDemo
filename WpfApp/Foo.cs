using SomeReferencedLibrary;

namespace WpfApp
{
    public class Foo
    {
        private SomeReferencedClass _src;

        public Foo()
        {
            // this is only to make sure the assembly is referenced
             _src = new SomeReferencedClass();
        }
    }
}