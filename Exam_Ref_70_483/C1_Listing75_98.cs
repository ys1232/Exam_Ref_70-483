using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Exam_Ref_70_483
{
    class C1_Listing75
    {
        // this delegate is very interesting!!!

        private delegate int Calculate(int x, int y);  // delegate is a defined method signature, can assigned to any method with the same signature

        private static int Add(int x, int y) { return x + y; }  // define a function Add with the same signature as Calculate
        private static int Multiply(int x, int y) { return x * y; }  // define a function Add with the same signature as Calculate

        public static void UseDelegate()
        {
            Calculate calc = Add;     // assign the function Add to cal
            Console.WriteLine(calc(3, 4));

            calc = Multiply;    // assign the function Multiple to cal
            Console.WriteLine(calc(3, 4));

            Console.ReadLine();
        }
    }

    class C1_Listing76
    {
        // you can even assign more than one method to a delegate, but the delegate will only return the return value of the last added method
        private static int methodOne()
        {
            Console.WriteLine("MethodOne");
            return 1;
        }

        private static int methodTwo()
        {
            Console.WriteLine("MethodTwo");
            return 2;
        }

        private delegate int Del();

        public static void Multicast()
        {
            Del d = methodOne;
            d += methodTwo;

            int x = d();
            Console.WriteLine("Return value is:{0}", x);  // return the value returned by methodTwo

            d -= methodTwo;
            x = d();
            Console.WriteLine("Return value is:{0}", x);  // return the value returned by methodOne since methodTwo has been removed

            Console.ReadLine();
        }
    }

    class C1_Listing77
    {
        // return type can be more derived than delegate signature
        // more derived means, all the exising class method and member of delegate returned value are also in the more derived type

        private static StreamWriter MethodStream()  //StreamWriter and StringWriter are both inhertted from TextWriter 
        {                                           // so you can assign MethodStream and MethodString to del, even though their returned value type are different
            Console.WriteLine("StreamWriter method called");  // if you change the method return type to int, then it won't work
            return null;
        }
        private static StringWriter MethodString()
        {
            Console.WriteLine("StringWriter method called");
            return null;
        }

        private delegate TextWriter CovarianceDel();
        private static CovarianceDel del;

        public static void Test_CovarianceDel()
        {  // you can assign both methods to del
            del = MethodStream;
            del();
            del = MethodString;
            del();

            Console.ReadLine();
        }
    }

    class C1_Listing78
    {
        // argument is less derived than delegate signature
        // then the argument passed to delegate method has all the required members and methods

        // similar concept as Listing77, but here the type of the argument vary
        // StreamWriter inheritted from TextWriter
        private static void DoSomething(TextWriter tw)  // less derived than delegate signature
        {
            Console.WriteLine("Method DoSomething is called");
        }

        private delegate void ContravarianceDel(StreamWriter tw);

        private static ContravarianceDel del = DoSomething;

        public static void Test_Contravariance()
        {
            del = DoSomething;
            del(null);
            Console.ReadLine();
        }
    }


    class C1_Listing79
    {
        // show how to use LambdaFunction, but you already known this...
        delegate int Calculate(int x, int y);

        public static void Test_LambdaFunction()
        {
            Calculate calc = (x, y) => x + y;
            Console.WriteLine(calc(3, 4));
            calc = (x, y) => x * y;
            Console.WriteLine(calc(3, 4));
            Console.ReadLine();
        }
    }

    class C1_Listing80
    {
        // Test_Lambda_Function_With_MultipleStatement
        delegate int Calculate(int x, int y);
        public static void Test_Lambda_Function_With_MultipleStatement()
        {
            Calculate calc = (x, y) =>
            {
                Console.WriteLine("Adding numbers");
                return x + y;
            };

            int result = calc(3, 4);
            Console.WriteLine("Result is:{0}", result);
            Console.ReadLine();
        }
    }

    class C1_Listing81
    {
        // using those build in delegate types are so convenient!
        public static void Test_BuildIn_Delegate_and_Action_Type()
        {
            // use action to define delegate function
            Action<int, int> calc = (x, y) =>
            {
                Console.WriteLine("The value retruned by action function is:{0}", x + y);
            };
            calc(3, 4);

            // use Func to define delegate function
            Func<int, int, string> Func_calc = (x, y) =>  // for the parameters of Func, the last parameter is the return type, others are input variables
            {
                return (x + y).ToString();
            };
            Console.WriteLine("The value returned by Func is:{0}", Func_calc(5, 6));

            Console.ReadLine();
        }
    }

    public class Pub
    {
        // here Action OnChange is defined as an auto-implemented property, for more details, check https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
        // Properties can be used as if they are public data members, but they are actually special methods called accessors. 
        public Action OnChange { get; set; }   // use Action to define a delegate OnChange that receives no parameters and has no return value 

        public void Raise()
        {
            if (OnChange != null)  // OnChange != null means, we have subscriptions for this event
                OnChange();
        }
    }

    class C1_Listing82
    {
        public static void CreateAndRaise()
        {
            Pub p = new Pub();
            p.OnChange += () => Console.WriteLine("Event raised to method 1");  // we add a subscription to OnChange event
            p.OnChange += () => Console.WriteLine("Event raised to method 2");  // add one more subscription
            p.Raise(); // p.Raise() will call all the subscriptors
            Console.ReadLine();
        }
    }

    class C1_Listing82_version2
    {
        // an easier way to use Action to create delegate, but here it is not event...
        public static Action OnChange2;
        public static void CreateAndRaise()
        {
            OnChange2 = () => Console.WriteLine("Event raised to method 3");
            OnChange2();
            Console.ReadLine();
        }
    }


    class C1_Listing83
    {
        // this is a simple example to show the very basic concept only... for detailed and running example, view Listing84
        public class Pub_2
        {
            public event Action OnChange = delegate { };   // here we are using a field directly, not a property

            public void Raise()
            {
                OnChange();
            }
        }

    }


    class C1_Listing84
    {
        public class MyEventArgs : EventArgs  // EventArgs Class Represents the base class for classes that contain event data, and provides a value to use for events that do not include event data
        {                                // To create a custom event data class, create a class that derives from the EventArgs class and provide the properties to store the necessary data. 
                                         // The name of your custom event data class should end with EventArgs. To pass an object that does not contain any data, use the Empty field.
            public MyEventArgs(int value)
            {
                Value = value;
            }
            public int Value { get; set; }
        }

        public class Pub
        {
            public event EventHandler<MyEventArgs> OnChange = delegate { };   // this MyEventArgs is the class we defined that derives from EventArgs
            public void Raise()
            {
                OnChange(this, new MyEventArgs(42)); // passing an instance of MyArgs
            }
        }

        public static void CreateAndRaise()
        {
            Pub p = new Pub();
            p.OnChange += (sender, e) => Console.WriteLine("Event raised:{0}", e.Value);    // add a subscriptor to the event 
            p.Raise();
            Console.ReadLine();
        }
    }

    class C1_Listing85
    {
        public class MyEventArgs : EventArgs  // the EventArgs class we defined
        {
            public MyEventArgs(int value)
            {
                Value = value;
            }
            public int Value { get; set; }
        }

        public class Pub
        {
            // a special version of property. When using event for property, we don't use get and set, we use add and remove instead.
            //The add contextual keyword is used to define a custom event accessor that is invoked when client code subscribes to your event. 
            //If you supply a custom add accessor, you must also supply a remove accessor.
            // You do not typically need to provide your own custom event accessors. 
            // The accessors that are automatically generated by the compiler when you declare an event are sufficient for most scenarios.
            private event EventHandler<MyEventArgs> onChange = delegate { };

            public event EventHandler<MyEventArgs> OnChange
            {
                add
                {
                    lock (onChange)
                    {
                        onChange += value;
                    }
                }

                remove
                {
                    lock (onChange)
                    {
                        onChange -= value;
                    }
                }
            }

            public void Raise()
            {
                onChange(this, new MyEventArgs(42));
            }
        }

        public static void CreateAndRaise()
        {
            Pub p = new Pub();
            p.OnChange += (sender, e) => Console.WriteLine("Event raised:{0}", e.Value);    // add a subscriptor to the event, internally the add function is called
            p.OnChange += (sender, e) => Console.WriteLine("Event raised again:{0}", e.Value);    // add a subscriptor to the event, internally the add function is called
            p.Raise();
            Console.ReadLine();
        }

    }

    class C1_Listing86
    {
        // exception handling. Event will not automatically handle the exception for you
        // in this examble, Subscriber 1 is called, then the error is thrown, Subscriber 3 is never called
        public class Pub
        {
            public event EventHandler OnChange = delegate { };
            public void Raise()
            {
                OnChange(this, EventArgs.Empty);
            }
        }

        public static void CreateAndRaise()
        {
            Pub p = new Pub();
            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 1 called");

            //p.OnChange += (sender, e) => throw new Exception();

            p.OnChange += (sender, e) => Console.WriteLine("Sbuscriber 3 called");

            p.Raise();

            Console.ReadLine();
        }
    }

    class C1_Listing87
    {
        public class Pub
        {
            public event EventHandler OnChange = delegate { };
            public void Raise()     // in event raise function, provide error handling code
            {
                var exceptions = new List<Exception>();   // add all exceptions into this exceptions list

                foreach (Delegate handler in OnChange.GetInvocationList()) // GetInvocationList() returns an array of delegates representing the invocation list of the current delegate.
                {
                    try
                    {
                        handler.DynamicInvoke(this, EventArgs.Empty);  // invoke the method of current delegate
                    }
                    catch (Exception ex)  // catch all exceptions and added to the list
                    {
                        exceptions.Add(ex);
                    }
                }

                if (exceptions.Any())  // if the exception list is not empty, throw exception
                {
                    throw new AggregateException(exceptions);  // throw the list to main 
                }
            }
        }

        public static void CreateAndRaise()
        {
            Pub p = new Pub();
            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 1 called");

            //p.OnChange += (sender, e) => throw new Exception();

            p.OnChange += (sender, e) => Console.WriteLine("Sbuscriber 3 called");

            try
            {
                p.Raise();
            }
            catch (AggregateException ex)  // catch the exception list thrown by Raise()
            {
                Console.WriteLine(ex.InnerExceptions.Count);
            }
            Console.ReadLine();
        }
    }

    class C1_Listing88
    {
        public static void Test_Unhandled_Exception()
        {
            string s = "NaN";
            int i = int.Parse(s);
        }
    }

    class C1_Listing89
    {
        public static void Test_Catch_FormatException()
        {
            Console.WriteLine("Please enter an integer (Enter exit to terminate and return):");
            while (true)
            {
                string s = Console.ReadLine();

                if (s == "exit")
                    break;

                if (string.IsNullOrWhiteSpace(s))
                {
                    Console.WriteLine("We don't accept blank/empty input, please try again...");
                    continue;
                }

                try
                {
                    int i = int.Parse(s);
                    Console.WriteLine("Your input is {0}, it is a valid integer.", i);
                    continue;
                }
                catch (FormatException)     // catch the formate exception
                {
                    Console.WriteLine("{0} is not a valid number. Please try again", s);
                }
            }
        }
    }

    class C1_Listing90
    {
        public static void Test_Catch_Dif_Exception_Types()
        {
            Console.WriteLine("Please enter an integer:");
            string s = Console.ReadLine();  // actually you can not explicitly enter a null value, you can only enter " " or ""
                                            // The only way to get into the first catch block is to hard code the following assignment...
                                            //s = null;

            try
            {
                int i = int.Parse(s);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You need to enter a value");
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not a valid number. Please try again", s);
            }
            Console.ReadLine();
        }
    }

    class C1_Listing91
    {
        public static void Test_FinallyBlock()
        {
            string s = Console.ReadLine();

            try
            {
                if (s == "" || s == " ")  // you can not enter null in console...
                    s = null;
                int i = int.Parse(s);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You need to enter a value");
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not a valid number. Please try again", s);
            }
            finally {
                Console.WriteLine("Program complete");
                Console.ReadLine();
            }
        }
    }

    class C1_Listing92
    {
        // by executing FailFast(), the program will return immediatly without executing the finally block. Since the catch won't work...
        public static void Test_Environment_FailFast()
        {
            string s = Console.ReadLine();
            try
            {
                int i = int.Parse(s);
                if (i == 42) Environment.FailFast("Special number entered");  //this message will be saved in windows application log
            }
            catch (ArgumentNullException e)   // if the exception can not be catched, the program will fail. Then the finally block will not be executed. 
                                              // For example, if we use ArgumentNullException here, the FormatException won't be catched, so finally block will not be executed and the program will fail immediately
                                              // ??? but the official document says the finally block will run even the excpetion is not catched... ???  https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-finally
            {
                Console.WriteLine("We encountered the error:{0}", e.Message);
            }
            finally
            {
                Console.WriteLine("Program complete");
                Console.ReadLine();
            }
        }
    }

    class C1_Listing93
    {
        public static void Test_Inspect_Exception()
        {
            try
            {
                int i = ReadAndParse();
                Console.WriteLine("Parsed {0}", i);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("StackTrace: {0}", e.StackTrace);
                Console.WriteLine("HelpLink: {0}", e.HelpLink);
                Console.WriteLine("InnerException: {0}", e.InnerException);
                //Console.WriteLine("TargetSite: {0}", e.TargetSite);  //no idea why the exception here don't have the TargetSite property...
                Console.WriteLine("Source: {0}", e.Source);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static int ReadAndParse()
        {
            string s = Console.ReadLine();
            int i = int.Parse(s);
            return i;
        }
    }

    class C1_Listing94
    {
        public static void Test_ThrowException()
        {
            string FileName = null;

            try
            {
                if (string.IsNullOrWhiteSpace(FileName))
                    throw new ArgumentNullException("fileName", "Filename is required");  // Initializes an instance of the ArgumentNullException class with a specified error message and the name of the parameter that causes this exception.
                else
                    Console.WriteLine("The FileName is valid");
            }
            catch (Exception e)
            {
                Console.WriteLine("Message: {0}", e.Message);  // will print:    Message: Filename is required. Parameter name: fileName
                Console.WriteLine("StackTrace: {0}", e.StackTrace);
                Console.WriteLine("HelpLink: {0}", e.HelpLink);
                Console.WriteLine("InnerException: {0}", e.InnerException);
                Console.WriteLine("Source: {0}", e.Source);
            }
            finally {
                Console.WriteLine("The program is complete");
                Console.ReadLine();
            }

        }
    }

    class C1_Listing95
    {
        public static void Test_ReThrow_Exception()
        {
            try { int.Parse("s"); }
            catch (Exception logEx)
            {
                // do the error logging here
                throw;  // no change on the exception, the stackTrace keeps the same
            }
        }
    }

    class C1_Listing96
    {
        public static void Test_Adding_To_Original_Exception()
        {
            try { int.Parse("s"); }
            catch (Exception ex)
            {
                //throw new OrderProcessingException("Error while processing order",ex);  // this OrderProcessingException() is a custom function, and the author didn't provide the implementation code
                // so there is nothing you can do...
            }
        }
    }

    class C1_Listing97
    {
        // using ExceptionDispatchInfo you can throw an exceptionand preserve the original stack trace
        public static void Test_Use_ExceptionDispatchInfo()
        {
            ExceptionDispatchInfo possibleException = null;
            try
            {
                string s = Console.ReadLine();
                int.Parse(s);
            }
            catch (FormatException ex)
            {
                possibleException = ExceptionDispatchInfo.Capture(ex);  // in the catch block, we put the exception into possibleException
                Console.WriteLine("exception captured");
            }

            if (possibleException != null)  // if possibleException is not null, we throw the exception
                possibleException.Throw();
        }
    }
    
    class C1_Listing98
    {
        // ??? figure it out tomorrow....
        //[Serializable]
        //public class OrderProcessingException : Exception, ISerializable
        //{
        //    public OrderProcessingException(int orderId)
        //    {
        //        OrderId = orderId;
        //        this.HelpLink = "http://www.microsoft.com";
        //    }

        //    public OrderProcessingException(int orderId, string message) : base(message)
        //    {
        //        OrderId = orderId;
        //    }

        //    public OrderProcessingException(int orderId, string message, Exception innerException) : base(message, innerException)
        //    {
        //        OrderId = orderId;
        //        this.HelpLink = "http://www.microsoft.com";
        //    }

        //    protected EntityOperationException(SerializationInfo info, StreamingContext context)
        //    {
        //        OrderId = (int)info.GetValue("OrderId", typeof(int));
        //    }
        //    public int OrderId { get; private set; }

        //    public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //    {
        //        base.GetObjectData(info, context);
        //        info.AddValue("entityId", entityId, typeof(int));
        //    }
        //}


    }
}
