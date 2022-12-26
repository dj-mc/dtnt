using System;
using System.Linq;
using System.Reflection;

#nullable disable

namespace DTNT
{
    class Program
    {
        static void Foo()
        {
            var subtotal = 20;
            var sales_tax = subtotal + (subtotal * 0.09);
            var total_price = subtotal + sales_tax;

            Console.WriteLine("Your total is {0}\n", total_price);
            Console.WriteLine("Today: {0:D}, Temp: {1} degrees F\n", DateTime.Today, 25);

            int dec = 2_000_000;
            int bin = 0b_0001_1110_1000_0100_1000_0000;
            int hex = 0x_001E_8480;
            Console.WriteLine("{0}\n{1}\n{2}", dec, bin, hex);
            Console.WriteLine(dec == bin && dec == hex); // True

            double a = 0.1d;
            double b = 0.2d;
            if (a + b == 0.3d) // 0.30000000000000004
            {
                Console.WriteLine($"True: {a + b}");
            }
            else
            {
                Console.WriteLine($"False: {a + b}");
            }

            decimal x = 0.1m;
            decimal y = 0.2m;
            if (x + y == 0.3m) // 0.3
            {
                Console.WriteLine($"True: {x + y}");
            }
            else
            {
                Console.WriteLine($"False: {x + y}");
            }

            Console.WriteLine($"Default value of int: {default(int)}");
            Console.WriteLine($"Default value of string: {default(string)}");
            Console.WriteLine($"Default value of bool: {default(bool)}");
            Console.WriteLine($"Default value of DateTime: {default(DateTime)}");

            string[] names = new string[4];
            names[0] = "Alice";
            names[1] = "Robert";
            names[2] = "Chelsea";
            names[3] = "Daniel";

            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{names[i]}");
            }
        }
        static void Numbers()
        {
            Console.WriteLine($"\n\nint: {sizeof(int)}-byte"); // 4-byte, 16-bit
            Console.WriteLine($"int range:\n{int.MinValue:N0}\nto\n{int.MaxValue:N0}");

            Console.WriteLine($"\n\nfloat: {sizeof(float)}-byte"); // 4-byte, 16-bit
            Console.WriteLine($"float range:\n{float.MinValue:N0}\nto\n{float.MaxValue:N0}");

            Console.WriteLine($"\n\ndouble: {sizeof(double)}-byte"); // 8-byte, 32-bit
            Console.WriteLine($"double range:\n{double.MinValue:N0}\nto\n{double.MaxValue:N0}");

            Console.WriteLine($"\n\ndecimal: {sizeof(decimal)}-byte"); // 16-byte, 64-bit
            Console.WriteLine($"decimal range:\n{decimal.MinValue:N0}\nto\n{decimal.MaxValue:N0}");
        }
        static void Paths()
        {
            var app_path = System.AppContext.BaseDirectory;
            var domain_path = System.AppDomain.CurrentDomain.BaseDirectory;
            var io_path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var io_dir_path = System.IO.Directory.GetCurrentDirectory();
            var env_path = System.Environment.CurrentDirectory;
            // Disable the escape of special symbols like \t with @
            var string_path = @"C:\tv_and_movies\terminator\";

            Console.WriteLine("app_path: {0}", app_path);
            Console.WriteLine("domain_path: {0}", domain_path);
            Console.WriteLine("io_path: {0}", io_path);
            Console.WriteLine("io_dir_path: {0}", io_dir_path);
            Console.WriteLine("env_path: {0}", env_path);
            Console.WriteLine("string_path: {0}", string_path);
        }
        static async System.Threading.Tasks.Task Go()
        {
            System.Data.DataSet data_set = new System.Data.DataSet("asdf");
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            try
            {
                System.Net.Http.HttpResponseMessage res = await client.GetAsync("http://microsoft.com/");
                res.EnsureSuccessStatusCode();
                string res_body = await res.Content.ReadAsStringAsync();
                Console.WriteLine(res_body);
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(data_set);
        }
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Foo();
            Numbers();
            Paths();

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

            await Go();
        }
    }
}
