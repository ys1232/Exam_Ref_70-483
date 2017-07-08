using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Ref_70_483
{
    //[FlagsAttribute]    // This attribute can only be applied to enumerations.Indicates that an enumeration can be treated as a bit field; that is, a set of flags
                        // to let the enum makes sense, you need to define enumeration constants in powers of two, that is, 1, 2, 4, 8, and so on. And use bitwise operation (AND, OR, EXCLUSIVE OR)
    enum Days
    {   // here you can also use decimal number 0,1,2,4,8,16,32,64
        None = 0x0,
        Sudany = 0X1,
        Monday = 0X2,
        Tuesday = 0X4,
        Wednesday = 0X8,
        Thursday = 0X10,
        Friday = 0X20,
        Saturday = 0X40
    }

    class C2_Listing1
    {
        public static void Test_Enum()
        {
            Days readingDays = Days.Monday | Days.Saturday;
            Console.WriteLine("the readingDays are {0}", readingDays);   // Monday, Saturday     if you don't use FlagsAttribute, the value here will be 66

            readingDays = Days.Monday;
            Console.WriteLine("the readingDays is {0}", readingDays);

            if (readingDays == Days.Monday)  // when doing the comparsion, you have to use the enum name, to use the value, you need to do the data type conversion
                Console.WriteLine("it is Monday");

            Console.WriteLine("The string value of the enum value is: {0}", readingDays.ToString()); //Monday    when using a value in WriteLine, the value will be implicitly converted to string. In this case, the value will be Monday
                                                                                                     // but for the undefined value, the CLR will do the bitwise combination. That is the whole point of using FlagsAttribute...

            Console.WriteLine("The value of readingDays is:{0}", (byte)readingDays);   //66
            Console.ReadLine();
        }

    }

    class C2_Listing2
    {
        public struct Point
        {
            public int x, y;

            public Point(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }
    }

    class C2_Listing3
    {
        public static void Test_Call_method()
        {
            Console.WriteLine("Calling a method");
            Console.ReadLine();
        }
    }

    class C2_Listing4
    {
        class Calculator
        {
            public static int Add(int x, int y)
            {
                return x + y;
            }
        }

        public static void Test_Creating_Method()
        {
            Console.WriteLine("1+2 = {0}", Calculator.Add(1,2));
            Console.ReadLine();
        }
    }

    class C2_Listing5
    {
        public static void Test_Passing_A_customer_to_Method()
        {
            Console.WriteLine("this example is not a complete code, there is truly nothing we can do here...");
        }
        
    }

    class C2_Listing6
    {
        public static void Test_Passing_An_Address_to_Method()
        {
            Console.WriteLine("this example is not a complete code, there is truly nothing we can do here...");
        }
    }

    class C2_Listing7
    {
        // we can use named argument to specify which argument we assigned value to. And for the argument without assignment, it will use the default value we defined
        private static void MyMethod(int firstArgument, bool secondArgument, string thirdArgument = "default value")
        {
            Console.WriteLine("the secondArgument is:{0}",secondArgument);
            Console.WriteLine("the thirdArgument is:{0}", thirdArgument);
        }

        private static void CallingMethod()
        {
            MyMethod(1, secondArgument: true);   // we will use default value for thirdArgument
            //MyMethod(3, thirdArgument: "value1");   // we will use "value1" for thirdArgument. But since we didn't provide default value for secondArgument, this calling is invalid and will fail
            MyMethod(5, true,"value2");       // we will use "value2" for thirdArgument
        }

        public static void Test_Default_and_Optional_Argument()
        {
            CallingMethod();
            Console.ReadLine();
        }

    }

    class C2_Listing8
    {
        public static void MethodWithoutAnyReturnValue()
        { 
            // Don't return any value to the caller
        }

        public static int MethodWithReturnValue()
        { return 42;}

        public static void Test_returnValue()
        {
            Console.WriteLine("very simple code, not much we can do");
        }
    }

    class C2_Listing9
    {
        public class MyClass
        {
            public string MyInstanceField;

            public void Concatenate(string valueToAppend)
            {
                if (string.IsNullOrWhiteSpace(MyInstanceField))
                    MyInstanceField = valueToAppend;
                else
                    MyInstanceField += ", " + valueToAppend;              
            }
        }

        public static void Test_UsingField()
        {
            MyClass instance = new MyClass();
            instance.Concatenate("Some New Value");
            instance.Concatenate("add more values");
            Console.WriteLine("instance.MyInstanceField:{0}", instance.MyInstanceField);
            Console.ReadLine();
        }
    }

    class C2_Listing10
    {
        //  In this example, we have a class Card and Deck. Deck contains a collection of Card. We need to add a property to access each element using index directly
        class Card
        {
            public Card(int value)
            {
                this.value = value;
            }

            public int value;
        }

        class Deck
        {
            public ICollection<Card> Cards { get; set; }  // the collectio of card

            public Card this[int index]  // now we can use this[index] to access individual card in the collection by index number 
            {
                get { return Cards.ElementAt(index); }  // here we can not use [index] directly(ICollection don't support that. 
            }                                           // that is the reason why we implement it here), we need to use the ElementAt() function. 
                                                        // With this implementation, now you can access index directly
            public Deck()   // we initialize the Cards in the constructor
            {
                Cards = new List<Card>();
            }
        }

        public static void Test_Creating_Collection_Class()
        {
            Card card_1 = new Card(1);  // create three instance
            Card card_2 = new Card(2);
            Card card_3 = new Card(3);

            Deck Deck_1 = new Deck();
            Deck_1.Cards.Add(card_1);  // add three instances to Deck.Cards
            Deck_1.Cards.Add(card_2);
            Deck_1.Cards.Add(card_3);

            Console.WriteLine(Deck_1[1].value.ToString());   // retrieve the card value using index
            Console.WriteLine(Deck_1[2].value.ToString());
            Console.WriteLine(Deck_1[0].value.ToString());
            Console.ReadLine();
        }
    }


    class C2_Listing11
    {
        class Card
        {
            public Card(int value)
            {
                this.value = value;
            }

            int value;
        }

        class Deck
        {
            public int _maximumNumberOfCards { get; set; }

            public List <Card> Cards { get; set; }

            public Deck(int maximumNumberOfCards)
            {
                _maximumNumberOfCards = maximumNumberOfCards;
                Cards = new List<Card>();
            }
        }

        public static void Test_Constructor()
        {
            Deck new_Deck = new Deck(52);
            Console.WriteLine("the maximumNumberOfCards is:{0}",new_Deck._maximumNumberOfCards);
            Console.ReadLine();
        }
    }

    class C2_Listing12
    {
        class ConstrunctorChaining
        {
            public int _p { get; set; }

            public ConstrunctorChaining() : this(3) { } // for the constructor with no argument, we call the constructor with one argument to set the default value to the field _p
            public ConstrunctorChaining(int p)
            { this._p = p; }
        }

        public static void Test_ConstructorChaining()
        {
            ConstrunctorChaining No_Arguments_Constructor = new ConstrunctorChaining();
            ConstrunctorChaining Constructor_with_Arguments = new ConstrunctorChaining(5);

            Console.WriteLine("No_Arguments_Constructor:{0}", No_Arguments_Constructor._p);
            Console.WriteLine("Constructor_with_Arguments:{0}", Constructor_with_Arguments._p);
            Console.ReadLine();
        }
    }

    class C2_Listing13
    {
        // ??? I have no idea how this is going to work 
        struct Nullable<T> where T : struct   // the constraint "where T:struct" means, T must be a value type (struct means value type)
        {                                     // Generic classes encapsulate operations that are not specific to a particular data type. The most common use for generic classes is with collections.
                                              // Operations such as adding and removing items from the collection are performed in basically the same way regardless of the type of data being stored.
                                              // that is the reason we call it generic
                                              // Typically, you create generic classes by starting with an existing concrete class
                                              // A good rule is to apply the constraints. That will prevent unintended use of your class

            private bool hasValue;      // we add a field to indicate null value
            private T value;            // T is the non-generic value type. We wrapped it into our generic struct, and set the orign value type as a private field

            public Nullable(T value)    // in constructor, we initialize struct fields
            {
                this.hasValue = true;  // true means, we provided values for field value, so we have value, it is not null
                this.value = value;
            }

            public bool HasValue { get { return this.hasValue; } }      // the method to check wether we have value

            public T Value   // the accessor for the private field value
            {
                get
                {
                    if (!this.HasValue) throw new ArgumentException();  // if the HasValue is false, which means the value is not initialized, so we throw an ArgumentException
                    return this.value;
                }
            }

            public T GetValueOrDefault()
            {
                return this.value;
            }
        }

        public static void Test_Nullable_Wrapper()
        {

        }
    }

    class C2_Listing14
    {
        class MyClass<T> where T : class, new()
        {
            public MyClass()
            {
                MyProperty = new T();
            }

            T MyProperty { get; set; }
        }
    }

    class C2_Listing15
    {
        public void MyGenericMethod<T>()
        {
            T defaultValue = default(T);    // use default(T) to set default value for generic type 
        }
    }

    // for C2_Listing16, the extension method has to be declared in a top level static class, so we can not put this static class in class C2_Listing16
    public static class MyExtensions   // we can put all extensin method for a class in one top level static class
    {
        public static decimal Discount(this Product product)    // the key word "this" in argument makes this method an extension method
        {                                                       // this method will make change to class field product.Price
            return product.Price * .9M;
        }
    }

    public class Product
    {
        public decimal Price { get; set; }      // we have one field for class product
    }


    class C2_Listing16
    {
        public class Calculator
        {
            public decimal CalculateDiscount(Product p)
            {
                return p.Discount();    // apply the extensio method for the passing class instance
            }
        }

        public static void Test_Extension_Method()
        {
            Product Tissue = new Product();
            Tissue.Price = 3;
            Console.WriteLine("The value is:{0}", new Calculator().CalculateDiscount(Tissue));
            Console.ReadLine();
        }
    }

    class C2_Listing17
    {
        class Base
        {
            public virtual int MyMethod()
            {
                return 42;
            }
        }

        class Derived : Base
        {
            public override int MyMethod()   // override the method MyMethod()
            {   
                // in the override method, you can add completely new code as replacement or reuse the method from base class
                return base.MyMethod() * 2;
                //return 30;
            }

            public int MyMethod_2() // add a new method to derived class
            {
                return 666;
            }
        }

        class Derived_Derived : Derived
        {
            //public override int MyMethod_2()   // you can not override Derived.MyMethod_2(). 
            //{                                  // You cannot override a non-virtual or static method. The overridden base method must be virtual, abstract, or override.

            //}
            
            public override int MyMethod()
            {
                return base.MyMethod();
                //return Derived.MyMethod();   //here you can not use return Derived.MyMethod(), can only use virtual method or abstrct method?
            }
        }

        public static void Test_Class_Derive()
        {
            Console.WriteLine("The return value from Base.MyMethod is:{0}", new Base().MyMethod());
            Console.WriteLine("The return value from Derived.MyMethod is:{0}", new Derived().MyMethod());
            Console.WriteLine("The return value from Derived.MyMethod_2 is:{0}", new Derived().MyMethod_2());
            Console.ReadLine();
        }
    }

    class C2_Listing18
    {
        class Base
        {
            public virtual int MyMethod()
            {
                return 42;
            }
        }

        class Derived : Base
        {
            public sealed override int MyMethod()
            {
                return base.MyMethod() * 2;
            }
        }

        class Derived2 : Derived
        {
            //public override int MyMythod() { return 1; }  // this code will fail, since we can not extend from Derived.MyMethod(), it is sealed
        }

        
    }

    class C2_Listing19
    {
        // Boxing is an implicit conversion of a value type to the type object.
        // When the CLR boxes a value type, it wraps the value inside a System.Object and stores it on the garbage-collected heap.
        // Unboxing extracts the value type from the object. Boxing is implicit; unboxing is explicit.
        public static void Test_Boxing_Unboxing_Integer()
        {
            int i = 42;
            Console.WriteLine("i is {0}", i.GetType());
            object o = i;  // we boxing interger into object o
            int x = (int)o;  // we unboxing object o to value type
            Console.WriteLine("i is {0}", i.GetType());
            Console.ReadLine();
        }
    }

    class C2_Listing20_21
    {
        public static void Test_Implicit_Conversion_Value_Type()
        {
            int i = 42;
            Console.WriteLine("i :{0} {1}", i, i.GetType());
            double d = i;
            Console.WriteLine("d :{0} {1}", d, d.GetType());
            Console.ReadLine();
        }

        public static void Test_Implicit_Conversion_Reference_Type()
        {
            HttpClient client = new HttpClient();
            Console.WriteLine("The object type is:{0}", client.GetType());
            object o = client;
            Console.WriteLine("The object type is:{0}", o.GetType());  // why the type is still HttpClient?
            IDisposable d = client;   // no idea why we need IDisposable?
            Console.ReadLine();
        }
    }

    class C2_Listing22_23
    {
        public static void Test_Explicit_Conversion_Value_Type()
        {
            // cast a double to an int
            double x = 1234.7;
            int a;
            a = (int)x;  
            Console.WriteLine("The value is:{0}",a);   // the console will show 1234, the decimal part is gone
            Console.ReadLine();
        }

        public static void Test_Explicit_Conversion_Reference_Type()
        {
            Object stream = new MemoryStream();  // if you use new Object() here, it won't work. 
            Console.WriteLine("The object type is:{0}", stream.GetType());
            MemoryStream memoryStream = (MemoryStream)stream;
            Console.WriteLine("The object type is:{0}", memoryStream.GetType());
            Console.ReadLine();
        }
    }

    class C2_Listing24_25
    {
        class Money
        {
            public Money(decimal amount)    // constructor
            {
                Amount = amount;
            }
            
            public decimal Amount { get; set; } // property

            // To overload an operator on a custom class requires creating a method on the class with the correct signature. 
            // The method must be named "operator X" where X is the name or symbol of the operator being overloaded.

            //The implicit keyword is used to declare an implicit user-defined type conversion operator. 
            //Use it to enable implicit conversions between a user-defined type and another type, if the conversion is guaranteed not to cause a loss of data.
            public static implicit operator decimal(Money money)    // add implicit conversion for decimal
            {
                return money.Amount + 5;  // you can do whatever you want as the conversion rule, such as plus 5..
            }

            public static explicit operator int(Money money)        // add cast for int
            {
                return (int)money.Amount;
            }

        }

        public static void Test_User_Defined_Conversion()
        {
            Money m = new Money(42.42M);
            decimal amount = m;
            Console.WriteLine("amount is:{0}", amount); // 42.42
            int truncatedAMount = (int)m;
            Console.WriteLine("truncatedAMount is:{0}", truncatedAMount);   // 42
            Console.ReadLine();
        }
    }

    class C2_Listing26
    {
        public static void Test_Built_in_Conversion_Function()
        {
            int value = Convert.ToInt32("42");  // Convert is a helper class for conversion between compatible types
            // for conversion between noncompatible types, we can use BitConverter class to convert base data types to an array of bytes, and an array of bytes to base data types.

            // and we can also use Parse and TryParse for various types. Here is the Parse and TryParse
            value = int.Parse("42");   
            bool success = int.TryParse("42", out value);   // for TryParse method, the return value is bool, indicating conversion is success or not
            Console.WriteLine("vlaue is:{0}, value is:{1}", value, value);
            Console.ReadLine();
          
        }
    }

    class C2_Listing27
    {
        static void OpenConnection(DbConnection connection)
        {
            if (connection is SqlConnection)    // is return booleam vaue
                Console.WriteLine("the connection is valid");
            else
                Console.WriteLine("The connection is not valid");
        }

        static void LogStream(Stream stream)
        {
            MemoryStream memoryStream = stream as MemoryStream;   // as return the converted value or null
            if (memoryStream != null)
                Console.WriteLine("memoryStream is convertable");
            else
                Console.WriteLine("memoryStream is not convertable");
        }

        public static void Test_as_And_is_operators()
        {
            OpenConnection(null);
            LogStream(null);
            Console.ReadLine();
        }
    }

    class C2_Listing28
    {
        // this example may need some Excel plugin supports from Office APIs, unable to run the code directly...
        //static void DisplayInExcel(IEnumerable<dynamic> entities)
        //{
        //    var excelApp = new Excel.Application();
        //    excelApp.Visible = true;

        //    excelApp.Workbooks.Add();

        //    dynamic workSheet = excelApp.ActiveSheet;  // we use dynamic keyword here. Dynamic represents an object whose operations will be resolved at runtime

        //    workSheet.Cells[1, "A"] = "Header A";
        //    workSheet.Cells[1, "B"] = "Header B";

        //    var row = 1;
        //    foreach (var entity in entities)
        //    {
        //        row++;
        //        workSheet.Cells[row, "A"] = entity.ColumnA;
        //        workSheet.Cells[row, "B"] = entity.ColumnB;
        //    }

        //    workSheet.Column[1].AutoFit();
        //    workSheet.Column[2].AutoFit();
        //}

        //public static void Test_Exporting_Data_To_Excel()
        //{
        //    var entities = new List<dynamic> {
        //        new
        //        {
        //            ColumnA = 1,
        //            ColumnB = "Foo"
        //        },
        //        new
        //        {
        //            ColumnA = 2,
        //            ColumnB = "Bar"
        //        }
        //    };
        //    DisplayInExcel(entities);
        //} 
    }

    class C2_Listing29
    {
        public class SampleObject : DynamicObject
        {          
            // DynamicObject.TryGetMember method: specify dynamic behavior for operations of getting a value for a property.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                // System.Dynamic.GetMemberBinder: Provides information about the object that called the dynamic operation. 
                // The binder.Name property provides the name of the member on which the dynamic operation is performed
                string Binder_Name = binder.Name;
                if (Binder_Name == "value")
                    if (value == null)
                        result = "value undefined";
                    else
                        result = value;
                else
                    result = binder.Name;
                return true;
            }

            string value = "abc";
        }

        public static void Test_Dynamic_Object()
        {
            // based on the implementation of TryGetMember above, if the requested member is not "value", we return the requested member name directly
            // else, we check the value of "value" member, if it is null, then we print "value undefined", else we print the value
            dynamic obj = new SampleObject();
            Console.WriteLine(obj.value);
            Console.WriteLine(obj.NoProperty);
            Console.ReadLine();
        }
    }

    class C2_Listing30
    {
        // this "ViewBag", "View" and "ActionResult" are all from ASP.NET MVC, they doesn't exist in this envrionment
        //public ActionResult Index()
        //{
        //    ViewBag.MyDynamicValue = "This property is not statically typed";
        //    return View();
        //}
    }

    class C2_Listing31_33
    {
        public class Accessibility
        {
            private string _myField;

            public string MyProperty
            {   // MyProperty is a wrapper of _myField
                get { return _myField; }
                set { _myField = value; }   // the value key word represent the value you assigned to this field in an instance
            }
        }

        public static void Test_Accesss_Modifier()
        {
            Accessibility obj = new Accessibility();
            obj.MyProperty = "abc";
            Console.WriteLine("the value is:{0}", obj.MyProperty);  // here you can not access _myField directly, you have to use the wrapper member, MyProperty
            Console.ReadLine();
        }
    }

    class C2_Listing_34
    {
        public class Base
        {
            private int _privateField = 42;
            protected int _protectedField = 42;

            private void MyPrivateMethod() { }
            protected void MyProtectedMethod() { }
        }

        public class Derived : Base
        {
            public void MyDerivedMethod()
            {
                //_privateField = 41;  // this member is unaccessible due to access modifier in base class
                _protectedField = 43;  // we can access this member

                //MyPrivateMethod();    // this method is unaccessible due to access modifier in base class
                MyProtectedMethod();    // we can access this method
            }
        }
    }

    class C2_Listing_35
    {
        internal class MyInternalClass
        {
            public void MyMethod() { }
        }
    }

    class C2_Listing_36_37
    {
        // listing_36
        private int _field;
        public void SetValue(int value) { _field = value; }
        public int GetValue() { return _field; }

        // listing_37
        class Person
        {
            private string _firstName;

            public string FirstName  // property for private field _firstName
            {
                get { return _firstName; }
                set {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException();
                    _firstName = value;
                }
            }

            public int Value { get; set; }  // auto-implemented property

            private int _Value_2;   // you can easily implement the get and set of the auto-implemented property
            public int Value_2
            {
                get { return _Value_2; }
                set
                {
                    _Value_2 = value;
                }
            }
        }

        public static void Test_Property()
        {
            Person Jim = new Person();
            //Jim.FirstName = " ";  // the set method will stop you to set empty value and throw error
            Jim.FirstName = "Jim";
            Jim.Value = 123;
            Console.WriteLine("Jim.FirstName: {0}, Jim.Value:{1}", Jim.FirstName, Jim.Value);
            Console.ReadLine();
        }
    }

    class C2_Listing_38_40
    {
        // Explicit interface implementation menas that an interface type element can be accessed only when using the interface directly

        // Listing_38
        public interface IObjectContextAdapter
        {
            IObjectContextAdapter objectContext { get; }
        }

        // Listing_39
        interface IInterfaceA
        {
            void MyMethod();
        }

        class Implementation : IInterfaceA
        {
            void IInterfaceA.MyMethod() { }   // this is explicit implementation
        }

        // Listing_40
        interface ILeft
        {
            void Move();
        }

        interface IRight
        {
            void Move();
        }

        class MoveableObject_explicit : ILeft, IRight
        {
            void ILeft.Move() { }   // If you don't use any access modifier, and use the interface name in front of the method, it is explicit implementation
            void IRight.Move() { }  
        }

        class MoveableObject_implicit : ILeft, IRight
        {
            public void Move() { }   // if you use keyword "public", and don't use interface name before method name, it is implicit implementation
            //public void IRight.Move() { }  // The different is, in implicit the interface methods are publicly implemented while in explicit the methods are privatelyimplemented.
        }
        
        public static void Test_Explicitly_Implement_Interface()
        {
            Implementation I_1 = new Implementation();
            //I_1.MyMethod();  // you can not access MyMethod from an instance of Implementation
            ((IInterfaceA)I_1).MyMethod();  // but if you convert the instance to the InterfaceA, then you can access MyMethod

            MoveableObject_explicit M_1 = new MoveableObject_explicit();
            //M_1.Move();  // you can not access the explicit implemented method

            MoveableObject_implicit M_2 = new MoveableObject_implicit();  // you can access the implicit implemented method
            M_2.Move();
        }
    }
}
