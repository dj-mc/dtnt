using System;
using System.Linq;
using System.Reflection;

namespace DTNT
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Data.DataSet data_set;
            System.Net.Http.HttpClient client;

            Console.WriteLine("Hello, C#!");

            var subtotal = 20;
            var sales_tax = subtotal + (subtotal * 0.09);
            var total_price = subtotal + sales_tax;

            Console.WriteLine("Your total is {0}", total_price);
            Console.WriteLine("Today is {0:D}, and the temperature is {1} degrees F", DateTime.Today, 25);

            // Get array of referenced assembly names
            foreach (var referenced in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                Console.WriteLine("\n---\nAssembly {0}", referenced.Name);

                // Load assembly of currently executed code
                var asm = Assembly.Load(new AssemblyName(referenced.FullName));
                Console.WriteLine("{0} types", asm.DefinedTypes.Count());

                int method_count = 0;
                foreach (var asm_type in asm.DefinedTypes)
                {
                    method_count += asm_type.GetMethods().Count();
                }
                Console.WriteLine("{0} methods", method_count);
            }
        }
    }
}
