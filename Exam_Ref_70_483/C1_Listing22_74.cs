using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Windows;
using System.Collections.Generic;

namespace Exam_Ref_70_483
{
    
    class Exam_Ref_70_483
    {
        public static void Test_AsParallel()
        {
            var number = Enumerable.Range(0, 500);//.OrderByDescending(c => c).ToArray();  // Enumerable class provides a set of static (Shared in Visual Basic) methods for querying objects that implement IEnumerable<T>
            // this AsParallel method enables parallelization of a query
            
            var parallelResult = number.AsParallel().Where((i) => i % 2 == 0).ToArray();   // i is the source data, which is the range in this case

            int counter = 1;
            foreach (int i in parallelResult)
            {
                Console.WriteLine("the {0}th element:{1}:", counter, i);   // from the result you can see, the numbers in the array don't follow the numeric order, since it is parallel processing
                counter++;
            }
            Console.ReadLine();
        }

        internal class Person
        {
            public Person()
            {
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

    class Listing1_23
    {
        // it is just what we did in Listing1_22
    }

    class Listing1_24
    {   // same as 1_22, but now the result is in the sorted order
        public static void Test_AsParallel_Ordered()
        {
            var number = Enumerable.Range(0, 10);  // Enumerable class provides a set of static (Shared in Visual Basic) methods for querying objects that implement IEnumerable<T>
                                                   // this AsParallel method enables parallelization of a query
            var parallelResult = number.AsParallel().AsOrdered().Where((i) => i % 2 == 0).ToArray();   // i is the source data, which is the range in this case

            int counter = 1;
            foreach (int i in parallelResult)
            {
                Console.WriteLine("the {0}th element:{1}:", counter, i);   // from the result you can see, the numbers in the array don't follow the numeric order, since it is parallel processing
                counter++;
            }
            Console.ReadLine();
        }
    }

    class Listing1_25
    {
        // ??? I can not see the difference with or without AsSequential()...
        public static void Test_Parallel_AsSequential()
        {
            var numbers = Enumerable.Range(0, 20000);
            var parallelResult = numbers.AsParallel().AsOrdered()
                .Where(i => i % 2 == 0);//.AsSequential();

            foreach (int i in parallelResult.Take(50))    // Take will define how many values to take from the Generic collection
                Console.WriteLine(i);
            Console.ReadLine();
        }
    }

    class Listing1_26
    {
        public static void Test_Parallel_ForAll()
        {
            var numbers = Enumerable.Range(0, 20);
            var parallelResult = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0);

            parallelResult.ForAll(e => Console.WriteLine(e)); // compared with the foreach loop in 1_25, ForAll uses multi-threading, so the value in console is not in the sequential order
            Console.ReadLine();
        }
    }

    class Listing1_27
    {
        public static void Test_Parallel_AggregateException()
        {
            var numbers = Enumerable.Range(0, 20);

            try
            {
                var parallelResult = numbers.AsParallel().Where(i => IsEven(i));
                parallelResult.ForAll(e => Console.WriteLine(e));
            }
            catch (AggregateException e)   // all exceptions within those parallel queries will be wrapped into one AggregateException object 
            {                              // you can loop through it's InnerExceptions to check each individual exception...
                Console.WriteLine("There where {0} exceptions", e.InnerExceptions.Count);
            }
            Console.ReadLine();
        }

        public static bool IsEven(int i)
        {
            if (i % 10 == 0) throw new ArgumentException("i:"+i.ToString());

            return i % 2 == 0;
        }
    }

    class Listing1_28
    {
        public static void Test_BlockingCollection()
        {
            /**
             * this code create two threads, one thread read itme and add the item to collection, one thread remove items from collection and print 
             * */
            BlockingCollection<string> col = new BlockingCollection<string>();

            Task read = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(col.Take());  // the take method will remove one item from the blockingCollection and return the removed item
                }  // so this while loop will keep checking items, print it and remove it
            });

            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();  // reading string from Console
                    if (string.IsNullOrWhiteSpace(s)) break;     // if the input is null or whitespace, break
                    col.Add(s);             // else, add the item to the collection
                }
            });

            write.Wait();  // this program will wait entil the write task is done (break the loop)         
        }
    }

    class Listing1_29
    {
        public static void Test_GetConsumingEnumerable()
        {
            // Compared with Listing1_28. GetConsumingEnumerable will return only when we got new available values, so no need to use while loop
            BlockingCollection<string> col = new BlockingCollection<string>();

            col.Add("abc");
            col.Add("cde");
            col.Add("fgh");

            Task read = Task.Run(() =>
            {
                foreach (string v in col.GetConsumingEnumerable())  // GetConsumingEnumerable will return only when we got new available values, so no need to use while loop
                    Console.WriteLine(v);
            });

            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();  // reading string from Console
                    if (string.IsNullOrWhiteSpace(s))      // if the input is null or whitespace, break
                    {
                        Console.WriteLine("Entered empty value, now entry any key to exit...");
                        break;
                    }
                    col.Add(s);             // else, add the item to the collection
                }
            });

            write.Wait();

            Console.ReadLine();
        }
    }

    class Listing1_30
    {
        public static void Test_ConcurrentBag()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            bag.Add(42);
            bag.Add(22);

            int result;
            if (bag.TryTake(out result))  // attemp to remove and return an object from ConcurrentBag
                Console.WriteLine("First TryTake, element {0} removed.",result);  // print and remove the first item

            if (bag.TryPeek(out result))   // attemp to return an object from ConcurrentBag without removing it
                Console.WriteLine("TryPeek element:{0}", result);  // print the second item

            if (bag.TryTake(out result)) 
                Console.WriteLine("Second TryTake, element {0} removed.", result);  // print and remove the second item

            if (bag.TryTake(out result))  
                Console.WriteLine("Second TryTake, element {0} removed.", result);  // try to print and remove the third item, which is not exist, so do nothing...

            Console.ReadLine();
        }
    }

    class Listing1_31
    {
        public static void Test_Enumerating_In_ConcurrentBag()
        {
            // when foreach loop started, only 42 is in the snapshot, so 21 will not shown in the console
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            Task.Run(() =>
            {
                bag.Add(42);
                Thread.Sleep(1000);
                bag.Add(21);
            });

            Task.Run(() =>
            {
                Thread.Sleep(200);  // add sleep(200) to make sure 42 will be added to the bag when running the foreach loop
                foreach (int i in bag)
                    Console.WriteLine(i);
            }).Wait();

            Console.ReadLine();
        }
    }

    class Listing1_32
    {
        public static void Test_ConcurrentStack()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            stack.Push(42);  // push 42 to stack

            int result;
            if (stack.TryPop(out result))   // pop 42 from stack
                Console.WriteLine("Popped: {0}", result);
            stack.PushRange(new int[] { 1,2,3,4});  // try to put multiple values into the stack, 4 will be on the top now

            int[] values = new int[2];
            stack.TryPopRange(values);  // try to pop multiple values from the top of the stack, here we try t pop two values, sinc the array size is 2. Now 2 is on the top of the stack

            foreach (int i in values)  // print the poped values
                Console.WriteLine(i);

            Console.WriteLine("Remaining values in stack");
            foreach (int i in stack)  // print the remaining values
                Console.WriteLine(i);

            Console.ReadLine();
        }
    }

    class Listing1_33
    {
        public static void Test_ConcurrentQueue()
        {
            // similar to Listing1_32, but we are testig ConcurrentQueue here
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
            queue.Enqueue(42);
            queue.Enqueue(32);

            int result;
            foreach (int i in queue)
            {
                queue.TryDequeue(out result);
                Console.WriteLine("Dequeued:{0}", result);
            }
            Console.ReadLine();
        }
    }

    class Listing1_34
    {
        public static void Test_ConcurrentDictionary()
        {
            var dict = new ConcurrentDictionary<string, int>();
            if (dict.TryAdd("k1", 42))
            {
                Console.WriteLine("Added");
            }

            if (dict.TryUpdate("k1", 21, 42))
                Console.WriteLine("42 updated to 21");

            dict["k1"] = 42;

            int r1 = dict.AddOrUpdate("k1", 3, (s, i) => i * 2);  // if the key doesn't exist, add the key value using the first two argumnets. 
                                                                  //If the key already exist, use the function provided by the third argument to update the key value pair, s is the exising key, i is the existing value

            //dict.AddOrUpdate("k2", 22, (s, i) => i * 2);  // if this line is uncommented, next line will get the value instead of add
            int r2 = dict.GetOrAdd("k2", 33);  // if the key already exist, get the value. Else, add the key value pair in the arguments

            int DicValue = 0;
            if (dict.TryGetValue("k1", out DicValue))
                Console.WriteLine("The value for Key K1 is:{0}", DicValue);
            if (dict.TryGetValue("k2", out DicValue))
                Console.WriteLine("The value for Key K2 is:{0}", DicValue);

            Console.ReadLine();
        }

    }

    class Listing1_35
    {
        // two threads competing the data
        public static void Test_MultiThread_Accesing_SharedData()
        {
            DateTime StartDTM = DateTime.Now;
            int n = 0;
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    n++;
            });

            for (int i = 0; i < 1000000; i++)
                n--;
            up.Wait();
            Thread.Sleep(1000); // even though we waitted the Task thread to finish, but n is still not 0... 
            Console.WriteLine("{0}  The TimeElapse:{1} - {2} ", n, StartDTM.ToString("yyyy-MM-dd HH:mm:ss.ffff"),DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
            Console.ReadLine();
            
        }
    }

    class Listing1_36
    {
        public static void Test_MultiThread_Accesing_SharedData_WithLock()
        {
            // by adding the lock, now n is zero in the end. And the running time is not significently increased by using lock. Performance is okay..
            DateTime StartDTM = DateTime.Now;
            int n = 0;

            object _lock = new object();

            // object _lock can only be locked by one thread at one time. Only when the object has been locked, the code in the following block can be executed..., so n can not be accessed by two threads concurrently
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    lock (_lock)
                    {
                        n++;
                    }
            });

            for (int i = 0; i < 1000000; i++)
                lock (_lock)
                {
                    n--;
                }
            up.Wait();
            Thread.Sleep(1000); 
            Console.WriteLine("{0}  The TimeElapse:{1} - {2} ", n, StartDTM.ToString("yyyy-MM-dd HH:mm:ss.ffff"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
            Console.ReadLine();
        }
    }


    class Listing1_37
    {

        public static void Test_Create_DeadLock()
        {
            // you will see a deadlock here, no thread can enter the inner block to lock A and B
            object lockA = new object();
            object lockB = new object();

            var up = Task.Run(() =>
            {
                lock (lockA)  // The lock keyword ensures that a block of code runs to completion without interruption by other threads. By obtaining a mutual-exclusion lock for a given object for the duration of the code block.
                {
                    Console.WriteLine("In lockA block");

                    Thread.Sleep(1000);
                    lock (lockB)
                    {
                        Console.WriteLine("Locked A and B");
                    }
                }
            });

            lock (lockB)
            {
                Console.WriteLine("In lockB block");

                Thread.Sleep(1000);
                lock (lockA)
                {
                    Console.WriteLine("Locked B and A");
                }
            }
            up.Wait();
            Console.ReadLine();
        }
    }

    class Listing1_37_2
    {
        // shows that, thread 2 has to wait for thread 1 to release lockA...
        public static void Test_Create_DeadLock()
        {
            object lockA = new object();

            var up = Task.Run(() =>
            {
                lock (lockA)  // The lock keyword ensures that a block of code runs to completion without interruption by other threads. By obtaining a mutual-exclusion lock for a given object for the duration of the code block.
                {
                    Console.WriteLine("In lockA blockA, sleep for 1 second now {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));                   
                    Thread.Sleep(1000);
                    Console.WriteLine("lockA after sleep {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

                }
            });

            lock (lockA)
            {
                Console.WriteLine("In lockA block_lockB, sleep for 1 second now {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));               
                Thread.Sleep(1000);
                Console.WriteLine("lockB after sleep {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
            }
            up.Wait();
            Console.ReadLine();
        }
    }

    class Listing1_38
    {
        public static void Test_GeneratedCode_for_LockStatement()
        {
            // The lock keyword calls Enter at the start of the block and Exit at the end of the block
            object gate = new object();

            bool __lockTaken = false;
            try
            {
                Monitor.Enter(gate, ref __lockTaken);  // Acquires an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken
                // Add code that accesses resources that are protected by the lock.
            }
            finally
            {
                if (__lockTaken)
                    Monitor.Exit(gate);
            }
        }
    }

    class Listing1_39
    {
        private static int _flag = 0;
        private static int _value = 0;

        public static void Test_Multithread_Poteintial_Problem()
        {            
            var up = Task.Run(()=> {
                Thread2();
            });

            Thread1();

            Console.ReadLine();
        }

        public static void Thread1()
        {
            _value = 5;   // it is possible that complier switch of execution order of these two assignment
            _flag = 1;
        }

        public static void Thread2()
        {
            if (_flag == 1)
                Console.WriteLine(_value);
        }
    }

    class Listing1_40
    {
        public static void Test_InterLockedClass_AtomicOperation()
        {
            int n = 0;
            // there are other atomic operations in Interlocked class you can use
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    Interlocked.Increment(ref n); // here we are using the reference of n
            });
            
            for (int i = 0; i < 1000000; i++)
                Interlocked.Decrement(ref n);
            Console.WriteLine("The value before thread up is done: {0}",n);
            up.Wait();
            Console.WriteLine(n);

            Console.ReadLine();
        }
    }

    class Listing1_41
    {
        static int value = 1;
        public static void Test_NonAtomic_Operations()
        {
            Task t1 = Task.Run(() =>
            {
                if (value == 1)  // t1 is 1 now, so we entered the block
                {
                    Console.WriteLine("t1_1: the value is {0} now", value);
                    Thread.Sleep(2000); // during the sleep, value has been change to 3 by t2
                    
                    value = 2;
                    Console.WriteLine("t1_2: the value has been changed to {0} now", value);
                }
            });

            Task t2 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                value = 3;
                Console.WriteLine("t2_1: the value has been changed to {0} now",value);
            });

            // you can use this atomic compare and exchange operation to replace Task t1
            //Interlocked.CompareExchange(ref value, 2, 1);  // compare value with 1, if they are equal, set value to 2

            Task.WaitAll(t1, t2);
            Console.WriteLine(value);
            Console.ReadLine();
        }
    
    }

    class Listing1_42
    {
        public static void Test_CancellationToken()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)  // check if Cancel function is called
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
            },token); // pass the token variable to the function

            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();
            cancellationTokenSource.Cancel();  // when you press enter, then we will run this Cancel function to stop task

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }
    }

    class Listing1_43
    {
        public static void Test_Throw_CanceledException()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)  // check if Cancel function is called
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }

                token.ThrowIfCancellationRequested();   // throw the CancellationRequested exception when the thread is stopped due to CancellationRequest
            }, token);

            try
            {
                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();

                cancellationTokenSource.Cancel();
                task.Wait();
            }
            catch (AggregateException e) // catch the throwed exception
            {
                Console.WriteLine("The exception message is:{0}",e.InnerExceptions[0].Message);
            }

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }
    }

    class Listing1_44
    {
        public static void Test_Add_Continuation_For_CanceledTasks()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)  // check if Cancel function is called
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }

                // the following two lines (throw new... / token...) have the same effect. You can use either one
                //throw new OperationCanceledException();
                token.ThrowIfCancellationRequested();   // throw the CancellationRequested exception when the thread is stopped due to CancellationRequest

            }, token).ContinueWith((t) =>
            {// no need to explicitly use catch{} or handle() here, just put the handler code here directly.

                Console.WriteLine("You have canceled the task");  // put handler code here directly, this onCanceled exception has been catched by the ContinueWith function

                //try {
                //    Console.WriteLine("You have canceled the task");
                //} catch (AggregateException e)
                //{
                //    Console.WriteLine("the error is:{0}",e.Message);
                //}


                //t.Exception.Handle((e) => 
                //{
                //    Console.WriteLine("t.Status {0}", t.Status);
                //    return true;
                //});
                //Console.WriteLine("You have canceled the task");
            }, TaskContinuationOptions.OnlyOnCanceled);

                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();

                cancellationTokenSource.Cancel();
                task.Wait();

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }
    }

    class Listing1_45
    {
        public static void Test_timeout_on_Task()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task LongRunning = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)  // check if Cancel function is called
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("LongRunning has been canceled");

            }, token);

            int index = Task.WaitAny(new[] { LongRunning }, 1000);  // put any array of tasks here, wait for any of those tasks to complete within the given interval
            // return the index of the completed Task object in the tasks array, if no task completed, return -1 (in our case, it will return -1)

            if (index == -1)
            {
                Console.WriteLine("Task timed out");
                cancellationTokenSource.Cancel();
            }
                
            LongRunning.Wait();
            Console.ReadLine();
        }
    }

    class Listing1_46_51
    {
        public static void Test_Equality_Operation()
        {
            int x = 42;
            int y = 1;
            int z = 42;
            Console.WriteLine(x == y);
            Console.WriteLine(x == z);
            Console.ReadLine();
        }

        public static void Test_Boolean_Or_Operator()
        {
            bool x = true;
            bool y = false;
            bool result = x || y;
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void Test_OrShortCircuit()
        {
            bool x = true;
            bool result = x || GetY();  // due to the ShorCircuit featureh of || operator, the function GetY() is not called
            Console.WriteLine("The evaluation result is: {0}", result);
            Console.ReadLine();
       
        }

        private static bool GetY()
        {
            Console.WriteLine("This method get called");
            return true;
        }


        public static void Test_And_Operator()
        {
            int value = 42;
            bool result = (0 < value) && (value < 100);
            Console.WriteLine("The evaluation result is: {0}", result);
            Console.ReadLine();
        }

        public static void Test_Short_Circuiting_And_Operator()
        {
            string input = "abc";  // use null or a string and see the dif output
            bool result = (input != null) && (And_Operator_Process_Input(input));
            if (!result)
                Console.WriteLine("The result is false, the function is not executed");
            Console.ReadLine();
        }

        private static bool And_Operator_Process_Input(object y)
        {
            Console.WriteLine("The input is {0}",y);
            return true;
        }

        public static void Test_XOR_Operator()
        {
            bool a = true;
            bool b = false;

            Console.WriteLine(a ^ a);
            Console.WriteLine(a ^ b);
            Console.WriteLine(b ^ b);
            Console.ReadLine();
        }
    }

    class Listing1_52_63
    {
        public static void Test_52_BasicIF()
        {
            bool b = true;
            if (b)
                Console.WriteLine("true");
            Console.ReadLine();
        }

        public static void Test_53_IfWithCodeBlock()
        {
            bool b = true;
            if (b)
            {
                Console.WriteLine("Both these lines");
                Console.WriteLine("Will be executed");
            }
            Console.ReadLine();
        }

        public static void Test_54_CodeBlocks_ANd_Scoping()
        {
            bool b = true;
            if (b)
            {
                int r = 42;
                b = false;
            }
            //Console.WriteLine("r is:{0}",r);  // r is not accessable outside the block
            Console.ReadLine();
        }

        public static void Test_55_ElseStatement()
        {
            bool b = false;
            if (b)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
            Console.ReadLine();
        }

        public static void Test_56_Multiple_IfElse()
        {
            bool b = false;
            bool c = true;

            if (b)
                Console.WriteLine("b is true");
            else if (c)
                Console.WriteLine("c is true");
            else
                Console.WriteLine("c is false");
            Console.ReadLine();
        }

        public static void Test_57_Nested_If_Statement()
        {
            bool b = false;
            bool c = true;

            if (c)
            {
                if (b)
                    Console.WriteLine("nested If block");
                else
                    Console.WriteLine("nested else block");
            }
            Console.ReadLine();
        }

        public static void Test_58_Null_Coalescing_Operator()
        {
            int? x = 2;  // Nullable types can represent all the values of an underlying type, and an additional null value => so if we don't use int?, we can not assign null to int x
            int y = x ?? -1;   // set y to the value of x if x is not null, else set -1
            Console.WriteLine("y :{0}", y);
            Console.ReadLine();
        }

        public static void Test_59_Nesting_Null_Coalescing_Operator()
        {
            int? x = null;
            int? z = null;
            int y = x ??
                z ??
                -1; // if x is null, then evaluate z, if z is also null, then set -1 to y. If x is not null, we will assign x to y and stop there
            Console.WriteLine("y :{0}", y);
            Console.ReadLine();
        }

        public static void Test_60_Conditional_Operator()
        {
            bool p = false;
            int a = 0;
            if (p)
                Console.WriteLine("1");
            else
                Console.WriteLine("2");

            a = p?  1 : 2;
            Console.WriteLine("The value of a is:{0}",a);
            Console.ReadLine();
        }

        public static void Test_61_Check(char input)
        {
            if (input == 'a' || input == 'e' || input == 'i' || input == 'o' || input == 'u')
                Console.WriteLine("Input is a vowel");
            else
                Console.WriteLine("Input is a consonant");
            Console.ReadLine();
        }

        public static void Test_62_CheckWithSwitch(char input)
        {
            // be careful with the syntax of switch statement
            switch (input)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    {
                        Console.WriteLine("Input is a vowel");
                        break;
                    }
                case 'y':
                    {
                        Console.WriteLine("Input is sometimes a vowel");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Input is a consonant");
                        break;
                    }
            }
            Console.ReadLine();
        }

        public static void Test_63_GoTo_Within_switch()
        {
            int i = 1;
            switch (i)
            {
                case 1:
                    {
                        Console.WriteLine("case 1");
                        goto case 2;
                    }
                case 2:
                    {
                        Console.WriteLine("case 2");
                        break;
                    }
            }
            Console.ReadLine();
        }
    }



    class Listing1_64_74
    {
        public static void Test_64_Basic_ForLoop()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index++)
            {
                Console.Write(values[index]);
            }
            Console.ReadLine();
        }

        public static void Test_65_ForLoop_With_MultipleVariables()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int x = 0, y = values.Length - 1; x < values.Length && y >= 0; x++, y--)  // this multiple variable syntax could be useful!
            {
                Console.Write(values[x]);
                Console.Write(values[y]);
            }
            Console.ReadLine();
        }

        public static void Test_66_ForLoop_With_Custom_Increment()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 1; index < values.Length; index += 2)
            {
                Console.Write(values[index]);
            }
            Console.ReadLine();
        }

        public static void Test_67_ForLoop_with_Break()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) break;
                Console.Write(values[index]);
            }
            Console.ReadLine();
        }

        public static void Test_68_ForLoop_Witn_Continue()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) continue;  // will print 12356, and skip 4
                Console.Write(values[index]);
            }
            Console.ReadLine();
        }

        public static void Test_69_While_Statement()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            int index = 0;
            while (index < values.Length)
            {
                Console.Write(values[index]);
                index++;
            }
            Console.ReadLine();
        }

        public static void Test_70_DoWhile_Loop()
        {
            do
            {
                Console.WriteLine("Executed once!");
            }
            while (false);
            Console.ReadLine();
        }

        public static void Test_71_ForeachLoop()
        {
            int[] values = { 1,2,3,4,5,6};

            foreach (var i in values)
            {
                Console.Write(i);
            }
            Console.ReadLine();
        }

        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public static void Test_72_Changing_Items_In_a_Foreach()
        {
            var people = new List<Person>
            {
                new Person() { FirstName = "John", LastName = "Doe"},
                new Person() {FirstName = "Jane", LastName = "Doe"},
            };

            foreach (Person p in people)
            {
                p.LastName = "Changed";  // you can change the object property p referenced to
                //p = new Person();   // but you can not change p to point to a new object (a dif memory position).
                                      // that is because the memory address is saved in the call stack, and can not change?
            }

            foreach (Person p in people)
            {
                Console.WriteLine(p.LastName);  // the p.LastName has been changed to "Changed" now
            }

            Console.ReadLine();
        }

        public static void Test_73_Complier_generated_Code_For_ForeachLoop()
        {
            var people = new List<Person>
            {
                new Person() { FirstName = "John", LastName = "Doe"},
                new Person() {FirstName = "Jane", LastName = "Doe"},
            };

            List<Person>.Enumerator e = people.GetEnumerator();   // List.GetEnumerator(): Returns an enumerator that iterates through the List<T>.

            // List<T>.Enumerator Structure is used to Enumerates the elements of a List<T>
            try
            {
                Person v;
                while (e.MoveNext())
                {
                    v = e.Current;
                    Console.WriteLine(v.FirstName);
                }
            }
            finally {
                System.IDisposable d = e as System.IDisposable;  // You can use the as operator to perform certain types of conversions between compatible reference types or nullable types.
                if (d != null) d.Dispose();  // Dispose(): Releases all resources used by the List<T>.Enumerator.
            }

            Console.ReadLine();
        }

        public static void Test_74_Goto_Statement_With_Label()
        {
            int x = 5;
            if (x == 3) goto customLable;
            x++;

            // even though the goto statement is not executed, the code will still execute the labed code sequentially...
            customLable:   // like in this case, the value of x will always been printed. But when you execute goto, the code between goto statement and the label will be skipped
            Console.WriteLine(x);
            Console.ReadLine();
        }
    }

}
