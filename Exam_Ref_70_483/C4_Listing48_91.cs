using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class C4_Listing55
    {

    }
    class C4_Listing56
    {

    }
    class C4_Listing57
    {

    }
    class C4_Listing58
    {

    }
    class C4_Listing59
    {

    }
    class C4_Listing60
    {

    }
    class C4_Listing61
    {

    }
    class C4_Listing62
    {

    }
    class C4_Listing63
    {

    }
    class C4_Listing64
    {

    }
    class C4_Listing65
    {

    }
    class C4_Listing66
    {

    }
    class C4_Listing67
    {

    }
    class C4_Listing68
    {

    }
    class C4_Listing69
    {

    }
    class C4_Listing70
    {

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
