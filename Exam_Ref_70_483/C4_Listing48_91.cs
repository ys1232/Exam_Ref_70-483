using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exam_Ref_70_483
{
    class C4_Listing48_50
    {
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public static void Test_Object_Initializer()
        {
            Person p = new Person { FirstName = "John", LastName = "Doe" };

            var people_Liast = new List<Person> {
                new Person { FirstName = "John", LastName = "Doe" },
                new Person { FirstName = "Jane", LastName = "Doe" }
            };

            foreach (var person in people_Liast)
            {
                Console.WriteLine("The firstName is:{0}, lastName is:{1}", person.FirstName, person.LastName);
            }
            Console.ReadLine();
        }
    }
    class C4_Listing51
    {
        public static void Test_anonymous_and_Lambda()
        {
            Func<int, int> my_anonymous_Delegate = delegate (int x) { return x * 2; };  // use anonymous version
            Console.WriteLine(my_anonymous_Delegate(21));

            Func<int, int> my_Lambda_Delegate = x => x * 2; // use lambda version
            Console.WriteLine(my_Lambda_Delegate(21));

            Console.ReadLine();
        }
    }
    public static class IntExtensions
    {
        // we can add extension method for int, string, and our custom type
        public static int Multiply(this int x, int y)   // extension method must be defined in a top level static class, 
                                                        //cannot be a nested class of C4_Listing52 
        {
            return x * y;
        }
    }
    class  C4_Listing52
    {
        public static void Test_Extension_Method()
        {
            int x = 2;
            Console.WriteLine(x.Multiply(3));   // in this case: this int x is 2, int y is 3
            Console.ReadLine();
        }
    }
    class C4_Listing53
    {
        public static void Test_Anonymoust_Type()
        {
            var person = new
            { FirstName = "John", LastName = "Doe" };

            Console.WriteLine(person.GetType().Name);  // Displays "<>f__AnonymousType0`2"
            Console.WriteLine(person.FirstName + " " + person.LastName);
            Console.ReadLine();
        }
    }
    class C4_Listing54
    {
        public static void Test_LINQ_select_Query()
        {
            int[] data = { 1, 2, 5, 8, 11 };

            var result = from d in data
                         where d % 2 == 0
                         select d;

            foreach (int i in result)
                Console.WriteLine(i);
            Console.ReadLine();
        }
    }
    class C4_Listing55_62
    {
        public static void Test_Select_Operator()
        {
            int[] data = { 1, 2, 5, 8, 11 };
            var result = from d in data select d;
            Console.WriteLine(String.Join(", ", result));
            Console.ReadLine();
        }

        public static void Test_Where_Opertator()
        {
            int[] data = { 1, 2, 5, 8, 11 };
            var result = from d in data
                         where d > 5
                         select d;
            Console.WriteLine(string.Join(", ", result));
            Console.ReadLine();
        }

        public static void Test_Orderby_Opertator()
        {
            int[] data = { 1, 2, 5, 8, 11 };
            var result = from d in data
                         where d > 5
                         orderby d descending
                         select d;
            Console.WriteLine(string.Join(", ", result));
            Console.ReadLine();
        }
        public static void Test_Multiple_From()
        {
            int[] data1 = { 1, 2, 5 };
            int[] data2 = { 2, 4, 6 };

            var result = from d1 in data1
                         from d2 in data2
                         select d1 * d2;

            Console.WriteLine(string.Join(", ", result));
            Console.ReadLine();
        }
    }

    //class C4_Listing63
    //{

    //}
    class C4_Listing64_69
    {
        public static string xml = @"<?xml version = ""1.0"" encoding = ""utf-8""?>
                            <people>
                              <person firstName = ""john"" lastName = ""doe"">
                                <contactdetails>
                                   <emailaddress>john@unknown.com</emailaddress>
                                </contactdetails>
                               </person>
                              <person firstName = ""jane"" lastName = ""doe"">
                                <contactdetails>
                                   <emailaddress>jane@unknown.com</emailaddress>
                                   <phonenumber>001122334455</phonenumber>
                                </contactdetails>
                               </person>
                             </people>";

        public static void Test_Query_XML()
        {
            XDocument doc = XDocument.Parse(xml);
            IEnumerable<string> personName = from p in doc.Descendants("person")  // get a collection of person element from the xml
                                             select (string)p.Attribute("firstName")    // get the value of firstName and lastName attribute of person element
                                             + " " + (string)p.Attribute("lastName");
            foreach (string s in personName)
                Console.WriteLine(s);
            Console.ReadLine();
        }

        public static void Test_XML_Where_and_OrderBy()
        {
            XDocument doc = XDocument.Parse(xml);
            IEnumerable<string> personName = from p in doc.Descendants("person")
                                             where p.Descendants("phonenumber").Any()  // select the person node with phonenumber element
                                             let name = (string)p.Attribute("firstName") + " " +
                                             (string)p.Attribute("lastName")
                                             orderby name
                                             select name;  // get all firstName and lastName for such elements

            foreach (string s in personName)
                Console.WriteLine(s);
            Console.ReadLine();
        }

        public static void Test_CreateXML_WithXElement()
        {
            XElement root = new XElement("Root", new List<XElement>  // add three elements and one attribute to the roor element
            {
                new XElement("Child1"),
                new XElement("Child2"),
                new XElement("Child3")
            }, new XAttribute("MyAttribute", 42));

            root.Save("test.xml"); // this xml file is saved in the bin/Debug/ folder under the project
        }

        public static void Test_Update_XML()  // trying to figure out how to update attribute value in xml
        {
            XElement root = XElement.Parse(xml);

            foreach (XElement p in root.Descendants("person"))
            {
                string name = (string)p.Attribute("firstName") + (string)p.Attribute("lastName");
                p.SetAttributeValue("lastName",123); // add IsMale attribute to John
                XElement contactDetails = p.Element("contactdetails");  // if there is no PhoneNumber element under ContactDetails,add PhoneNumber
                if (!contactDetails.Descendants("phonenumber").Any())
                    contactDetails.Add(new XElement("phonenumber", "001122334455"));
            }

            Console.WriteLine(root.ToString());
            Console.ReadLine();
        }

        public static void Test_Transforming_XML()
        {
            XElement root = XElement.Parse(xml);

            XElement newTree = new XElement("people", from p in root.Descendants("person")
                                                      let name = (string)p.Attribute("firstName") + (string)p.Attribute("lastName")
                                                      let contactDetails = p.Element("contactdetails")
                                                      select new XElement("person",
                                                      new XAttribute("IsMale", name.Contains("John")),
                                                      p.Attributes(),
                                                      new XElement("contactdetails",
                                                           contactDetails.Element("emailaddress"),
                                                           contactDetails.Element("phonenumber")
                                                           ?? new XElement("phonenumber", "1122334455")
                                                      )));

            Console.WriteLine(newTree.ToString());
            Console.ReadLine();
        }
    }

    class C4_Listing70
    {
        [Serializable]
        public class Peson
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }


    }
    class C4_Listing71
    {

    }
    class C4_Listing72
    {

    }
    class C4_Listing73
    {

    }
    class C4_Listing74
    {

    }
    class C4_Listing75
    {

    }
    class C4_Listing76
    {

    }
    class C4_Listing77
    {

    }
    class C4_Listing78
    {

    }
    class C4_Listing79
    {

    }
    class C4_Listing80
    {

    }
    class C4_Listing81
    {

    }
    class C4_Listing82
    {

    }
    class C4_Listing83
    {

    }
    class C4_Listing84
    {

    }
    class C4_Listing85
    {

    }
    class C4_Listing86
    {

    }
    class C4_Listing87
    {

    }
    class C4_Listing88
    {

    }
    class C4_Listing89
    {

    }
    class C4_Listing90
    {

    }
    class C4_Listing91
    {

    }
}
