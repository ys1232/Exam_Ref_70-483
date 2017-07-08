using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Exam_Ref_70_483
{
    class C2_Listing79_81  // use finalizer to explicitely release the resource
    {
        // ??? even though we called GC to run, no idea why finalizer is not called ???
        // the DeleteFile will still fail

        class FileHandler
        {
            StreamWriter stream;
            string FilePath;

            public FileHandler(string FilePath)
            {
                this.FilePath = FilePath;
                stream = File.CreateText(FilePath);
                stream.Write("some data");
            }

            public void DeleteFile()
            {
                //stream.Close();
                File.Delete(FilePath);
            }

            ~FileHandler()  // destructor, mave have same name as the class
            {
                Console.WriteLine("the destructor is executing");
                stream.Close();
            }
        }

        public static void Test_finalizer()
        {
            FileHandler Handler = new FileHandler("temp.dat");

            GC.Collect();   // ??? finalizer is not called ???
            GC.WaitForPendingFinalizers();

            Handler.DeleteFile();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }


    class C2_Listing82_83
    {
        // to use IDisposable interface, we need to derive from this interface and implement the Dispose method. In your code, call this method explicitely to release resource
        class FileHandler : IDisposable
        {
            StreamWriter stream;
            string FilePath;

            public FileHandler(string FilePath)
            {
                this.FilePath = FilePath;
                stream = File.CreateText(FilePath);
                stream.Write("some data");
            }

            public void DeleteFile()
            {
                //stream.Close();
                File.Delete(FilePath);
            }

            ~FileHandler()  // destructor, mave have same name as the class
            {
                Console.WriteLine("the destructor is executing");
                stream.Close();
            }

            public void Dispose()
            {
                Console.WriteLine("the Dispose is executing");
                stream.Close();
            }
        }

        public static void Test_Dispose()
        {
            FileHandler Handler = new FileHandler("temp.dat");

            Handler.Dispose();

            Handler.DeleteFile();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }

    class C2_Listing84
    {
        // let's review and test this code later... too much unfamiliar points here
        class UnmanagedWrapper : IDisposable
        {
            private IntPtr unmanagedBuffer;
            public FileStream Stream { get; private set; }

            public UnmanagedWrapper()
            {
                CreateBuffer();
                this.Stream = File.Open("temp.dat", FileMode.Create);
            }

            private void CreateBuffer()
            {
                byte[] data = new byte[1024];
                new Random().NextBytes(data);
                unmanagedBuffer = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, unmanagedBuffer, data.Length);
            }

            ~UnmanagedWrapper()
            {
                Dispose(false);
            }

            public void Close()
            {
                Dispose();
            }

            public void Dispose()
            {
                Dispose(true);
                System.GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                Marshal.FreeHGlobal(unmanagedBuffer);
                if (disposing)
                {
                    if (Stream != null)
                        Stream.Close();
                }
            }
        }
    }

    /** using Statement: provides a convenient syntax that ensures the correct use of IDisposable objects.
     * All such types must implement the IDisposable interface.
     * when you use an IDisposable object, you should declare and instantiate it in a using statement. 
     * The using statement calls the Dispose method on the object in the correct way.
     * Within the using block, the object is read-only and cannot be modified or reassigned.
     * The using statement ensures that Dispose is called even if an exception occurs while you are calling methods on the object. 
     * You can achieve the same result by putting the object inside a try block and then calling Dispose in a finally block; 
     * in fact, this is how the using statement is translated by the compiler. 
     * */
    class C2_Listing85
    {
        // to test Listing85, we need to implement LoadLargeList()
        static WeakReference data;
        public static void Run()
        {
            object result = GetData();
            result = GetData();
        }

        private static object GetData()
        {
            if (data == null)
                data = new WeakReference(LoadLargeList());
            if (data.Target == null)
                data.Target = LoadLargeList();
            return data.Target;
        }

        private static object LoadLargeList()
        {
            throw new NotImplementedException();
        }
    }

    /** object 2.7
    *String Class: Represents text as a sequence of UTF-16 code units.
    * 
    * */
    class C2_Listing86_88
    {
        // when you need to frenquently manipulate a string, use StringBuilder, which create a string buffer internally to improve performance.
        public static void Test_String_Builder()
        {
            StringBuilder sb = new StringBuilder(string.Empty);

            for (int i = 0; i < 100; i++)
                sb.Append("X");
            Console.WriteLine("The value is:{0}",sb);
            Console.ReadLine();
        }
    }


    class C2_Listing89_90
    {
        public static void Test_StringWriter_StringReader()
        {
            // stringWriter
            var stringWriter = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(stringWriter))   // the method XmlWriter.Create() accept TextWriter to create XmlWriter. 
            {                                                           // We use  stringWriter, a wrapper of string, to play the role
                writer.WriteStartElement("book");   // add top element
                writer.WriteElementString("price", "19.95");
                writer.WriteElementString("TimeZone", "+8");
                writer.WriteEndElement();
                writer.Flush();     // write the in-memory string back to stringWriter
            }

            string xml = stringWriter.ToString();
            Console.WriteLine("The stringWriter xml is:{0}",xml);

            //stringReader
            var stringReader = new StringReader(xml);
            using (XmlReader reader = XmlReader.Create(stringReader))   // we expect TextReader as argument, we use stringReader to play the rule
            {
                reader.ReadToFollowing("price");    // read the value of price element
                decimal price = decimal.Parse(reader.ReadInnerXml(), new CultureInfo("en-US")); // convert to US-doller format
                Console.WriteLine("Price is:{0}",price);
            }
            Console.ReadLine();
        }
    }


    class C2_Listing91_94
    {
        public static void Test_Search_For_String()
        {
            string value = "My Sample Value";
            int indexOfp = value.IndexOf('p');  // 6
            int lastIndexOfe = value.LastIndexOf('e');  // 14
            Console.WriteLine("indexOfp:{0}, lastIndexOfe:{1}", indexOfp, lastIndexOfe);

            value = "<mycustominput>";
            if (value.StartsWith("<")) { Console.WriteLine("startsWith \"<\""); }   // true
            if (value.EndsWith(">")) { Console.WriteLine("EndsWith \">\""); }   // true
            if (value.EndsWith("}")) { Console.WriteLine("EndsWith \"}\""); }   // false

            value = "My Sample Value";
            string substring = value.Substring(3, 6);

            // using regExp is much more complicated than this example
            string pattern = "(Mr\\.? | Mrs\\.? | Miss |Ms\\.?)";
            string[] names = { "Mr. Henry Hunt", "Ms. Sara Samuels", "Abraham Adams", "Ms. Nicole Norris" };

            foreach (string name in names)
                Console.WriteLine(Regex.Replace(name, pattern, String.Empty));  // remove Mr\Mrs\Miss\Ms from the string

            Console.ReadLine();
        }
    }

    class C2_Listing95
    {
        public static void Test_enumerating_string()
        {
            string value = "My Custom Value";
            foreach (char c in value)
                Console.WriteLine(c);

            foreach (string word in "My sentence separated by spaces".Split(' '))   // the split method will return a string array
                Console.WriteLine(word);

            Console.ReadLine();
        }
    }

    class C2_Listing96
    {
        class Person    // we override the ToString method for Person class to return more meaningful result
        {
            public Person(string firstName, string lastName)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                return FirstName + " " + LastName;
            }
        }

        public static void Test_Override_ToString_Method()
        {
            Person p = new Person("Yuan", "Shen");
            Console.WriteLine(p.ToString());
            Console.ReadLine();
        }
    }

    class C2_Listing97_98
    {
        public static void Test_Display_Formatted_Strings()
        {
            double cost = 1234.56;
            Console.WriteLine(cost.ToString("C", new System.Globalization.CultureInfo("en-US"))); // ToString(string Format, IFormatProvider provider). here C means Currency

            DateTime d = new DateTime(2013, 4, 22);
            CultureInfo provider = new CultureInfo("en-US");
            Console.WriteLine(d.ToString("d", provider));
            Console.WriteLine(d.ToString("D", provider));
            Console.WriteLine(d.ToString("M", provider));

            Console.ReadLine();
        }
    }

    class C2_Listing99
    {
        class Person
        {
            public Person(string firstName, string lastName)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string ToString(string format)   // added a custom format method
            {
                if (string.IsNullOrWhiteSpace(format) || format == "G") format = "FL";

                format = format.Trim().ToUpperInvariant();

                switch (format)
                {
                    case "FL":
                        return FirstName + " " + LastName;
                    case "LF":
                        return LastName + " " + FirstName;
                    case "FSL":
                        return FirstName + ", " + LastName;
                    case "LSF":
                        return LastName + ", " + FirstName;
                    default:
                        throw new FormatException(String.Format("The '{0}' format string is not supported.", format));
                }
            }

        }

        public static void Test_Custom_Format_Method()
        {
            Person Stu_1 = new Person("Yuan", "Shen");
            Console.WriteLine("The output is:{0}",Stu_1.ToString("LF"));
            Console.WriteLine("The output is:{0}", Stu_1.ToString("FSL"));
            Console.WriteLine("The output is:{0}", Stu_1.ToString("FFF"));
            Console.ReadLine();
        }
    }

    class C2_Listing100
    {
        public static void Test_Create_Composite_String_Format()
        {
            int a = 1;
            int b = 2;
            string result = string.Format("a: {0}, b:{1}", a, b);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
