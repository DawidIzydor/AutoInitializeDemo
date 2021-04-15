# AutoInitializeDemo
A demonstration of how to automatically initialize classes using an attribute.

# Why?
This is an example of how to prepare to modify the application workflow without modifying the application.

# Examples
Let's say a music-player has a build-in support for wav fileformats, we want to add support for adding different fileformats in the future without modyfing the app. 
On application startup a `fileformats` directory is scanned and all libraries inside it are loaded, then every class that has a `FileFormat` attribute is added to a registry. 
If a user wants to add support for running a very specific fileformat (ie `xyz` - their custom music format) all they needs to do is create the library that decodes the format and put the library into the `fileformat` directory.
The player can play then songs in `xyz` format - application use-cases are increased without modyfing the app.

TBD

# What is happening in this demo app

This app shows just the basic concept of running methods from other assemblies, an example that modifies the app behaviour might be added in the future.

0. Compile and run the app
1. Click "Execute init from referenced libraries"
2. All referenced libraries will be scaned for classes that are decorated using the `[AutoInitialize]` attibute, only `SomeReferencedLibrary` is refenced because the `SomeReferencedClass` is used inside the `Foo` class in the wpf project so the library reference doesn't get optimized.
3. Message from inside the initialization method from `SomeReferencedClass` is displayed in the console output
4. Click "Load external library and execute init"
5. Select "SomeLibrary.dll"
6. Message from inside the initialization method from `SomeClass` is displayed in the console output
7. Message from inside the initialization method from `SomeOtherClass` is displayed in the console output. `SomeOtherClass` is inheriting from `SomeClass` both the atribute and the Init method. An additional Init2 method is also added but it's completly optional, the inherited one will run even if the AutoInitialization attribute for the Init2 method is removed
