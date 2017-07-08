#define DEBUG
#define Yuan
#define Shen
#undef Shen
//#undef DEBUG   // Then the function in C3_Listing34 will show "Not debug"
// The #define directive must appear in the file before you use any instructions that aren't also preprocessor directives
// You can define or undefine Symbols in the begining of a file

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Exam_Ref_70_483
{
    //class C3_Listing29
    //{
    //}

    //class C3_Listing30
    //{
    //}

    //class C3_Listing31
    //{
    //}

    //class C3_Listing32
    //{
    //}

    class C3_Listing33
    {
        // Under Solution properties, change the project configuration to Release Mode / Debug Mode, you will see the difference
        public static void Test_debug_Mode_release_Mode()
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);  // in Debug mode, TimerCallback function will be called every 2 seconds, but in release mode, TimerCallback will only be called once
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
            GC.Collect();
        }
    }

    class C3_Listing34_35
    {
        public static void Test_Compiler_Directive_IF()
        {
            /**
             * #if directive is used to check if the specified symbol is defined
             * You can define or undef any symbols in the beginging of the file (as shown in this file)
             * */

#if DEBUG
            Console.WriteLine("Debug Mode");
#else
                Console.WriteLine("Not debug");
#endif

#if Yuan
            Console.WriteLine("Symbol Yuan Defined");
#else
                Console.WriteLine("Symbol Yuan not defined");
#endif

#if Shen
            Console.WriteLine("Symbol Shen Defined");
#else
                Console.WriteLine("Symbol Shen not defined");
#endif
            Console.ReadLine();
                
        }
    }

    class C3_Listing36
    {
        //// we don't have Assembly property here .... no idea why... so this function won't work
        //public Assembly LoadAssembly<T>()
        //{
        //#if !WINRT
        //            Assembly assembly = typeof(T).Assembly;   // we don't have Assembly property here .... no idea why...
        //#else
        //            Assembly assembly = typeof(T).GetTypeInfo().Assembly;
        //#endif
        //            return assembly;
        //}
    }

    class C3_Listing37
    {
        public static void Test_Warning_Error_Directive()
        {
#warning This Code is obsolete

// since we are now in DEBUG mode, and the code in this IDE is precompiled, if we uncomment the code below, the error will shown, then we can not even run the code
//#if DEBUG
//#error Debug build is not allowed
//#endif
        }
    }

    class C3_Listing38
    {   // run this code and check the Error List window
        // #line lets you modify the compiler's line number and (optionally) the file name output for errors and warnings.
        public static void Test_Line_Directive()
        {  
            // the line# for the warning for "Line 114" will become line 200, and the File will become "otherFileName" instead of C3_Listing29_54.cs
#line 200 "OtherFileName"
#warning  Line 114
#line default
#warning  Line 116
#line hidden
#warning  Line 118
#warning  Line 119
        }
    }

    class C3_Listing39_40
    {
        public static void Test_Pragma_directive()
        {
            // pragma gives the compiler special instructions for the compilation of the file. Syntax: #pragma pragma-name pragma-arguments  
            // in this example, warning will be disabled, and restored again
#pragma warning disable
            while (false)
            {
                Console.WriteLine("Unreachable code");  // when warning is not disabled, warning "unreachable code detected" will be shown
            }
#pragma warning restore

            // here we only disable specific warnings (0162, 0168)
#pragma warning disable 0162, 0168
            int i;  // warning 0168: "variable is declared but never used". Since we disabled warning 0168, we don't see that warning here
#pragma warning restore 0168
            int k;     // now you can see warning 0168 
            while (false)
            {
                Console.WriteLine("Unreachable code");  // warning 0162 "unreachable code detecte" is not shown
            }
#pragma warning restore 0162
            while (false)
            {
                Console.WriteLine("Unreachable code");  // now you can see warning 0162
            }
        }
    }

    class C3_Listing41
    {
        public static void Test_Call_Method_Only_In_Debug_Mode()
        {
            // for the directive #if, we don't need the {} to define the block, 
            // we need #endif to explicitely indicate the end of the entire if/else block
#if DEBUG
            Log("Step1");
            Log("Step2");
            Log("Step3");
#else
            Log("Not in debug mode");
#endif
            Console.ReadLine();
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    class C3_Listing42
    {
        public static void Test_Conditional_Attribute()
        {
            Log("Log_Message"); // this call will work only when symbol DEBUG is defined
            Log_2("Log_2_Message"); // this call will work only when symbol Shen is defined. 
                                    //If the symbol is undefined, this method call will just not be called, but there is no error

            Console.ReadLine();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        [System.Diagnostics.Conditional("Shen")]
        private static void Log_2(string message)
        {
            Console.WriteLine(message);
        }
    }

    class C3_Listing43
    {
        // The DebuggerDisplayAttribute Class controls how an object, property, or field is displayed in the debugger variable windows. 
        // This attribute can be applied to types, delegates, properties, fields, and assemblies.
        
        [System.Diagnostics.DebuggerDisplay("Name = {FirstName} {LastName}")]
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        // by using the DebuggerDisplay function, during debug, in variable window, 
        // you will set the value for new_Person as FirstName LastName instead of simpliy object name
        public static void Test_DebuggerDisplay()
        {
            Person new_Person = new Person();
            new_Person.FirstName = "Yuan";
            new_Person.LastName = "Shen";
        }
    }

    /**
    * Program Database Files (C#, F#, and Visual Basic) A program database (PDB) file holds debugging and project state information 
    * that allows incremental linking of a debug configuration of your program. A PDB file is created when you build with /debug. 
    * You can build applications with /debug:full or /debug:pdbonly.
    * 
    * To set this compiler option in the Visual Studio development environment
    Open the project's Properties page.
    Click the Build property page.
    Click the Advanced button.
    Modify the Debug Info property.
    * For this project, find the generated PDB file in 
    * C:\Visual_Studio_Project\DotNet_Projects\ExamRef_70_483_Chapter1\ExamRef_70_483_Chapter3\bin\Debug\netcoreapp1.1\ 
     **/

    //class C3_Listing44
    //{

    //}

    class C3_Listing45
    {
        public static void Test_Debug_Class()
        {
            // the debug output will be shown in immediate window
            Debug.WriteLine("Starting application");
 
            int i = 1 + 2;
            Debug.Assert(i == 3); // Debug.Assert: Checks for a condition; 
                                  // if the condition is false, outputs messages and displays a message box that shows the call stack
                                  // but this message box doesn't work in this console application project 
            Debug.WriteLineIf(i>0,"i is greater than 0");   // will generate debug output only when the bool condition is true
            Console.ReadLine();
        }
    }

    class C3_Listing46
    {
        public static void Test_TraceSourceClass()
        {
            TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);   // SourceLevels.All, specify the levels of trace message filered by source
            traceSource.TraceInformation("......Tracing application......");  // write an informal message to the trace listener

            traceSource.TraceEvent(TraceEventType.Critical,0, "Critical trace");    // write a trace event message to the trace listener
            traceSource.TraceEvent(TraceEventType.Information, 0, "Critical trace");    // eventType includes: Critical, Error, Warning, information...
            // the diff is TraceEvent accepts string trace message and TraceData accepts object array message
            traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b", "c" });   // write trace data to the trace listener
            traceSource.TraceData(TraceEventType.Critical, 1, new object[] { "a", "b", "c" });   // you can put multiple values as trace output message

            traceSource.Flush();
            traceSource.Close();

            Console.ReadKey();
        }
    }

    class C3_Listing47
    {
        public static void Test_Configure_TraceListener()
        {
            Stream outputFile = File.Create("tracefile.txt");
            TextWriterTraceListener textListener = new TextWriterTraceListener(outputFile);

            TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);  

            traceSource.Listeners.Clear();  // clear the default listner, which will output to output window
            traceSource.Listeners.Add(textListener);    // you can add as many listeners as you want to the Listener collection

            traceSource.TraceInformation("Trace outpout");

            traceSource.Flush();
            traceSource.Close();
        }
    }

    //class C3_Listing48
    //{

    //}

    class C3_Listing49
    {
        public static void Test_Writing_Event_Log() // must run Visual Studio as Administrator to use EventLog
        {
            // read the logged event from windows Event Viewer
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Please restart application");
                Console.ReadKey();
                return;
            }
            EventLog myLog = new EventLog();
            myLog.Source = "MySource";
            myLog.WriteEntry("Log event!");
        }
    }

    class C3_Listing50
    {
        public static void Test_Read_From_EventLog()
        {
            EventLog log = new EventLog("MyNewLog");    // read the MyNewLog

            Console.WriteLine("Total entries:"+log.Entries.Count);
            EventLogEntry last = log.Entries[log.Entries.Count - 1];
            Console.WriteLine("Index: " + last.Index);
            Console.WriteLine("Source: " + last.Source);
            Console.WriteLine("Type: " + last.EntryType);
            Console.WriteLine("Time: " + last.TimeWritten);
            Console.WriteLine("Message: " + last.Message);

            Console.ReadKey();
        }
    }

    class C3_Listing51
    {
        // no idea why, but can not subscribe the change, not shown in console
        public static void Test_Write_Data_To_EvntLog()
        {
            EventLog applicationLog = new EventLog("Application", ".", "testEventLogEvent");
            applicationLog.EntryWritten += (sender, e) =>
            {
                Console.WriteLine(e.Entry.Message);
            };
            applicationLog.EnableRaisingEvents = true;
            applicationLog.WriteEntry("Test message", EventLogEntryType.Information);

            Console.ReadKey();
        }
    }

    class C3_Listing52
    {
        const int numberOfIterations = 100000;

        public static void Test_StopWatchClass()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Alrorithm1();
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            sw.Reset();
            sw.Start();
            Algorithm2();
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("Ready...");
            Console.ReadLine();

        }

        public static void Test_Profiler()
        {
            Alrorithm1();
            Algorithm2();
        }
        private static void Algorithm2()
        {
            string result = "";
            for (int x = 0; x < numberOfIterations; x++)
                result += 'a';
        }

        private static void Alrorithm1()
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < numberOfIterations; x++)
                sb.Append('a');
            string result = sb.ToString();
        }

    }

    // cannot install/use PerformanceCounter for current environment (.NETCoreApp, Version=v1.1)
    //class C3_Listing53
    //{
    //    public static void Test_Performance_Counter()
    //    {
    //        Console.WriteLine("Press escape key to stop");
            
    //    }
    //}

    //class C3_Listing54
    //{

    //}

}
