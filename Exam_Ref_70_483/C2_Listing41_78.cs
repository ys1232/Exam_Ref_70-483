using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

namespace Exam_Ref_70_483
{
    class C2_Listing41
    {
        interface IExample
        {
            string GetResult();
            int Value { get; set; }
            event EventHandler ResultRetrieved;
            int this[string index] { get; set; }
        }

        class ExampleImplementation : IExample
        {
            public string GetResult()  // implement GetResult method
            {
                return "result";
            }

            int IExample.Value { get; set; }  // explicitly implement Value property

            public event EventHandler CalculationPerformed;  // add a new EventHandler field

            public event EventHandler ResultRetrieved;  // implement ResultRetrieved EventHandler

            public int this[string index]   // implement the index field
            {
                get
                {
                    return 42;
                }
                set { }
            }
        }
    }

    class C2_Listing42
    {
        interface IReadOnlyInterface
        {
            int Value { get; }
        }

        struct ReadAndWriteImplementation : IReadOnlyInterface
        {
            public int Value { get; set; }  // we add a set accessor in the implementation
        }
    }

    class C2_Listing43
    {
        // in yoru implemention, you can use different Type for T, such as IRepository<Product>, IRepository<Order>, and add type constrains
        interface IRepository<T>
        {
            T FindById(int id);
            IEnumerable<T> All();
        }
    }

    class C2_Listing44
    {
        interface IAnimal
        {
            void Move();
        }

        class Dog : IAnimal
        {
            public void Move() { Console.WriteLine("Move"); }
            public void Bark() { Console.WriteLine("Bark"); }
        }

        public static void Test_Instantiating_Interface()
        {
            // you can instantiate a concrete type that implements an interface, but you can not instantiate the interface directly
            IAnimal animal = new Dog();
            animal.Move();  // Dog.Move() is called
            //animal.Bark();    // but you can not access Dog.Bark() unless you cast animal to Dog
            ((Dog)animal).Bark();
            Dog Dog_1 = new Dog();

            Console.ReadLine();
            // IAnimal animal_2 = new IAnimal();  // this instantiation doesn't work
        }

    }


    class C2_Listing45_46
    {
        // Listing45
        interface IEntity  // create interface IEntity
        {
            int Id { get; }
        }

        class Repository<T> where T : IEntity   // create a generic class, constrain to IEntity
        {
            protected IEnumerable<T> _elements; // IEnumerable is a build-in generic interface, support a simple iteration over a collection of specified type  .
                                                // here we define a generic collection _elements

            public Repository(IEnumerable<T> elements)  // Constructor: initalize this generic field
            {
                _elements = elements;
            }

            public T FindById(int id)
            {
                return _elements.SingleOrDefault(e => e.Id == id);  // Enumerable.SingleOrDefault method: Returns the only element of a sequence, or a default value if the sequence is empty
            }

        }

        // listing 46
        // Order is an implementation of IEntity, OrderRepository is a derivation of Repository. We show how this works in this example
        class Order : IEntity   // create class Order to implement IEntity interface
        {
            public int Id { get; }
        }

        class OrderRepository : Repository<Order>   // create collection OrderRepository derive from generic collection Repository use Order as type T
        {
            public OrderRepository(IEnumerable<Order> orders) : base(orders) { }    // the constructor

            public IEnumerable<Order> FilterOrdersOnAmount(decimal amount)  // in this derived collection, we add this method to filter orders on given amount
            {
                // we create an empty collection first, then apply the filter rule to OrderRepository based on a given amount. Then return the filtered collection

                List<Order> result = null;
                // add filter code here
                return result;
            }
        }
    }


    class C2_Listing47
    {   // you can only override the virtual method in base class
        class Base
        {
            public virtual void Execute()
            {
                Console.WriteLine("Executing Base method");
            }
        }

        class Derived : Base
        {
            public override void Execute()
            {
                Log("Before executing");
                base.Execute(); // use base.Execute() to reuse the methods from base class
                Log("After executing");
            }

            private void Log(string message) { Console.WriteLine(message); }
        }

        public static void Test_Virtual_Class_Extension()
        {
            Derived D1 = new Derived();
            D1.Execute();
            Console.ReadLine();
        }
    }

    class C2_Listing48
    {
        class Base
        {
            public void Execute() { Console.WriteLine("Base.Execute"); }
        }

        class Derived : Base
        {
            public new void Execute() { Console.WriteLine("Derived.Execute"); }
        }

        public static void Test_Hiding_Method_With_New()
        {
            Base b = new Base();
            Derived d = new Derived();
            b.Execute();    // Base.Execute
            b = new Derived();
            b.Execute();    // Base.Execute
            ((Derived)b).Execute(); // Derived.Execute
            d.Execute();    // Derived.Execute
            Console.ReadLine();
        }
    }

    class C2_Listing49
    {
        // for abstract class, you can implement the methods or leave it for derived class
        abstract class Base
        {
            public virtual void MethodWithImplementation() { }  // implemented the method. And add virtual keyword, to make it overridable

            public abstract void AbstractMethod();  // didn't implement this method
            // you can override both virtual and abstract members. Abstract method can not have implementation, but virtual class can have implementation
        }

        class Derived : Base
        {
            public override void AbstractMethod() { }   // override the abstracti method
            public override void MethodWithImplementation()     // override the virtual method
            {
                base.MethodWithImplementation();
            }
        }
    }

    class C2_Listing50_52
    {
        // Listing 50
        class Rectangle
        {
            public Rectangle(int width, int height) // constructor
            {
                Width = width;
                Height = height;
            }

            public virtual int Height { get; set; } // virtual method Height
                
            public virtual int Width { get; set; }  // virtual method Width

            public int Area // Area method
            {
                get { return Height * Width; }
            }
        }

        // Listing 51
        private class Square : Rectangle
        {
            public Square(int size) : base(size, size) { }  // base refer the base class, so base(size, size) is the base constructor,  we use the same value for width and height

            public override int Width   // override the Width and Height method
            {
                get { return base.Width; }
                set
                {
                    base.Width = value;
                    base.Height = value;
                }
            }

            public override int Height
            {
                get { return base.Height; }
                set
                {
                    base.Height = value;
                    base.Width = value;
                }
            }
        }

        // Listing 52
        public static void Test_Use_Square_Class()
        {
            Rectangle rectangle = new Square(1);
            rectangle.Width = 10;
            rectangle.Height = 5;

            Console.WriteLine(rectangle.Area);
            Console.ReadLine();
        }
    }

    /** abstract class VS interface
    An abstract class is a class that cannot be instantiated, but must be inherited from. 
        An abstract class may be fully implemented, but is more usually partially implemented or not implemented at all.
    An interface, by contrast, is a totally abstract set of members that can be thought of as defining a contract for conduct. 
        The implementation of an interface is left completely to the developer.
    Abstract classes should be used primarily for objects that are closely related, whereas interfaces are best suited for providing common functionality to unrelated classes. 
        Since we can reuse the implementation code provided by abstract class
    **/
    class C2_Listing53_54
    {
        // IComparable is a standard interface defined by .NET Framework. The class implementation need to implement a comparison method, CompareTo.
        // To compares the current instance with another object of the same type and returns an integer that indicates 
        // whether the current instance precedes (return less than zero), follows (return greater than zero), or occurs in the same position (return zero) in the sort order as the other object.

        public interface IComparable
        {
            int CompareTo(object obj);
        }

        class Order : IComparable
        {
            public DateTime Created { get; set; }   // the comparable property

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                Order o = obj as Order; // convert the input argument to Order type

                if (o == null)  // if the input argument is not ocnvertable, throw an error
                {
                    throw new ArgumentException("Object is not an Order");
                }

                return this.Created.CompareTo(o.Created);
            }
        }

        public static void Test_Implement_IComparable_Interface()
        {
            List<Order> orders = new List<Order>
            {
                new Order {Created = new DateTime(2012, 12, 1)},
                new Order {Created = new DateTime(2012, 1, 6)},
                new Order {Created = new DateTime(2012, 7, 8)},
                new Order {Created = new DateTime(2012, 2, 20)}
            };
            orders.Sort();
        }
    }

    class C2_Listing55
    {
        public static void Test_Enumerator_Iteration()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 5, 7, 9 };

            using (List<int>.Enumerator enumerator = numbers.GetEnumerator())   // GetEnumerator method: Returns an enumerator that iterates through the List<T>
            {
                while (enumerator.MoveNext()) Console.WriteLine(enumerator.Current);    // use enumerator.MoveNext and enumerator.Curren to iterate the list
            }

            Console.ReadLine();

        }
    }

    class C2_Listing56
    {
        class Person
        {
            public Person(string firstName, string lastName)    // constructor
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()   // override the ToString method
            {
                return FirstName + " " + LastName;
            }
        }

        class People : IEnumerable<Person>  // derive from IEnumerable interface using Person class
        {
            public People(Person[] people)  // constructor
            {
                this.people = people;   // initialize the array of Person
            }
            Person[] people;    // array of person

            public IEnumerator<Person> GetEnumerator()  // implement the GetEnumerator method
            {
                for (int index = 0; index < people.Length; index++)
                    yield return people[index];
            }

            IEnumerator IEnumerable.GetEnumerator() // the GetEnumerator method of the interface
            {
                return GetEnumerator();
            }
        }
    }

    class C2_Listing57
    {
        public interface IDisposable    //  IDisposable interface contains only one method, Dispose. It is used to free unmanaged resources, like file handles and database connections
        {
            void Dispose();
        }

        class DB_Handler : IDisposable
        {
            SqlConnection DW_Connection;
            StreamReader Logging_SR;
            public void Dispose()
            {
                if (DW_Connection != null)
                    DW_Connection.Close();
                if (Logging_SR != null)
                    Logging_SR.Close();
            }
            // ... add extension methods here...
        }
    }

    // no need to put code for Listing 58 - 60 here...
    //class C2_Listing58
    //{

    //}

    class C2_Listing59
    {
        [Conditional("CONDITION1"), Conditional("CONDITION2")]  // except condition1 and condition2, the call to this method will be ignored
        static void MyMethod() { }
    }

    //class C2_Listing60
    //{

    //}

    class C2_Listing61
    {
        [Serializable]
        class Person { }

        // to check an attribute is added to a type
        public static void Test_Check_Attribute_Defined()
        {
            if (Attribute.IsDefined(typeof(Person), typeof(SerializableAttribute))) // Attribute.IsDefined(MemberInfo, AttributeType) : Determines whether any custom attributes are applied to a member of a type
                Console.WriteLine("SerializableAttribute is added");
            else
                Console.WriteLine("SerializableAttribute is not added");
            Console.ReadLine();
        }
    }

    class C2_Listing62
    {
        public static void Test_Getting_Attribute_Instance()
        {
            // ??? no idea how to get this code work...???
            // GetCustomAttributes(MemberInfo, Type): Retrieves an array of the custom attributes applied to a member of a type.  First(): returns the first element of a sequence
            ConditionalAttribute conditionalAttribute = (ConditionalAttribute)Attribute.GetCustomAttributes(typeof(C2_Listing59), typeof(ConditionalAttribute)).First();
            // use GetCustomAttributes below can retrieve the Conditional("CONDITION1") from C2_Listing59
            Console.WriteLine("The conditionalAttribute is:{0}",conditionalAttribute.ConditionString);
            Console.ReadLine();
        }
    }

    // Attributes provide a powerful method of associating declarative information with C# code
    // Once associated with a program entity, the attribute can be queried at run time and used in any number of ways.
    // Declaring an attribute in C# is simple — it takes the form of a class declaration that inherits from System.Attribute
    class C2_Listing63_68   // the example in the book use Nunit property, that is unaccessible in current environment
    {
        [AttributeUsage(AttributeTargets.All)]  // define the AttributeTargets for this custom attribute to apply, you can select All, class, event, method, field, constructor...
        // [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]   // or you can use multiple Targets in this way
        // [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]  // AllowMultiple property indicates whether multiple instances of your attribute can exist on an element. By default, it is false
        public class CategoryAttribute : System.Attribute   //all custom attribute has to be derived from System.Attribute directly or indirectly
        {
            public string value;
            public CategoryAttribute(string value)
            { this.value = value; }
        }

        [Category("Category_B")]    // now the CategoryAttribute is associated with field Student_Name
        public class Student
        { }

        // We accessing attributes through reflection, to query their existence and values. 
        // The main reflection methods to query attributes are contained in the System.Reflection.MemberInfo class
        public static void Test_Access_Custom_Attribute()
        {
            System.Reflection.MemberInfo info = typeof(Student);
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                System.Console.WriteLine(attributes[i].GetType());
            }
            Console.ReadLine();
        }
    }

    /**
     * Reflection
     * all reflection methods are defined in the System.Reflection namespace, it contains types that 
     * retrieve information about assemblies, modules, members, parameters, and other entities 
     * in managed code by examining their metadata.
     * */

    class C2_Listing69_71
    {
        // this code is tricky...
        public interface IPlugin
        {
            string Name { get; }
            string Description { get; }
            bool Load(MyApplication application);
        }

        public class MyApplication
        { }

        public class MyPlugin : IPlugin
        {
            public string Name  // property
            {
               get { return "MyPlugin"; }
            }

            public string Description   // property
            {
                get { return "My Sample Plugin"; }
            }

            public bool Load(MyApplication application) // method
            { return true; }
        }

        public static void Test_Get_Plugin_Info_Via_custom_Attribute()
        {
            Assembly pluginAssembly = Assembly.Load("assemblyname"); // Assembly Class reprsents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
            // Assembly.Load(Assemblyname): load an assembly by given name
            var plugins = from type in pluginAssembly.GetTypes()    // use Linq to select all valid types (check by IsAssignableFrom method) from the loaded assembly
                          where typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface
                          select type;

            foreach (Type pluginType in plugins) 
            {
                IPlugin plugin = Activator.CreateInstance(pluginType) as IPlugin;
            }

        }
    }

    class C2_Listing72
    {
        public class Student
        {
            private int grade;
            private int age;
            private string name;
            public int Test_Code;
            private int school_code { get; set; }    // how about private property?
            public int district_code { get; set; }    // how about public property?
            public string Gender;

            public Student()
            {
                grade = 1;
                age = 10;
                name = "Yuan";
                Test_Code = 2;
                school_code = 321;
                district_code = 567;
                Gender = "female";
            }
        }

        public static void Test_Getting_Values_of_a_Field_Via_Reflection()
        {
            Student student = new Student();
            DumpObject(student);    // prints :1,10,321,567
            Console.ReadLine();
        }

        static void DumpObject(object obj)  // use reflection to iterate over an object and selects all the private integer fields
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic); // BindingFlags is a bitmask comprised of one or more BindingFlags that specify how the search is conducted.
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(int)) // if the FieldType is int, print the value
                    Console.WriteLine(field.GetValue(obj));
            }
        }
    }

    class C2_Listing73
    {
        public static void Test_execute_method_through_reflection()
        {
            int i = 42;
            MethodInfo compareToMethod = i.GetType().GetMethod("CompareTo", new Type[] { typeof(int) });    // using reflection method GetType, GetMethod to get the Int32 method "CompareTo"
            int result = (int)compareToMethod.Invoke(i, new object[] { 41 });   // then call this method

            Console.WriteLine("result :{0}",result);
            Console.ReadLine();
        }
    }

    /**
     * The System.CodeDom namespace contains classes that can be used to represent the elements and structure of a source code document. 
     *  The classes in this namespace can be used to model the structure of a source code document that can be output as source code 
     *  in a supported language using the functionality provided by the System.CodeDom.Compiler namespace.
     *  --> in this example, we use CSharpCodeProvider to output C# code
     * */
    class C2_Listing74_76
    {
        public static void Test_Using_CodeDOM()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();    // file
            CodeNamespace myNamespace = new CodeNamespace("MyNamespace");   // namespace
            myNamespace.Imports.Add(new CodeNamespaceImport("System")); // using System;
            CodeTypeDeclaration myClass = new CodeTypeDeclaration("MyClass");   // class
            CodeEntryPointMethod start = new CodeEntryPointMethod();    // main method
            CodeMethodInvokeExpression cs1 = new CodeMethodInvokeExpression(    // method
                new CodeTypeReferenceExpression("Console"), "WriteLine", new CodePrimitiveExpression("Hello World!"));
            CodeMethodInvokeExpression cs2 = new CodeMethodInvokeExpression(    // method
                new CodeTypeReferenceExpression("Console"), "ReadLine");

            compileUnit.Namespaces.Add(myNamespace);    // add namespace to file
            myNamespace.Types.Add(myClass); // add class to namespace
            myClass.Members.Add(start); // add main function to myClass
            start.Statements.Add(cs1);  // add cs1 and cs2 to main method
            start.Statements.Add(cs2);


            CSharpCodeProvider provider = new CSharpCodeProvider();
            using (StreamWriter sw = new StreamWriter("HelloWorld.cs", false))  // the output file path is: C:\Visual_Studio_Project\DotNet_Projects\ExamRef_70_483_Chapter1\ExamRef_70_483_Chapter2\bin\Debug
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "   ");
                provider.GenerateCodeFromCompileUnit(compileUnit, tw, new System.CodeDom.Compiler.CodeGeneratorOptions());
                tw.Close();
            }
        }
    }

    class C2_Listing77
    {
        public static void Test_Create_Lambda_Function()
        {
            // use lambda function to define a delegate function
            Func<int, int, int> addFunc = (x, y) => x + y; // the last argument is return type
            Console.WriteLine(addFunc(2, 3)); //use the delegate function
            Console.ReadLine();
        }
    }

    /**
     * System.Linq.Expressions namespace contains classes, interfaces and enumerations that enable language-level code expressions 
     * to be represented as objects in the form of expression trees.
     * 
     * */
    class C2_Listing78
    {
        public static void Test_Creating_Expression_Tree()
        {  // BlockExpression and Expression are all classes in System.Linq.Expressions namespace
            // BlockExpression Class: Represents a block that contains a sequence of expressions where variables can be defined.
            // Expression Class: Provides the base class from which the classes that represent expression tree nodes are derived.

            BlockExpression blockExpr = Expression.Block(   // Block(Expression, Expression):Creates a BlockExpression that contains two expressions and has no variables.
                Expression.Call(    // Call(Expression, MethodInfo): Creates a MethodCallExpression that represents a call to a method that takes no arguments.                     
                    null, typeof(Console).GetMethod("Write", new Type[] { typeof(String)}), Expression.Constant("Hello ")  
                    ),  // Type.GetMethod Method (String, Type[], ParameterModifier[]): Searches for the specified public method whose parameters match the specified argument types and modifiers.
                Expression.Call(
                    null, typeof(Console).GetMethod("Write", new Type[] { typeof(String) }), Expression.Constant("World")
                    )
                );

            Expression.Lambda<Action>(blockExpr).Compile()();   // the expression is compiled to an Action and executed
            Console.ReadLine();
        }
    }

}
