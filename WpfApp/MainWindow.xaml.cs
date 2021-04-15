using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Abstractions;
using Microsoft.Win32;
using SomeLibrary;

namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : IDisposable
    {
        private readonly StringWriterExt _stringWriterExt;

        public MainWindow()
        {
            InitializeComponent();

            // this is only for Console Output logging
            _stringWriterExt = new StringWriterExt(true);
            Console.SetOut(_stringWriterExt);
            Console.SetError(_stringWriterExt);
            _stringWriterExt.Flushed += (_, _) =>
            {
                ConsoleOutput = _stringWriterExt.ToString();
                ConsoleOutTextBlock.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                OutupScrollViewer.ScrollToBottom();
            };
            Console.WriteLine("Console output initialized.");
        }

        public string ConsoleOutput { get; private set; }

        public void Dispose()
        {
            _stringWriterExt?.Dispose();
        }

        private void ReferencedLibrariesLoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReferencedLibrariesLoadButton.IsEnabled = false;
                InitReferencedLibraries();
            }
            finally
            {
                ReferencedLibrariesLoadButton.IsEnabled = true;
            }
        }

        private static void InitReferencedLibraries()
        {
            // get all referenced assemblies for current object assembly
            Console.WriteLine("Initializing from all referenced assemblies");
            var assembilies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Select(assemblyName => Assembly.Load(assemblyName));


            foreach (var assembly in assembilies)
            {
                InitTypesFromAssembly(assembly);
            }


            Console.WriteLine("Done initializing referenced libraries");
        }

        private static void InitTypesFromAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var autoInitializeAttributes = type.GetCustomAttributes<AutoInitializeAttribute>().ToArray();
                if (autoInitializeAttributes.Any() == false)
                {
                    continue;
                }

                Console.WriteLine($"Found type with AutoInitialize attribute in assembly: {assembly.FullName}{Environment.NewLine} Type: {type.FullName}");
                var obj = Activator.CreateInstance(type);

                foreach (var autoInitializeAttribute in autoInitializeAttributes)
                {
                    var initMethod = type.GetMethod(autoInitializeAttribute.InitMethodName);
                    initMethod.Invoke(obj, Array.Empty<object>());
                }

                Console.WriteLine($"{type.FullName} initialized!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                DefaultExt = "*.dll",
                AddExtension = true
            };
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            var assembly = Assembly.LoadFile(openFileDialog.FileName);
            InitTypesFromAssembly(assembly);
        }
    }
}