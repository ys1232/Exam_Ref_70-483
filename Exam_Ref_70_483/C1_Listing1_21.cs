using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Windows;

namespace Exam_Ref_70_483
{
    

    public static class Listing1_1
    {
        /**
        * create multiple threads, and see child threds and main threads run parallelly
        * */
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                //Debug.WriteLine("ThreadProc:{0}", i);
                Console.WriteLine("ThreadProc:{0}", i);
                Thread.Sleep(0);
            }
        }

        public static void TestThreadMethod()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();

            for (int i = 0; i < 4; i++)
            {
                //Debug.WriteLine("Main thread: Do some work.");
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }
            t.Join();

            Console.ReadLine();
        }
    }


    class Listing1_2
    {
        /**
        * by default, the IsBackground is false, which means, the child threads will not run in the background, but you can set it to true
        * 
        * */
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc:{0}", i);
                Thread.Sleep(1000);

            }
        }

        public static void CallThreadMethod()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.IsBackground = false;   // try true / false
            t.Start();
            //t.Join();    // if you enabled t.Join() or Console.ReadLine(), the main thread will not exit

            //Console.ReadLine();
        }
    }


    class Listing1_3
    {
        /**
        * here we use ParameterizedThreadStart constructor to create a thread instance, which can accept variable (can only accept one object variable)
        **/
        public static void ThreadMethod(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("ThreadProc:{0}", i);
                Thread.Sleep(1000);

            }
        }

        public static void CallThreadMethod()
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));

            t.Start(3);     
        }


    }


    class Listing1_4
    {
        /**
         * we use a shared variable <stopped> to terminate the child thread
         * try to understand why this variable can be shared between multiple threads
         * */
        public static void stopThread()
        {
            bool stopped = false;
            Thread t = new Thread(new ThreadStart(() =>      // we are using a lambda inline function here
            {
                while (!stopped)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);
                }
            }));

            t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            stopped = true;
            t.Join();

            //Console_Holding.Holding();
        }
    }


    class Listing1_5
    {
        //[ThreadStatic] // if this [ThreadStatic] is removed, the variable _field will be shared global value...
        public static int _field;
        public static void ThreadStaticAttribute()
        {
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread A:{0}{1}", _field, ",");
                    Thread.Sleep(1000);
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread B: {0}", _field);
                    Thread.Sleep(1000);
                }
            }).Start();

            Console.ReadKey();
        }
    }


    class Listing1_6
    {
        public static ThreadLocal<int> _field = new ThreadLocal<int>(() =>   // provde Thread-Local storage of data
        {                                                                    // each thread get a local copy of this variable, can be assigned dif initial values
            //return Thread.CurrentThread.ManagedThreadId;
            return 3;
        });

        public static void ThreadLocal()
        {
            new Thread(() =>
            {
                for (int x = 0; x < _field.Value; x++)
                {
                    Console.WriteLine("Thread A:{0}, the _field is:{1}", x, _field);
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < _field.Value; x++)
                {
                    Console.WriteLine("Thread B:{0}, the _field is:{1}", x, _field);
                }
            }).Start();


            Console.ReadKey();
        }

    }


    class Listing1_7
    {
        /**
         *  ThreadPool.QueueUserWorkItem is another way to create new thread, and we reuse threads here instead of already create new one...
         *  the below method shows, some threads reuse the same ThreadID... but some details are still not clear in this experiment, try to figure it out when you have time...
         *  
         * */
        public Listing1_7()  // you can not put a ThreadPool in a class method, but in constructor, it works fine...
        {
            int MaxThread = 0;
            int MaxThreadId = 0;

            while (true)
            {
                ThreadPool.QueueUserWorkItem((s) =>
                {
                    if (Thread.CurrentThread.ManagedThreadId > MaxThreadId)
                    {
                        MaxThreadId = Thread.CurrentThread.ManagedThreadId;
                    }
                    MaxThread++;
                    Console.WriteLine("ThreadId is: {0}, ThreadCNT is: {1}", MaxThreadId, MaxThread);
                    Thread.Sleep(0);
                });

                Thread.Sleep(10);
                if (MaxThread >= MaxThreadId)
                {

                    Console.WriteLine("MaxThread:{0}, MaxThreadId:{1}, We are reusing the threads now, exit...", MaxThread, MaxThreadId);
                    break;
                }
            }
            Console.ReadLine();
        }

    }


    class Listing1_8
    {
        public static void newTask()
        {
            // we print the ThreadID for the Task and main, to prove that they are using dif threads

            Task t = Task.Run(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write('*');
                    Console.WriteLine("Child Thread, ID:{0}", Thread.CurrentThread.ManagedThreadId);
                }
            });

            Console.WriteLine("Main Thread, ID:{0}", Thread.CurrentThread.ManagedThreadId);
            t.Wait();
            Console.ReadLine();
        }
    }


    class Listing1_9
    {
        public static void TaskReturnValue()
        {
            Task<int> t = Task.Run(() =>   // if task has a return value, then you can use Task<T>, here T is the returned Data type
            {                               // use Task.Result to retrieve this returned value
                Thread.Sleep(1000);
                return 42;
            });
            Console.WriteLine(t.Result);  // the writeLine will work only when we got the return value after the sleep time
            Console.ReadLine();
        }
    }


    class Listing1_10
    {
        public static void TaskContinuation()
        {
            // by using ContinueWith, we added another method to utilize the result returned from the Task

            Task<int> t = Task.Run(() =>  // () means this anonyous function takes no input
            {
                return 42;
            }).ContinueWith((i) =>      // (i) means this anonyous function takes one input parameter
            {                           // i is an action to run when the Task completes. When run, the delegate will be passed the completed task as an argument.
                return i.Result * 2;
            });

            Console.WriteLine(t.Result);
            Console.ReadLine();
        }

    }


    class Listing1_11
    {
        /**
         * ???  need to understand and test when to use the TaskContinueationOptions other than Completed
         * */
        public static void TaskContinueOverWrite()
        {
            Task<int> t = Task.Run(() =>
            {
                return 42;
            });

            t.ContinueWith((i) =>
            {
                Console.WriteLine("Canceled");
                Console.ReadLine();
            }, TaskContinuationOptions.OnlyOnCanceled);

            t.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
                Console.ReadLine();
            }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = t.ContinueWith((i) =>
            {
                Console.WriteLine("Completed {0}", i.Result);
                Console.ReadLine();
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            completedTask.Wait();
        }
    }


    class Listing1_12
    {
        public static void ChildTask()
        {
            /**
             * The result in Console is very interesting, it shows the running order of Parent task, Child task and main program, and the threadID they used. Since the threads are from the pool, so they may and maynot taks the same thread
             * */
            Console.WriteLine("The ThreadID for Main_BeforeTask:{0}", Thread.CurrentThread.ManagedThreadId);

            Task<Int32[]> parent = Task.Run(() =>
            {
                Console.WriteLine("The ThreadID for Task_Parent:{0}", Thread.CurrentThread.ManagedThreadId);
                var results = new Int32[3];
                new Task(() =>
                {
                    results[0] = 1;
                    Console.WriteLine("The ThreadID for Child_1:{0}", Thread.CurrentThread.ManagedThreadId);
                },
                    TaskCreationOptions.AttachedToParent).Start();  // set the TaskCreationOptions to AttachedToParent
                new Task(() =>
                {
                    results[1] = 2;
                    Console.WriteLine("The ThreadID for Child_2:{0}", Thread.CurrentThread.ManagedThreadId);
                },
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() =>
                {
                    results[2] = 3;
                    Console.WriteLine("The ThreadID for Child_3:{0}", Thread.CurrentThread.ManagedThreadId);
                },
                    TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            // actually the ContinueWith will wait for the Parent Task to finish, but not the Child Task. So you may see 0 in the array, which means the Child Task hasn't run yet
            Task finalTask = parent.ContinueWith(       // when all child tasks are done, then run this
                (parentTask) =>
                {
                    Console.WriteLine("The ThreadID for ContinueWithFunction:{0}", Thread.CurrentThread.ManagedThreadId);
                    foreach (int i in parentTask.Result) // parentTask.Result is the array Int32[3]
                        Console.WriteLine(i);
                });

            Console.WriteLine("The ThreadID for Main_AfterTask:{0}", Thread.CurrentThread.ManagedThreadId);
            finalTask.Wait();

            Console.ReadLine();

        }
    }



    class Listing1_12_WithFactory
    {
        public static void ChildTask()
        {
            /**
             * Here we use the factory to create child task, we added a new option:TaskContinuationOptions.ExecuteSynchronously. 
             * I can not add this option when using new Task(()) method to create child Task. However, I can still see 0 in the array with this option
             * which means, the continous function runs before all children complete
             * */
            Console.WriteLine("The ThreadID for Main_BeforeTask:{0}", Thread.CurrentThread.ManagedThreadId);

            Task<Int32[]> parent = Task.Run(() =>
            {
                Console.WriteLine("The ThreadID for Task_Parent:{0}", Thread.CurrentThread.ManagedThreadId);
                var results = new Int32[3];

                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() =>
                {
                    results[0] = 1;
                    Console.WriteLine("The ThreadID for Child_1:{0}", Thread.CurrentThread.ManagedThreadId);
                });

                tf.StartNew(() =>
                {
                    results[1] = 2;
                    Console.WriteLine("The ThreadID for Child_2:{0}", Thread.CurrentThread.ManagedThreadId);
                });

                tf.StartNew(() =>
                {
                    results[2] = 3;
                    Console.WriteLine("The ThreadID for Child_3:{0}", Thread.CurrentThread.ManagedThreadId);
                });

                return results;
            });

            // actually the ContinueWith will wait for the Parent Task to finish, but not the Child Task. So you may see 0 in the array, which means the Child Task hasn't run yet
            Task finalTask = parent.ContinueWith(       // when all child tasks are done, then run this
                (parentTask) =>
                {
                    Console.WriteLine("The ThreadID for ContinueWithFunction:{0}", Thread.CurrentThread.ManagedThreadId);
                    foreach (int i in parentTask.Result) // parentTask.Result is the array Int32[3]
                        Console.WriteLine(i);
                });

            Console.WriteLine("The ThreadID for Main_AfterTask:{0}", Thread.CurrentThread.ManagedThreadId);
            finalTask.Wait();

            Console.ReadLine();

        }
    }


    class Listing1_13
    {
        public static void UseTaskFactory()
        {
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];

                // same thing as 1_12, we just putted the options in one place and reuse them when creating Child Task
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => results[0] = 0);
                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);

                return results;
            });

            var finalTask = parent.ContinueWith(
                parentTask =>
                {
                    foreach (int i in parentTask.Result)
                        Console.WriteLine(i);
                });
            finalTask.Wait();
            Console.ReadLine();
        }
    }

    class Listing1_14
    {
        public static void TaskWaitAll_Test()
        {
            // we use WaitAll() to wait for all tasks to finish
            Task[] tasks = new Task[3];

            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
            });

            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("2");
            });

            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("3");
            });

            Task.WaitAll(tasks);  // wait for all tasks provided by tasks to complete execution
            Console.ReadLine();
        }
    }

    class Listing1_15
    {
        public static void waitAny_Test()
        {
            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = Task.Run(() => { Thread.Sleep(1000); return 1; });
            tasks[1] = Task.Run(() => { Thread.Sleep(800); return 2; });
            tasks[2] = Task.Run(() => { Thread.Sleep(600); return 3; });

            Thread.Sleep(200); // after the sleep, even though all tasks are completed, the WaitAny will still return 3 times

            while (tasks.Length > 0)
            {

                int i = Task.WaitAny(tasks);   // this int i is the index, not the return value....
                Task<int> completedTask = tasks[i];

                Console.WriteLine(completedTask.Result);
                var temp = tasks.ToList();
                temp.RemoveAt(i);  // remove the completed element and recreate the task array
                tasks = temp.ToArray();
            }

            // if we use the below while loop instead, the console will only return once, the first completed task index id.
            // but in the while loop above, since we removed the completed task, the console will return the next completed one...
            //while (1 == 1)
            //{
            //    int i = Task.WaitAny(tasks);  // this int i is the index, not the return value....
            //    Console.WriteLine("Completed Task:{0}", i);
            //    Thread.Sleep(1000);
            //}

            //Console.ReadLine();

        }
    }


    class Listing1_16
    {
        // use Parallel.For and Parallel.ForEach to execute multiple tasks parallelly
        // from the timeStamp, we know all tasks doesn't run at exactly the same time, but at least it is not single thread sequentially
        public static void Parallel_Loop()
        {       // use ParallelOptions to set the max concurrent threads
            Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (i) =>  // i is the counter for this thread
            {
                Console.WriteLine("{0} ForLoop Counter Started at:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), i);
                Thread.Sleep(10000);
                Console.WriteLine("{0} ForLoop Counter Finished at:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), i);
            });

            var numbers = Enumerable.Range(0, 10);
            Parallel.ForEach(numbers, i =>
            {
                Console.WriteLine("ForeachLoop:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Thread.Sleep(1000);
            });

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
            Console.ReadLine();
        }
    }

    class Listing1_17
    {
        public static void TerminateParallelLoop_Test()
        {
            bool breakingFlag = false;
            ParallelLoopResult result = Parallel.For(0, 1000, (int i, ParallelLoopState loopState) =>
            {
                //ParallelLoopState enables iterations of parallel loops to interact with other iterations. 
                //An instance of this class is provided by the Parallel class to each loop; you can not create instances in your code.

                if (i == 500)
                {
                    Console.WriteLine("Breaking loop");
                    breakingFlag = true;
                    loopState.Break();
                }
                return;
            });

            // how can we know the loop is completed?
            while (true)
            {
                if (breakingFlag)
                {
                    Console.WriteLine("Result.IsCompleted:{0},result.LowestBreakIteration:{1}", result.IsCompleted, result.LowestBreakIteration);
                    Console.ReadLine();
                }
            }

        }
    }

    class Listing1_18
    {
        public static void Test_Task_Async_And_Await()
        {
            string result = DownloadContent().Result;
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync("http://www.microsoft.com"); // the DownloadContent function with Async method will be blocked until this Async method finished and return
                return result;                  // this Async method uses asynchronous code internally, while being blocked, this thread can do other works
            }
        }
    }

    // you can not run Listing 1- 21 directly, required functioning piece is missing, or required library is not avaiable

    //class Listing1_19
    //{
    //    public Task SleepAsyncA(int millisecondsTimeout)
    //    {
    //        return Task.Run(() => Thread.Sleep(millisecondsTimeout));       // one thread from thread pool will still be occupied when the thread is asleep
    //    }

    //    public Task SleepAsyncB(int millisecondsTimeout)       // ??? I really don't understand how this function works...
    //    {                                                        // does not occupy a thread while waiting for the timer to run
    //        TaskCompletionSource<bool> tcs = null;                         
    //        var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);
    //                                                                // public Timer(TimerCallback callback,object state,int dueTime,int period) 
    //                                                                // callback:A TimerCallback delegate representing a method to be executed.
    //                                                                // When a managed thread is created, the method that executes on the thread is represented by a ThreadStart delegate
    //        tcs = new TaskCompletionSource <bool> (t);
    //        t.Change(millisecondsTimeout, -1);
    //        return tcs.Task;
    //    }
    //}



    //class Listing1_20_21
    //{

    //    private async void Button_Click(object sender, RoutedEventArgs e)
    //    {
    //        HttpClient httpClient = new HttpClient();

    //        string content = await httpClient.GetStringAsync("http://www.microsoft.com").ConfigureAwait(false);

    //        Output.Content = content;
    //    }

    //    private async void Button_Click(object sender, RouteEventArgs e)
    //    {
    //        HttpClient httpClient = new HttpClient();

    //        string content = await httpClient.GetStringAsync("http://www.microsoft.com").ConfigureAwait(false);

    //        using (FileStream sourceStream = new FileStream("temp.html", FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
    //        {
    //            byte[] encodedText = Encoding.Unicode.GetBytes(content);
    //            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length).ConfigureAwait(false);
    //        }

    //    }



    //}



}
